using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ModernDesigner.Toolbox
{
    public class ToolboxCategoryCollection : ReadOnlyCollection<ToolboxCategoryItem>
    {
        public ToolboxCategoryCollection(IList<ToolboxCategoryItem> list) : base(list)
        {
        }
    }

}
