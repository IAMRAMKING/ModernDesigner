using System;

namespace ModernDesigner.Toolbox
{
    public class ToolboxItemDragEventArgs : EventArgs
    {
        public ToolboxBaseItem Item { get; set; }

        public ToolboxItemDragEventArgs(ToolboxBaseItem item)
        {
            this.Item = item;
        }
    }

}
