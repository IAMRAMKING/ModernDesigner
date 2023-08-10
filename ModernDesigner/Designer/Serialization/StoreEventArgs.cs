using System.ComponentModel;
using System.ComponentModel.Design;

namespace ModernDesigner.Serialization
{
    public class StoreEventArgs : ComponentEventArgs
    {
        public StoreEventArgs(IComponent component) : base(component)
        {
        }

        public bool Cancel { get; set; }
    }
}
