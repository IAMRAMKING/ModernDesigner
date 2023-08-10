using System;
using System.Windows.Forms;
using ModernDesigner.Serialization;

namespace ModernDesigner
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DesignerControl : UserControl
    {
        public Control DesignedForm
        {
            get { return this.Designer.DesignedForm; }
            private set { this.Designer.DesignedForm = value; }
        }

        public IDesignerLoader DesignerLoader { get => this.Designer?.DesignerLoader; }

        public Designer Designer { get => this.designer; }

        #region Constructor

        public DesignerControl()
        {
            InitializeComponent();
        }

        public DesignerControl(Control root) : this()
        {
            this.DesignedForm = root;
        }

        public DesignerControl(Control root, string layoutXml) : this()
        {
            this.DesignedForm = root;
            this.Designer.LayoutXML = layoutXml;
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            this.Designer.DesignContainer = this;
            this.Dock = DockStyle.Fill;
            if (this.DesignedForm != null)
            {
                this.Designer.Active = true;
            }
        }

        #region The designer automatically generates code

        /// <summary> 
        /// Need designer variables。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up all the resources you are using。
        /// </summary>
        /// <param name="disposing">If you should release the hosting resources，for true；Otherwise false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary> 
        /// The design of the designer supports - Don't modify
        /// Use code editor to modify the content of this method。
        /// </summary>
        private void InitializeComponent()
        {
            this.defaultDesignerLoader = new DefaultDesignerLoader();
            this.designer = new Designer();
            this.SuspendLayout();
            // 
            // designer
            // 
            this.designer.DesignerLoader = this.defaultDesignerLoader;
            this.designer.GridSize = new System.Drawing.Size(8, 8);
            // 
            // DesignerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Name = "DesignerControl";
            this.Size = new System.Drawing.Size(339, 374);
            this.ResumeLayout(false);

        }

        private DefaultDesignerLoader defaultDesignerLoader;
        private Designer designer;

        #endregion
    }
}
