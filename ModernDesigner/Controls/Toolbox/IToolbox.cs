using System;
using System.Drawing.Design;

namespace ModernDesigner.Toolbox
{
    /// <summary>
    /// Tool box interface
    /// </summary>
    public interface IToolbox
    {
        /// <summary>
        /// Start the drag and drop control event
        /// </summary>
        event EventHandler BeginDragAndDrop;

        /// <summary>
        /// Control drag and drop completion event
        /// </summary>
        event EventHandler DropControl;

        /// <summary>
        /// Get the toolbox classification collection
        /// </summary>
        ToolboxCategoryCollection Items { get; }

        /// <summary>
        /// Currently selected control
        /// </summary>
        ToolboxItem SelectedItem { get; set; }

        /// <summary>
        /// The classification of control in the current selection
        /// </summary>
        string SelectedCategory { get; set; }

        /// <summary>
        /// Add control classification to the toolbox
        /// </summary>
        /// <param name="text"></param>
        void AddCategory(string text);

        /// <summary>
        /// Add control to the toolbox
        /// </summary>
        /// <param name="item">Control information object</param>
        /// <param name="category">分类</param>
        void AddItem(ToolboxItem item, string category = null);

        /// <summary>
        /// Remove the specified control from the toolbox
        /// </summary>
        /// <param name="item">Control information object</param>
        /// <param name="category">分类</param>
        void RemoveItem(ToolboxItem item, string category = null);

        /// <summary>
        /// Refresh the state of the toolbox
        /// </summary>
        void Refresh();
    }

}
