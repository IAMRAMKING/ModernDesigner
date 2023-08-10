using System.Collections.Generic;

namespace ModernDesigner.Serialization
{
    internal class ReferencedItem
    {
        internal string Key { get; set; }
        internal IList<ComponentProperty> Properties { get; private set; } = new List<ComponentProperty>();
    }
}
