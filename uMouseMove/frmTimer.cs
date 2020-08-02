using System;
using System.Drawing;
using System.Windows.Forms;
using uMouseMove.Entities;
using uMouseMove.Entities.Helpers;

namespace uMouseMove
{
    public partial class frmTimer : Form
    {
        bool ScheduledStartMode = false;
        bool firstTimeScheduledExec = false;
        bool showNotification = true;
        Form harryInvisibilityCloak = null;
        Setting.MousePosition mousePosition;

        Schedule WorkingDaysSchedule { get; set; }
        Schedule LaunchTimeSchedule { get; set; }
        Setting Settings { get; set; }

        public frmTimer()
        {
            InitializeComponent();
        }

        #region Events

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                // Load schedules
                this.WorkingDaysSchedule = Schedule.GetScheduleObject(SettingsManager.Get(Schedule.ScheduleTypes.WorkingDays));
                this.LaunchTimeSchedule = Schedule.GetScheduleObject(SettingsManager.Get(Schedule.ScheduleTypes.LaunchTime));
                this.Settings = Setting.GetSettingObject(SettingsManager.Get());

                if (this.Settings == null)
                    throw new ApplicationException("Please, configure the settings first.");

                // Disable controls
                this.btnStart.Enabled = false;
                this.btnStop.Enabled = true;

                // Set mouse position
                this.mousePosition = Settings.MousePositionControl;

                // Initialize timerCheckExecution
                this.timerCheckExecution.Enabled = true;
                this.timerCheckExecution.Tick += TimerCheckExecution_Tick;

                // Initialize timerMouseMove
                this.timerMouseMove.Enabled = false;
                this.timerMouseMove.Interval = 1000;
                this.timerMouseMove.Tick += TimerMouseMove_Tick;

                // Show status message
                this.ShowStatusMessage();

                // Show balloon
                this.ShowNotification("Starting...", $"{this.tsslExecutionStatus.Text} The app is running in hidden mode. Found me on try icon...");

                // Hide form
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;

                // Control menu state
                (this.contextMenu.Items["tsmStart"] as ToolStripMenuItem).Checked = true;
            }
            catch (Exception ex) { UIHelper.ShowMessage(ex.Message, MessageBoxIcon.Error); }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                // Stop timerMoveMouse
                this.timerMouseMove.Enabled = false;
                this.timerCheckExecution.Enabled = false;

                // Enable controls
                this.btnStart.Enabled = true;
                this.btnStop.Enabled = false;

                // Show Balloon
                this.ShowNotification("Stoping...", $"The proccess was stopped successfuly...");

                // Control menu state
                (this.contextMenu.Items["tsmStart"] as ToolStripMenuItem).Checked = true;
            }
            catch (Exception ex) { UIHelper.ShowMessage(ex.Message, MessageBoxIcon.Error); }
        }

        private void TimerCheckExecution_Tick(object sender, EventArgs e)
        {
            try
            {
                // Set timerCheckExecution interval
                this.timerCheckExecution.Interval = 1000; // 1 minute per tick

                // Get schedule
                this.ScheduledStartMode = this.Settings.ScheduledStartMode;
                this.firstTimeScheduledExec = this.Settings.ScheduledStartMode;

                // Check if must start and work away
                if (this.ScheduledStartMode)
                {
                    // Evaluate if the conditions are ideal to initialize the process
                    bool workTime = this.WorkingDaysSchedule.DoProcess();
                    bool launchTime = this.LaunchTimeSchedule.DoProcess();
                    if (workTime && !launchTime)
                        this.timerMouseMove.Enabled = true;
                    else
                        this.timerMouseMove.Enabled = false;

                    // Show status message
                    this.ShowStatusMessage(workTime, launchTime);
                }
                else
                    this.timerMouseMove.Enabled = true;
            }
            catch (Exception ex) { UIHelper.ShowMessage(ex.Message, MessageBoxIcon.Error); }
        }

        private void TimerMouseMove_Tick(object sender, EventArgs e)
        {
            try
            {
                // Starting to move the mouse
                this.MoveCursor();
            }
            catch (Exception) { throw; }
        }

        private void tssConfig_Click(object sender, EventArgs e)
        {
            try
            {
                frmConfig ofrmConfig = new frmConfig();
                ofrmConfig.ShowDialog();
                ofrmConfig.Dispose();
            }
            catch (Exception ex) { UIHelper.ShowMessage(ex.Message, MessageBoxIcon.Error); }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmAbout ofrmAbout = new frmAbout() {
                    StartPosition = FormStartPosition.CenterScreen,
                    WindowState = FormWindowState.Normal,
                    TopMost = true
                };
                ofrmAbout.ShowDialog();
                ofrmAbout.Dispose();
                ofrmAbout.Close();
            }
            catch (Exception ex) { UIHelper.ShowMessage(ex.Message, MessageBoxIcon.Error); }
        }

        #endregion

        #region NotifyIcon

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Right) return;

                this.contextMenu.Show(Control.MousePosition);
            }
            catch (Exception ex) { UIHelper.ShowMessage(ex.Message, MessageBoxIcon.Error); }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                // Show form
                this.WindowState = FormWindowState.Normal;
            }
            catch (Exception ex) { UIHelper.ShowMessage(ex.Message, MessageBoxIcon.Error); }
        }

        #endregion

        #region ContextMenu

        private void tsmOpen_Click(object sender, EventArgs e)
        {
            try
            {
                // Show form
                this.WindowState = FormWindowState.Normal;
            }
            catch (Exception ex) { UIHelper.ShowMessage(ex.Message, MessageBoxIcon.Error); }
        }

        private void tsmStart_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnStart.PerformClick();
                (this.contextMenu.Items["tsmStop"] as ToolStripMenuItem).Checked = false;
            }
            catch (Exception ex) { UIHelper.ShowMessage(ex.Message, MessageBoxIcon.Error); }
        }

        private void tsmStop_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnStop.PerformClick();
                (this.contextMenu.Items["tsmStart"] as ToolStripMenuItem).Checked = false;
            }
            catch (Exception ex) { UIHelper.ShowMessage(ex.Message, MessageBoxIcon.Error); }
        }

        private void tsmSettings_Click(object sender, EventArgs e)
        {
            try
            {
                this.tssConfig.PerformClick();
            }
            catch (Exception ex) { UIHelper.ShowMessage(ex.Message, MessageBoxIcon.Error); }
        }

        private void tsmSilentMode_Click(object sender, EventArgs e)
        {
            try
            {
                this.showNotification = !this.showNotification;
                (this.contextMenu.Items["tsmSilentMode"] as ToolStripMenuItem).Checked = this.showNotification ? false : true;
            }
            catch (Exception ex) { UIHelper.ShowMessage(ex.Message, MessageBoxIcon.Error); }
        }

        #endregion

        #region Methods

        private void MoveCursor()
        {
            try
            {
                // Generate random position
                Random r = new Random();
                int rInt = r.Next(1000, 2000); //for ints
                int range = 150;
                double dPosicion = r.NextDouble() * range;
                int iPosicion = (int)dPosicion;
                bool needMove = false;

                // Set cursor position depending of preferences
                Point newPosition = Cursor.Position;
                switch (mousePosition)
                {
                    case Setting.MousePosition.Top:
                        newPosition = new Point(iPosicion, Screen.PrimaryScreen.Bounds.Top);
                        needMove = Cursor.Position.Y <= Screen.PrimaryScreen.Bounds.Top + 1;
                        break;
                    case Setting.MousePosition.Left:
                        newPosition = new Point(Screen.PrimaryScreen.Bounds.Left, iPosicion);
                        needMove = Cursor.Position.X <= Screen.PrimaryScreen.Bounds.Left + 1;
                        break;
                    case Setting.MousePosition.Right:
                        newPosition = new Point(Screen.PrimaryScreen.Bounds.Right, iPosicion);
                        needMove = Cursor.Position.X >= Screen.PrimaryScreen.Bounds.Right - 1;
                        break;
                    case Setting.MousePosition.Bottom:
                        newPosition = new Point(iPosicion, Screen.PrimaryScreen.Bounds.Bottom);
                        needMove = Cursor.Position.Y >= Screen.PrimaryScreen.Bounds.Bottom - 1;
                        break;
                    default:
                        newPosition = new Point(0, 0);
                        break;
                }

                // If it is the first time that scheduled start mode starts, put the mouse at position zero automatically
                if (ScheduledStartMode && this.firstTimeScheduledExec)
                {
                    MouseOperations.SetCursorPosition(newPosition.X, newPosition.Y);
                    this.firstTimeScheduledExec = false;
                }

                // Avoid do click over controls
                this.ShowHarryInvisibilityCloak(needMove && this.Settings.HarryInvisibilityCloak);

                // Evaluate if is neccesary move the mouse
                if (needMove)
                {
                    // Change mouse position emulating working
                    MouseOperations.SetCursorPosition(newPosition.X, newPosition.Y);
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
                }
            }
            catch (Exception) { throw; }
        }

        private void ShowHarryInvisibilityCloak(bool show)
        {
            try
            {
                if (show)
                {
                    // Create transparent form to avoid do click over important controls
                    this.harryInvisibilityCloak = harryInvisibilityCloak ?? new Form()
                    {
                        FormBorderStyle = FormBorderStyle.None,
                        BackColor = Color.Black,
                        Opacity = Convert.ToDouble(this.Settings.HarryInvisibilityCloakOpacity) / 100,
                        WindowState = FormWindowState.Maximized,
                        TopMost = true,
                        StartPosition = FormStartPosition.CenterScreen,
                        ShowInTaskbar = false
                    };

                    if (!this.harryInvisibilityCloak.Visible)
                        this.harryInvisibilityCloak.Show();

                    return;
                }

                if (this.harryInvisibilityCloak != null)
                {
                    this.harryInvisibilityCloak.Close();
                    this.harryInvisibilityCloak.Dispose();
                    this.harryInvisibilityCloak = null;
                }
            }
            catch (Exception) { throw; }
        }

        private void ShowStatusMessage(bool workTime = false, bool launchTime = false)
        {
            try
            {
                // Show status message
                this.tsslExecutionStatus.Text = workTime && !launchTime ? "Hey! I'm working for you!" :
                                                launchTime ? "Hey! I'm going to launch right now!" : "Hey! I'am waiting to enter in action...";

                // Show mouse position
                this.tsslMousePosition.Text = $"Mouse position is set to: {this.Settings.MousePositionControl.ToString()}";
            }
            catch (Exception) { throw; }
        }

        private void ShowNotification(string title, string msg)
        {
            try
            {
                if (this.showNotification)
                    this.notifyIcon.ShowBalloonTip(10000, title, msg, ToolTipIcon.Info);
            }
            catch (Exception) { throw; }
        }

        #endregion
    }
}
