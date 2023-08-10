using System;
using System.Drawing.Design;

namespace ModernDesigner.Toolbox
{
    public class ToolboxItemUsedArgs : EventArgs
    {
        public ToolboxItem UsedItem { get; set; }

        public ToolboxItemUsedArgs(ToolboxItem usedItem)
        {
            this.UsedItem = usedItem;
        }
    }
}
