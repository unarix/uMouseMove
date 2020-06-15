namespace uMouseMove
{
    partial class frmTimer
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTimer));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.timerMouseMove = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssConfig = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslMousePosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslExecutionStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerCheckExecution = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmStart = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmStop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSilentMode = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(167, 13);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 28);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(389, 13);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 28);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // timerMouseMove
            // 
            this.timerMouseMove.Interval = 2000;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssConfig,
            this.tsslMousePosition,
            this.tsslExecutionStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 54);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(652, 25);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssConfig
            // 
            this.tssConfig.Image = global::uMouseMove.Properties.Resources.ico114;
            this.tssConfig.Name = "tssConfig";
            this.tssConfig.Size = new System.Drawing.Size(82, 20);
            this.tssConfig.Text = "Settings";
            this.tssConfig.ToolTipText = "Settings";
            this.tssConfig.Click += new System.EventHandler(this.tssConfig_Click);
            // 
            // tsslMousePosition
            // 
            this.tsslMousePosition.Name = "tsslMousePosition";
            this.tsslMousePosition.Size = new System.Drawing.Size(0, 20);
            this.tsslMousePosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslExecutionStatus
            // 
            this.tsslExecutionStatus.Name = "tsslExecutionStatus";
            this.tsslExecutionStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tsslExecutionStatus.Size = new System.Drawing.Size(555, 20);
            this.tsslExecutionStatus.Spring = true;
            this.tsslExecutionStatus.Text = "Status Message";
            this.tsslExecutionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "uMouseMove";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // contextMenu
            // 
            this.contextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmOpen,
            this.tsmSettings,
            this.tsmSilentMode,
            this.toolStripSeparator1,
            this.tsmStart,
            this.tsmStop,
            this.toolStripSeparator2,
            this.aboutToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenu.ShowCheckMargin = true;
            this.contextMenu.ShowImageMargin = false;
            this.contextMenu.Size = new System.Drawing.Size(211, 188);
            // 
            // tsmOpen
            // 
            this.tsmOpen.Name = "tsmOpen";
            this.tsmOpen.Size = new System.Drawing.Size(210, 24);
            this.tsmOpen.Text = "Open";
            this.tsmOpen.ToolTipText = "Open main panel";
            this.tsmOpen.Click += new System.EventHandler(this.tsmOpen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(207, 6);
            // 
            // tsmStart
            // 
            this.tsmStart.CheckOnClick = true;
            this.tsmStart.Name = "tsmStart";
            this.tsmStart.Size = new System.Drawing.Size(210, 24);
            this.tsmStart.Text = "Start";
            this.tsmStart.ToolTipText = "Start process";
            this.tsmStart.Click += new System.EventHandler(this.tsmStart_Click);
            // 
            // tsmStop
            // 
            this.tsmStop.CheckOnClick = true;
            this.tsmStop.Name = "tsmStop";
            this.tsmStop.Size = new System.Drawing.Size(210, 24);
            this.tsmStop.Text = "Stop";
            this.tsmStop.ToolTipText = "Stop process";
            this.tsmStop.Click += new System.EventHandler(this.tsmStop_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(207, 6);
            // 
            // tsmSettings
            // 
            this.tsmSettings.Name = "tsmSettings";
            this.tsmSettings.Size = new System.Drawing.Size(210, 24);
            this.tsmSettings.Text = "Settings";
            this.tsmSettings.ToolTipText = "Open setting panel";
            this.tsmSettings.Click += new System.EventHandler(this.tsmSettings_Click);
            // 
            // tsmSilentMode
            // 
            this.tsmSilentMode.CheckOnClick = true;
            this.tsmSilentMode.Name = "tsmSilentMode";
            this.tsmSilentMode.Size = new System.Drawing.Size(210, 24);
            this.tsmSilentMode.Text = "Silent mode";
            this.tsmSilentMode.ToolTipText = "Do not show any notification";
            this.tsmSilentMode.Click += new System.EventHandler(this.tsmSilentMode_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // frmTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 79);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "frmTimer";
            this.Opacity = 0.8D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "uMouseMove";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Timer timerMouseMove;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslExecutionStatus;
        private System.Windows.Forms.Timer timerCheckExecution;
        private System.Windows.Forms.ToolStripStatusLabel tssConfig;
        private System.Windows.Forms.ToolStripStatusLabel tsslMousePosition;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmStart;
        private System.Windows.Forms.ToolStripMenuItem tsmStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmSettings;
        private System.Windows.Forms.ToolStripMenuItem tsmSilentMode;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

