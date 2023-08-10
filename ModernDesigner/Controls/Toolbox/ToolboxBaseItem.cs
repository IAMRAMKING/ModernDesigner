using System.Design;
using System.Drawing.Design;
namespace ModernDesigner.Toolbox
{
    /// <summary>
    /// 
    /// </summary>
    public class ToolboxBaseItem
    {
        /// <summary>
        /// Display text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Icon index
        /// </summary>
        public int ImageIndex { get; set; }

        /// <summary>
        /// Whether to group item
        /// </summary>
        public bool IsGroup { get; set; }

        /// <summary>
        /// ToolboxItem
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="imageIndex"></param>
        /// <param name="isGroup"></param>
        public ToolboxBaseItem(string text, int imageIndex, bool isGroup)
        {
            this.Text = text;
            this.ImageIndex = imageIndex;
            this.IsGroup = isGroup;
            if (isGroup) this.Tag = ToolboxCategoryState.Expanded;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="image"></param>
        /// <param name="toolboxItem"></param>
        public ToolboxBaseItem(string text, int image, ToolboxItem toolboxItem)
        {
            this.Text = text;
            this.ImageIndex = image;
            this.IsGroup = false;
            this.Tag = toolboxItem;
        }

    }

}
