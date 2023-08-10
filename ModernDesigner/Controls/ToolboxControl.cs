using ModernDesigner.Properties;
using ModernDesigner.Toolbox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace ModernDesigner
{
    /// <summary>
    /// Bleak
    /// </summary>
    public class ToolboxControl : UserControl, IToolbox
    {
        protected enum PictureIndex
        {
            Plus,
            Minus,
            Arrow
        }

        private Toolbox.ToolboxService _toolboxService;

        private ToolboxListControl _listbox;

        private ImageList _images;

        private Dictionary<string, List<ToolboxItemWithImage>> _toolboxItems;

        private IContainer components;

        public string DefaultCategoryText { get; set; } = "conventional";


        public string PointerItemText { get; set; } = "pointer";


        public Designer Designer
        {
            get
            {
                return _toolboxService.Designer;
            }
            set
            {
                _toolboxService.Designer = value;
            }
        }

        public ToolboxCategoryCollection Items
        {
            get
            {
                ToolboxCategoryItem[] array = new ToolboxCategoryItem[_toolboxItems.Count];
                int num = 0;
                foreach (KeyValuePair<string, List<ToolboxItemWithImage>> toolboxItem in _toolboxItems)
                {
                    List<ToolboxItemWithImage> value = toolboxItem.Value;
                    ToolboxItem[] array2 = new ToolboxItem[value.Count];
                    int num2 = 0;
                    foreach (ToolboxItemWithImage item in value)
                    {
                        array2[num2++] = item.Item;
                    }

                    array[num++] = new ToolboxCategoryItem(toolboxItem.Key, new ToolboxItemCollection(array2));
                }

                return new ToolboxCategoryCollection(array);
            }
        }

        [Browsable(false)]
        public string SelectedCategory
        {
            get
            {
                int selectedIndex = _listbox.SelectedIndex;
                if (selectedIndex < 0)
                {
                    return null;
                }

                ToolboxBaseItem toolboxBaseItem;
                do
                {
                    toolboxBaseItem = (ToolboxBaseItem)_listbox.Items[selectedIndex--];
                }
                while (!toolboxBaseItem.IsGroup && selectedIndex >= 0);
                if (!toolboxBaseItem.IsGroup)
                {
                    return null;
                }

                return toolboxBaseItem.Text;
            }
            set
            {
                int num = FindCategoryItem(value);
                if (num >= 0)
                {
                    if ((ToolboxCategoryState)((ToolboxBaseItem)_listbox.Items[num]).Tag == ToolboxCategoryState.Collapsed)
                    {
                        ExpandCategory(num);
                    }

                    _listbox.SelectedIndex = num + 1;
                }
            }
        }

        [Browsable(false)]
        public ToolboxItem SelectedItem
        {
            get
            {
                return _listbox.SelectedItem?.Tag as ToolboxItem;
            }
            set
            {
                if (value == null)
                {
                    _listbox.SelectedIndex = -1;
                    this.ToolboxItemUsed?.Invoke(this, new ToolboxItemUsedArgs(SelectedItem));
                    return;
                }

                foreach (ToolboxBaseItem item in _listbox.Items)
                {
                    if (item.Tag == value)
                    {
                        _listbox.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        public event EventHandler BeginDragAndDrop;

        public event EventHandler<ToolboxItemUsedArgs> ToolboxItemUsed;

        public event EventHandler DropControl;

        public ToolboxControl()
        {
            InitializeComponent();
            
            _toolboxItems = new Dictionary<string, List<ToolboxItemWithImage>>();
            _listbox.Click += OnItemClick;
            _listbox.DoubleClick += OnItemDoubleClick;
            _listbox.ItemDrag += OnItemDrag;
            _toolboxService = new Toolbox.ToolboxService(this);
        }

        public void AddCategory(string text)
        {
            ToolboxBaseItem toolboxBaseItem = new ToolboxBaseItem(text, 1, isGroup: true);
            ToolboxPointerItem toolboxPointerItem = new ToolboxPointerItem(PointerItemText);
            ListBox.ObjectCollection items = _listbox.Items;
            object[] items2 = new ToolboxBaseItem[2] { toolboxBaseItem, toolboxPointerItem };
            items.AddRange(items2);
            _toolboxItems[text] = new List<ToolboxItemWithImage>();
        }

        public void AddItem(ToolboxItem item, string category)
        {
            if (category == null)
            {
                category = DefaultCategoryText;
            }

            if (!_toolboxItems.ContainsKey(category))
            {
                AddCategory(category);
            }

            List<ToolboxItemWithImage> list = _toolboxItems[category];
            int image = _images.Images.Add(item.Bitmap, item.Bitmap.GetPixel(0, 0));
            list.Add(new ToolboxItemWithImage(item, image));
            int num = FindCategoryItem(category);
            ToolboxBaseItem item2 = new ToolboxBaseItem(item.DisplayName, image, item);
            _listbox.Items.Insert(num + 1 + list.Count, item2);
        }

        public void RemoveItem(ToolboxItem item, string category)
        {
            if (category == null)
            {
                category = DefaultCategoryText;
            }

            if (!_toolboxItems.ContainsKey(category))
            {
                return;
            }

            List<ToolboxItemWithImage> list = _toolboxItems[category];
            int num = 0;
            foreach (ToolboxItemWithImage item2 in list)
            {
                if (item2.Item == item)
                {
                    list.RemoveAt(num);
                    int num2 = FindCategoryItem(category);
                    _listbox.Items.RemoveAt(num2 + 2 + num);
                    break;
                }

                num++;
            }
        }

        private int FindCategoryItem(string category)
        {
            int num = 0;
            foreach (ToolboxBaseItem item in _listbox.Items)
            {
                if (item.IsGroup && item.Text == category)
                {
                    return num;
                }

                num++;
            }

            return -1;
        }

        private void CollapseCategory(int categoryItem)
        {
            ToolboxBaseItem toolboxBaseItem = (ToolboxBaseItem)_listbox.Items[categoryItem];
            if (toolboxBaseItem.IsGroup && (ToolboxCategoryState)toolboxBaseItem.Tag != ToolboxCategoryState.Collapsed)
            {
                toolboxBaseItem.Tag = ToolboxCategoryState.Collapsed;
                _listbox.BeginUpdate();
                toolboxBaseItem.ImageIndex = 0;
                _listbox.Invalidate(_listbox.GetItemRectangle(categoryItem));
                List<ToolboxItemWithImage> list = _toolboxItems[toolboxBaseItem.Text];
                categoryItem++;
                _listbox.Items.RemoveAt(categoryItem);
                for (int i = 0; i < list.Count; i++)
                {
                    _listbox.Items.RemoveAt(categoryItem);
                }

                _listbox.EndUpdate();
            }
        }

        private void ExpandCategory(int categoryItem)
        {
            ToolboxBaseItem toolboxBaseItem = (ToolboxBaseItem)_listbox.Items[categoryItem];
            if (toolboxBaseItem.IsGroup && (ToolboxCategoryState)toolboxBaseItem.Tag != 0)
            {
                toolboxBaseItem.Tag = ToolboxCategoryState.Expanded;
                _listbox.BeginUpdate();
                toolboxBaseItem.ImageIndex = 1;
                _listbox.Invalidate(_listbox.GetItemRectangle(categoryItem));
                List<ToolboxItemWithImage> list = _toolboxItems[toolboxBaseItem.Text];
                _listbox.Items.Insert(++categoryItem, new ToolboxPointerItem(PointerItemText));
                for (int i = 0; i < list.Count; i++)
                {
                    ToolboxItemWithImage toolboxItemWithImage = list[i];
                    ToolboxBaseItem item = new ToolboxBaseItem(toolboxItemWithImage.Item.DisplayName, toolboxItemWithImage.ImageIndex, toolboxItemWithImage.Item);
                    _listbox.Items.Insert(++categoryItem, item);
                }

                _listbox.EndUpdate();
            }
        }

        private void OnItemDrag(object sender, ToolboxItemDragEventArgs arg)
        {
            if (!arg.Item.IsGroup && arg.Item.Tag is ToolboxItem && this.BeginDragAndDrop != null)
            {
                this.BeginDragAndDrop(this, null);
            }
        }

        private void OnItemDoubleClick(object sender, EventArgs e)
        {
            ToolboxBaseItem selectedItem = _listbox.SelectedItem;
            if (!selectedItem.IsGroup && selectedItem.Tag is ToolboxItem && this.DropControl != null)
            {
                this.DropControl(this, EventArgs.Empty);
            }
        }

        private void OnItemClick(object sender, EventArgs e)
        {
            int selectedIndex = _listbox.SelectedIndex;
            if (selectedIndex < 0)
            {
                return;
            }

            ToolboxBaseItem toolboxBaseItem = (ToolboxBaseItem)_listbox.Items[selectedIndex];
            if (toolboxBaseItem.IsGroup)
            {
                if ((ToolboxCategoryState)toolboxBaseItem.Tag == ToolboxCategoryState.Expanded)
                {
                    CollapseCategory(selectedIndex);
                }
                else
                {
                    ExpandCategory(selectedIndex);
                }
            }
        }

        internal ToolboxItem CreateToolboxItem(Type type)
        {
            try
            {
                ToolboxItemAttribute toolboxItemAttribute = TypeDescriptor.GetAttributes(type)[typeof(ToolboxItemAttribute)] as ToolboxItemAttribute;
                if (toolboxItemAttribute == null)
                {
                    return new ToolboxItem(type);
                }

                ConstructorInfo constructor = toolboxItemAttribute.ToolboxItemType.GetConstructor(new Type[0]);
                if (constructor == null)
                {
                    return new ToolboxItem(type);
                }

                ToolboxItem obj = (ToolboxItem)constructor.Invoke(new object[0]);
                obj.Initialize(type);
                return obj;
            }
            catch (Exception)
            {
                return new ToolboxItem(type);
            }
        }

        public void AddToolboxItem(Type componentType, string category)
        {
            _toolboxService.AddToolboxItem(CreateToolboxItem(componentType), category);
        }

        public void AddToolboxItems(string category, IEnumerable<Type> componentTypes)
        {
            foreach (Type componentType in componentTypes)
            {
                _toolboxService.AddToolboxItem(CreateToolboxItem(componentType), category);
            }
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            _listbox = new ModernDesigner.Toolbox.ToolboxListControl();
            _images = new System.Windows.Forms.ImageList(components);
            SuspendLayout();
            _listbox.BackColor = System.Drawing.SystemColors.Control;
            _listbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            _listbox.Dock = System.Windows.Forms.DockStyle.Fill;
            _listbox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            _listbox.GroupBackColor = System.Drawing.SystemColors.Window;
            _listbox.Images = _images;
            _listbox.ItemHeight = 24;
            _listbox.ItemHoverBackColor = System.Drawing.SystemColors.ControlLight;
            _listbox.Location = new System.Drawing.Point(0, 0);
            _listbox.Name = "_listbox";
            _listbox.SelectedItemBackColor = System.Drawing.SystemColors.ActiveCaption;
            _listbox.SelectedItemBorderColor = System.Drawing.SystemColors.WindowFrame;
            _listbox.SelectedItemHoverBackColor = System.Drawing.SystemColors.InactiveCaption;
            _listbox.Size = new System.Drawing.Size(149, 256);
            _listbox.TabIndex = 0;
            _images.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            _images.ImageSize = new System.Drawing.Size(16, 16);
            _images.TransparentColor = System.Drawing.Color.Transparent;
            BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            base.Controls.Add(_listbox);
            base.Name = "ToolboxControl";
            base.Size = new System.Drawing.Size(149, 256);
            ResumeLayout(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _toolboxService != null)
            {
                _toolboxService.Dispose();
            }

            base.Dispose(disposing);
        }
    }

    internal class ToolboxItemWithImage
    {
        public System.Drawing.Design.ToolboxItem Item { get; }

        public int ImageIndex { get; }

        public ToolboxItemWithImage(System.Drawing.Design.ToolboxItem item, int image)
        {
            this.Item = item;
            this.ImageIndex = image;
        }
    }

}

