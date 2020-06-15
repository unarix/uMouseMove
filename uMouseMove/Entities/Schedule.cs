using System;
using System.Collections.Generic;
using uMouseMove.Entities.Helpers;

namespace uMouseMove.Entities
{
    public class Schedule
    {
        #region Variables

        private static Random random;

        #endregion

        #region Enums
        public enum ScheduleTypes { WorkingDays, LaunchTime }
        #endregion

        #region Properties

        public ScheduleTypes ScheduleType { get; set; }
        public List<DayOfWeek> wdList { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool RandomTime { get; set; }
        public DateTime RandomTime_StartTime { get; set; }
        public DateTime RandomTime_EndTime { get; set; }
        public bool RandomStartTimeBefore { get; set; }
        public bool RandomEndTimeBefore { get; set; }

        #endregion

        #region Constructors

        private Schedule() { }

        /// <summary>
        /// Generate empty intance
        /// </summary>
        public Schedule(ScheduleTypes scheduleTypes)
        {
            this.ScheduleType = scheduleTypes;
            this.wdList = new List<DayOfWeek>();
            this.StartTime = DateTime.Now;
            this.EndTime = DateTime.Now;
            this.RandomTime_StartTime = DateTime.Now.Date;
            this.RandomTime_EndTime = DateTime.Now.Date;
            this.RandomTime = false;
        }

        /// <summary>
        /// Generate instance with data
        /// </summary>
        /// <param name="scheduleType">Set schedule type to assing times</param>
        /// <param name="startTime">Normal start time</param>
        /// <param name="endTime">Normal end time</param>
        /// <param name="randomStartTime">Random start time</param>
        /// <param name="randomEndTime">Random end time</param>
        /// <param name="randomTime">Enable random time</param>
        /// <param name="randomStartTimeBefore">Indicate if random start time will be added before or after normal start time</param>
        /// <param name="randomEndTimeBefore">Indicate if random end time will be added before or after normal end time</param>
        public Schedule(ScheduleTypes scheduleType,
                        DateTime startTime,
                        DateTime endTime,
                        DateTime randomStartTime,
                        DateTime randomEndTime,
                        bool randomTime,
                        bool randomStartTimeBefore,
                        bool randomEndTimeBefore) : this(scheduleType)
        {
            this.ScheduleType = scheduleType;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.RandomTime_StartTime = randomStartTime;
            this.RandomTime_EndTime = randomEndTime;
            this.RandomTime = randomTime;
            this.RandomStartTimeBefore = randomStartTimeBefore;
            this.RandomEndTimeBefore = randomEndTimeBefore;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get an schedule instance from Json
        /// </summary>
        /// <param name="Json">Object as json</param>
        /// <returns>Object instance</returns>
        public static Schedule GetScheduleObject(string Json)
        {
            try
            {
                // Get object from json
                return JsonSerializer.JsonToObject<Schedule>(Json);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Get Json from a shcedule instance
        /// </summary>
        /// <returns>Current object converted to Json</returns>
        public string GetScheduleJson()
        {
            try
            {
                // Get json from object
                return JsonSerializer.ObjectToJson(this);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Generate random time
        /// </summary>
        /// <param name="time">Start or end time</param>
        /// <param name="randomTime">Start or end random time</param>
        /// <param name="randomTimeBefore">Add random time before or after normal time</param>
        /// <returns></returns>
        public static DateTime GenerateRandomTime(DateTime time, DateTime randomTime, bool randomTimeBefore)
        {
            try
            {
                random = random ?? new Random();
                TimeSpan randomTimeSpan = new TimeSpan(randomTime.Hour,
                                                        randomTime.Minute,
                                                        randomTime.Second);

                int randomMinutes = random.Next(Convert.ToInt32(randomTimeSpan.TotalMinutes));
                return time.AddMinutes(randomTimeBefore ? -randomMinutes : randomMinutes);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Generate normal or random start and end time
        /// </summary>
        /// <param name="startTime">Returns generated normal or random start time</param>
        /// <param name="endTime">Returns generate normal or random end time</param>
        public void GenerateRandomTime(out DateTime startTime, out DateTime endTime)
        {
            try
            {
                // Validate all times
                this.ValidateTime();

                // Generate random times
                startTime = GenerateRandomTime(this.StartTime, this.RandomTime ? this.RandomTime_StartTime : default(DateTime), this.RandomStartTimeBefore);
                endTime = GenerateRandomTime(this.EndTime, this.RandomTime ? this.RandomTime_EndTime : default(DateTime), this.RandomEndTimeBefore);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Validate all required properties to return a valid time
        /// </summary>
        private void ValidateTime()
        {
            try
            {
                DateTime startTime = default(DateTime), endTime = default(DateTime);

                // Validation normal time
                if (this.StartTime > this.EndTime)
                    ThrowMessage();

                // Validate random time
                startTime = startTime + (this.RandomStartTimeBefore ? -this.RandomTime_StartTime.TimeOfDay : this.RandomTime_StartTime.TimeOfDay);
                endTime = endTime + (this.RandomEndTimeBefore ? -this.RandomTime_EndTime.TimeOfDay : this.RandomTime_EndTime.TimeOfDay);

                // Validation normal time
                if (startTime > endTime && this.RandomTime)
                    ThrowMessage("Random Time");

                // Throw local message exception
                void ThrowMessage(string msg = null) => throw new ApplicationException($"The end time ({endTime}) cannot occur before start time ({startTime}) for {this.ScheduleType.ToString()} {msg}");
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Execute all the logics to determine if the program continue working
        /// </summary>
        /// <returns></returns>
        public bool DoProcess()
        {
            try
            {
                // Set current time
                DateTime currentDate = DateTime.Now;

                // First Step - Evaluate working days
                if (!this.wdList.Exists(x => x == DateTime.Now.DayOfWeek) && this.ScheduleType == ScheduleTypes.WorkingDays)
                    return false;

                // Second Step - Evaluate Random Time
                if (!this.RandomTime)
                {
                    // Third Step - Evaluate time
                    if (currentDate.TimeOfDay >= this.StartTime.TimeOfDay && currentDate.TimeOfDay <= this.EndTime.TimeOfDay)
                        return true;
                    else
                        return false;
                }

                // Generate random times
                this.GenerateRandomTime(out DateTime startTime, out DateTime endTime);

                // Evaluate random time
                if (currentDate >= startTime && currentDate <= endTime)
                    return true;

                return false;
            }
            catch (Exception) { throw; }
        }

        #endregion
    }
}
