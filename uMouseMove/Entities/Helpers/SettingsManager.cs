using System;

namespace uMouseMove.Entities.Helpers
{
    static class SettingsManager
    {
        /// <summary>
        /// Save settings
        /// </summary>
        /// <param name="json"></param>
        public static void Save(string json)
        {
            try
            {
                Properties.Settings.Default.Setting = json;
                Properties.Settings.Default.Save();
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Save serialized object
        /// </summary>
        /// <param name="json">Serialized object as Json</param>
        public static void Save(Schedule.ScheduleTypes scheduleTypes, string json)
        {
            try
            {
                switch (scheduleTypes)
                {
                    case Schedule.ScheduleTypes.WorkingDays:
                        Properties.Settings.Default.WorkingDaySchedule = json;
                        break;
                    case Schedule.ScheduleTypes.LaunchTime:
                        Properties.Settings.Default.LaunchTimeSchedule = json;
                        break;
                    default:
                        throw new ArgumentException($"This argument {scheduleTypes.ToString()} is not implemented yet!");
                }

                Properties.Settings.Default.Save();
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Get settings
        /// </summary>
        /// <returns>Serialized object as Json</returns>
        public static string Get()
        {
            try
            {
                Properties.Settings.Default.Reload();
                return Properties.Settings.Default.Setting;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Get serialized object
        /// </summary>
        /// <returns>Serialized object as Json</returns>
        public static string Get(Schedule.ScheduleTypes scheduleTypes)
        {
            try
            {
                Properties.Settings.Default.Reload();

                switch (scheduleTypes)
                {
                    case Schedule.ScheduleTypes.WorkingDays:
                        return Properties.Settings.Default.WorkingDaySchedule;
                    case Schedule.ScheduleTypes.LaunchTime:
                        return Properties.Settings.Default.LaunchTimeSchedule;
                    default:
                        throw new ArgumentException($"This argument {scheduleTypes.ToString()} is not implemented yet!");
                }
            }
            catch (Exception) { throw; }
        }
    }
}
