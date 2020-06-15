using System;

using uMouseMove.Entities.Helpers;

namespace uMouseMove.Entities
{
    public class Setting
    {
        #region Enums
        public enum MousePosition { Top, Left, Right, Bottom }
        #endregion

        #region Properties
        public MousePosition MousePositionControl { get; set; }
        public bool ScheduledStartMode { get; set; }
        public bool HarryInvisibilityCloak { get; set; }
        public double HarryInvisibilityCloakOpacity { get; set; }
        #endregion

        #region Constructors
        public Setting()
        {
            this.MousePositionControl = MousePosition.Top;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get an settings instance from Json
        /// </summary>
        /// <param name="Json">Object as json</param>
        /// <returns>Object instance</returns>
        public static Setting GetSettingObject(string Json)
        {
            try
            {
                // Get object from json
                return JsonSerializer.JsonToObject<Setting>(Json);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Get Json from a settings instance
        /// </summary>
        /// <returns>Current object converted to Json</returns>
        public string GetSettingJson()
        {
            try
            {
                // Get json from object
                return JsonSerializer.ObjectToJson(this);
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
