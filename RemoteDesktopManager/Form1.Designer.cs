
namespace RemoteDesktopManager
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.a = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.start = new System.Windows.Forms.ToolStripMenuItem();
            this.stop = new System.Windows.Forms.ToolStripMenuItem();
            this.a.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.ContextMenuStrip = this.a;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // a
            // 
            this.a.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.start,
            this.stop});
            this.a.Name = "a";
            this.a.Size = new System.Drawing.Size(181, 70);
            // 
            // start
            // 
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(180, 22);
            this.start.Text = "连接";
            // 
            // stop
            // 
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(180, 22);
            this.stop.Text = "断开";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.a.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ContextMenuStrip a;
        private System.Windows.Forms.ToolStripMenuItem start;
        private System.Windows.Forms.ToolStripMenuItem stop;
    }
}

