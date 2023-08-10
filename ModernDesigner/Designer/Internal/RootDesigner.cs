using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using ModernDesigner.Services;

namespace ModernDesigner.Internal
{
    internal class RootDesigner : DocumentDesigner
    {
        protected override void OnCreateHandle()
        {
            base.OnCreateHandle();
            var eventFilter = (EventService)this.GetService(typeof(EventService));
            eventFilter.KeyDown += EventFilter_KeyDown;
            eventFilter.KeyDown += this.EventFilter_KeyDown;
        }
        protected override void Dispose(bool disposing)
        {
            var eventFilter = (EventService)this.GetService(typeof(EventService));
            if (eventFilter != null)
            {
                eventFilter.KeyDown -= this.EventFilter_KeyDown;
            }
            base.Dispose(disposing);
        }

        private void EventFilter_KeyDown(object sender, KeyEventArgs e)
        {
            var designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
            if (e.KeyValue == 9) //Tab
            {
                designerHost.SelectNextControl((e.Modifiers & Keys.Shift) == Keys.None);
            }
            else
            {
                bool ctrlFlag = false;
                if (this.GetService(typeof(Designer)) is Designer designer && !designer.SnapToGrid)
                {
                    ctrlFlag = true;
                }
                if ((e.Modifiers & Keys.Control) != Keys.None)
                {
                    ctrlFlag = !ctrlFlag;
                }
                int x = 0, y = 0;
                switch (e.KeyValue)
                {
                    case 37: // Left
                    case 39: // right
                        x = ctrlFlag ? 1 : GridSize.Width;
                        if (e.KeyValue == 37) x = -x;
                        break;
                    case 38: // superior
                    case 40: // Down
                        y = ctrlFlag ? 1 : GridSize.Height;
                        if (e.KeyValue == 38) y = -y;
                        break;
                    default:
                        return;
                }
                if ((e.Modifiers & Keys.Shift) != Keys.None)
                {
                    designerHost.Layout("Resize controls", control =>
                    {
                        if (x != 0) control.Width += x;
                        if (y != 0) control.Height += y;
                    });
                }
                else
                {
                    designerHost.Layout("Move controls", control =>
                    {
                        if (x != 0) control.Left += x;
                        if (y != 0) control.Top += y;
                    });
                }
            }
        }

    }
}
