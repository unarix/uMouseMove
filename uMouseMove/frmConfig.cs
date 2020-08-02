using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using uMouseMove.Entities.Helpers;
using uMouseMove.Entities;

namespace uMouseMove
{
    public partial class frmConfig : Form
    {
        public Schedule WorkingDaysSchedule { get; set; }
        public Schedule LaunchTimeSchedule { get; set; }
        public Setting Settings { get; set; }

        public frmConfig()
        {
            InitializeComponent();
        }

        #region Events

        private void frmConfig_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadSchedule();
                this.SetForm();
                this.SetToolTips();
            }
            catch (Exception ex) { UIHelper.ShowMessage(ex.Message, MessageBoxIcon.Error); }
        }

        private void chkWD_RandomTime_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.EnableRandomControls((sender as CheckBox).Checked, Schedule.ScheduleTypes.WorkingDays);
            }
            catch (Exception) { throw; }
        }

        private void chkWD_RandomStartTimeBefore_CheckedChanged(object sender, EventArgs e)
        {
            this.ChangeRandomBeforeLabel(this.lblWD_RandomStartTimeBefore, (CheckBox)sender);
        }

        private void chkWD_RandomEndTimeBefore_CheckedChanged(object sender, EventArgs e)
        {
            this.ChangeRandomBeforeLabel(this.lblWD_RandomEndTimeBefore, (CheckBox)sender);
        }

        private void btnWD_RandomTest_Click(object sender, EventArgs e)
        {
            try
            {
                Schedule schedule = new Schedule(Schedule.ScheduleTypes.WorkingDays,
                                                 this.dtpWD_StartTime.Value,
                                                 this.dtpWD_EndTime.Value,
                                                 this.dtpWD_RandomStartTime.Value,
                                                 this.dtpWD_RandomEndTime.Value,
                                                 this.chkWD_RandomTime.Checked,
                                                 this.chkWD_RandomStartTimeBefore.Checked,
                                                 this.chkWD_RandomEndTimeBefore.Checked);

                // Get random times
                schedule.GenerateRandomTime(out DateTime startTime, out DateTime endTime);

                // Show result
                this.ShowTestMessage(startTime, endTime);
            }
            catch (Exception ex) { UIHelper.ShowMessage(ex.Message, MessageBoxIcon.Error); }
        }

        private void chkLT_RandomTime_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.EnableRandomControls((sender as CheckBox).Checked, Schedule.ScheduleTypes.LaunchTime);
            }
            catch (Exception) { throw; }
        }

        private void chkLT_RandomStartTimeBefore_CheckedChanged(object sender, EventArgs e)
        {
            this.ChangeRandomBeforeLabel(this.lblLT_RandomStartTimeBefore, (CheckBox)sender);
        }

        private void chkLT_RandomEndTimeBefore_CheckedChanged(object sender, EventArgs e)
        {
            this.ChangeRandomBeforeLabel(this.lblLT_RandomEndTimeBefore, (CheckBox)sender);
        }

        private void btnLT_RandomTest_Click(object sender, EventArgs e)
        {
            try
            {
                // Create new instance
                Schedule schedule = new Schedule(Schedule.ScheduleTypes.LaunchTime,
                                                 this.dtpLT_StartTime.Value,
                                                 this.dtpLT_EndTime.Value,
                                                 this.dtpLT_RandomStartTime.Value,
                                                 this.dtpLT_RandomEndTime.Value,
                                                 this.chkLT_RandomTime.Checked,
                                                 this.chkLT_RandomStartTimeBefore.Checked,
                                                 this.chkLT_RandomEndTimeBefore.Checked);

                // Get random times
                schedule.GenerateRandomTime(out DateTime startTime, out DateTime endTime);

                // Show result
                this.ShowTestMessage(startTime, endTime);
            }
            catch (Exception ex) { UIHelper.ShowMessage(ex.Message, MessageBoxIcon.Error); }
        }

        private void cbMC_MouseZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cbMC_MouseZone.SelectedItem.ToString().ToUpper() != this.Settings.MousePositionControl.ToString().ToUpper())
                {
                    switch (this.cbMC_MouseZone.SelectedItem.ToString().ToUpper())
                    {
                        case "TOP":
                            this.Settings.MousePositionControl = Setting.MousePosition.Top;
                            break;
                        case "LEFT":
                            this.Settings.MousePositionControl = Setting.MousePosition.Left;
                            break;
                        case "RIGHT":
                            this.Settings.MousePositionControl = Setting.MousePosition.Right;
                            break;
                        case "BOTTOM":
                            this.Settings.MousePositionControl = Setting.MousePosition.Bottom;
                            break;
                        default:
                            throw new ArgumentException($"Argument '{this.cbMC_MouseZone.SelectedItem}' is not implemented yet!");
                    }
                }
            }
            catch (Exception) { throw; }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveSchedule();
                this.SaveSettings();
                this.Close();
                this.Dispose();
            }
            catch (Exception ex) { UIHelper.ShowMessage(ex.Message, MessageBoxIcon.Error); }
        }

        private void SaveSettings()
        {
            try
            {
                this.Settings.ScheduledStartMode = this.rbMC_ScheduledStartMode.Checked;
                this.Settings.HarryInvisibilityCloak = this.chkMC_HarryInvisibilityCloak.Checked;
                this.Settings.HarryInvisibilityCloakOpacity = Convert.ToDouble(this.nudMC_HarryInvisibilityCloakOpacity.Value);
                SettingsManager.Save(JsonSerializer.ObjectToJson(this.Settings));
            }
            catch (Exception) { throw; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        #endregion

        #region Methods

        private void SaveSchedule()
        {
            try
            {
                #region Set WorkingDays controls
                // Set WorkingDays
                this.WorkingDaysSchedule.wdList.Clear();

                if (chkWD_Monday.Checked)
                    this.WorkingDaysSchedule.wdList.Add(DayOfWeek.Monday);
                if (chkWD_Tuesday.Checked)
                    this.WorkingDaysSchedule.wdList.Add(DayOfWeek.Tuesday);
                if (chkWD_Wednesday.Checked)
                    this.WorkingDaysSchedule.wdList.Add(DayOfWeek.Wednesday);
                if (chkWD_Thursday.Checked)
                    this.WorkingDaysSchedule.wdList.Add(DayOfWeek.Thursday);
                if (chkWD_Friday.Checked)
                    this.WorkingDaysSchedule.wdList.Add(DayOfWeek.Friday);
                if (chkWD_Saturday.Checked)
                    this.WorkingDaysSchedule.wdList.Add(DayOfWeek.Saturday);
                if (chkWD_Sunday.Checked)
                    this.WorkingDaysSchedule.wdList.Add(DayOfWeek.Sunday);

                // Start and End time
                this.WorkingDaysSchedule.StartTime = this.dtpWD_StartTime.Value;
                this.WorkingDaysSchedule.EndTime = this.dtpWD_EndTime.Value;

                // Random Time
                this.WorkingDaysSchedule.RandomTime = this.chkWD_RandomTime.Checked;
                this.WorkingDaysSchedule.RandomTime_StartTime = this.dtpWD_RandomStartTime.Value;
                this.WorkingDaysSchedule.RandomTime_EndTime = this.dtpWD_RandomEndTime.Value;
                this.WorkingDaysSchedule.RandomStartTimeBefore = this.chkWD_RandomStartTimeBefore.Checked;
                this.WorkingDaysSchedule.RandomEndTimeBefore = this.chkWD_RandomEndTimeBefore.Checked;
                #endregion

                #region Set LaunchTime controls

                // Set Start and End time
                this.LaunchTimeSchedule.StartTime = this.dtpLT_StartTime.Value;
                this.LaunchTimeSchedule.EndTime = this.dtpLT_EndTime.Value;

                // Set Random time
                this.LaunchTimeSchedule.RandomTime = this.chkLT_RandomTime.Checked;
                this.LaunchTimeSchedule.RandomTime_StartTime = this.dtpLT_RandomStartTime.Value;
                this.LaunchTimeSchedule.RandomTime_EndTime = this.dtpLT_RandomEndTime.Value;
                this.LaunchTimeSchedule.RandomStartTimeBefore = this.chkLT_RandomStartTimeBefore.Checked;
                this.LaunchTimeSchedule.RandomEndTimeBefore = this.chkLT_RandomEndTimeBefore.Checked;

                #endregion

                SettingsManager.Save(Schedule.ScheduleTypes.WorkingDays, WorkingDaysSchedule.GetScheduleJson());
                SettingsManager.Save(Schedule.ScheduleTypes.LaunchTime, LaunchTimeSchedule.GetScheduleJson());
            }
            catch (Exception) { throw; }
        }

        private void LoadSchedule()
        {
            try
            {
                // Load schedule
                this.WorkingDaysSchedule = Schedule.GetScheduleObject(SettingsManager.Get(Schedule.ScheduleTypes.WorkingDays));
                this.LaunchTimeSchedule = Schedule.GetScheduleObject(SettingsManager.Get(Schedule.ScheduleTypes.LaunchTime));
                this.WorkingDaysSchedule = this.WorkingDaysSchedule ?? new Schedule(Schedule.ScheduleTypes.WorkingDays);
                this.LaunchTimeSchedule = this.LaunchTimeSchedule ?? new Schedule(Schedule.ScheduleTypes.LaunchTime);
                this.Settings = Setting.GetSettingObject(SettingsManager.Get()) ?? new Setting();

                #region Set WorkingDays controls

                // Set WorkingDays
                foreach (DayOfWeek day in this.WorkingDaysSchedule.wdList)
                {
                    switch (day)
                    {
                        case DayOfWeek.Sunday:
                            chkWD_Sunday.Checked = true;
                            break;
                        case DayOfWeek.Monday:
                            chkWD_Monday.Checked = true;
                            break;
                        case DayOfWeek.Tuesday:
                            chkWD_Tuesday.Checked = true;
                            break;
                        case DayOfWeek.Wednesday:
                            chkWD_Wednesday.Checked = true;
                            break;
                        case DayOfWeek.Thursday:
                            chkWD_Thursday.Checked = true;
                            break;
                        case DayOfWeek.Friday:
                            chkWD_Friday.Checked = true;
                            break;
                        case DayOfWeek.Saturday:
                            chkWD_Saturday.Checked = true;
                            break;
                    }
                }

                // Start and End time
                this.dtpWD_StartTime.Value = this.WorkingDaysSchedule.StartTime;
                this.dtpWD_EndTime.Value = this.WorkingDaysSchedule.EndTime;

                // Random Time
                this.chkWD_RandomTime.Checked = this.WorkingDaysSchedule.RandomTime;
                this.dtpWD_RandomStartTime.Value = this.WorkingDaysSchedule.RandomTime_StartTime;
                this.dtpWD_RandomEndTime.Value = this.WorkingDaysSchedule.RandomTime_EndTime;
                this.chkWD_RandomStartTimeBefore.Checked = this.WorkingDaysSchedule.RandomStartTimeBefore;
                this.chkWD_RandomEndTimeBefore.Checked = this.WorkingDaysSchedule.RandomEndTimeBefore;
                #endregion

                #region Set LaunchTime controls

                // Set Start and End time
                this.dtpLT_StartTime.Value = this.LaunchTimeSchedule.StartTime;
                this.dtpLT_EndTime.Value = this.LaunchTimeSchedule.EndTime;

                // Set Random time
                this.chkLT_RandomTime.Checked = this.LaunchTimeSchedule.RandomTime;
                this.dtpLT_RandomStartTime.Value = this.LaunchTimeSchedule.RandomTime_StartTime;
                this.dtpLT_RandomEndTime.Value = this.LaunchTimeSchedule.RandomTime_EndTime;
                this.chkLT_RandomStartTimeBefore.Checked = this.LaunchTimeSchedule.RandomStartTimeBefore;
                this.chkLT_RandomEndTimeBefore.Checked = this.LaunchTimeSchedule.RandomEndTimeBefore;

                #endregion

                #region Set StartMode

                this.rbMC_ScheduledStartMode.Checked = this.Settings.ScheduledStartMode;
                this.rbMC_ManualStartMode.Checked = !this.Settings.ScheduledStartMode;

                #endregion

                #region Set HarryInvisibilityCloak

                this.chkMC_HarryInvisibilityCloak.Checked = this.Settings.HarryInvisibilityCloak;
                this.nudMC_HarryInvisibilityCloakOpacity.Value = this.Settings.HarryInvisibilityCloakOpacity == 0 ?
                                                                 1 : Convert.ToDecimal(this.Settings.HarryInvisibilityCloakOpacity);

                #endregion
            }
            catch (Exception) { throw; }
        }

        private void SetForm()
        {
            try
            {
                // Set random controls
                this.EnableRandomControls(this.chkWD_RandomTime.Checked, Schedule.ScheduleTypes.WorkingDays);
                this.EnableRandomControls(this.chkLT_RandomTime.Checked, Schedule.ScheduleTypes.LaunchTime);

                // Default date
                DateTime defaultDate = DateTime.Now.Date;

                // Working days random time
                this.dtpWD_RandomStartTime.Value = this.chkWD_RandomTime.Checked ? this.dtpWD_RandomStartTime.Value : defaultDate;
                this.dtpWD_RandomEndTime.Value = this.chkWD_RandomTime.Checked ? this.dtpWD_RandomEndTime.Value : defaultDate;

                // Launch random time
                this.dtpLT_RandomStartTime.Value = this.chkLT_RandomTime.Checked ? this.dtpLT_RandomStartTime.Value : defaultDate;
                this.dtpLT_RandomEndTime.Value = this.chkLT_RandomTime.Checked ? this.dtpLT_RandomEndTime.Value : defaultDate;

                // Add items to Muse zone control
                List<string> options = new List<string>() { "Top", "Left", "Right", "Bottom" };
                options.ForEach(x => this.cbMC_MouseZone.Items.Add(x));
                this.cbMC_MouseZone.SelectedItem = this.Settings.MousePositionControl.ToString();
            }
            catch (Exception) { throw; }
        }

        private void EnableRandomControls(bool state, Schedule.ScheduleTypes scheduleType)
        {
            try
            {
                switch (scheduleType)
                {
                    case Schedule.ScheduleTypes.WorkingDays:
                        this.dtpWD_RandomStartTime.Enabled =
                        this.dtpWD_RandomEndTime.Enabled =
                        this.chkWD_RandomStartTimeBefore.Enabled =
                        this.chkWD_RandomEndTimeBefore.Enabled = state;
                        break;
                    case Schedule.ScheduleTypes.LaunchTime:
                        this.dtpLT_RandomStartTime.Enabled =
                        this.dtpLT_RandomEndTime.Enabled =
                        this.chkLT_RandomStartTimeBefore.Enabled =
                        this.chkLT_RandomEndTimeBefore.Enabled = state;
                        break;
                    default:
                        throw new InvalidEnumArgumentException();
                }
            }
            catch (Exception) { throw; }
        }

        private void ChangeRandomBeforeLabel(Label lblControl, CheckBox chkControl)
        {
            try
            {
                lblControl.Text = chkControl.Checked ? "Before" : "After";
            }
            catch (Exception) { throw; }
        }

        private void SetToolTips()
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(chkMC_HarryInvisibilityCloak, "Add a layer between desktop and cursor to avoid do click over shorcuts or controls");
        }

        private void ShowTestMessage(DateTime startTime, DateTime endTime) =>
            MessageBox.Show($"The schedule will be starting at {startTime} and ending at {endTime}", "Schedule", MessageBoxButtons.OK, MessageBoxIcon.Information);

        #endregion
    }
}
