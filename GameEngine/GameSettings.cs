namespace GameEngine
{
    /// <summary>
    /// Настройки
    /// </summary>
    public static class GameSettings
    {
        /// <summary>
        /// Полноэкранный режим
        /// </summary>
        public static bool FullScreenMode = false;
        /// <summary>
        /// Отрисовка отсвещения
        /// </summary>
        public static bool RenderLight = false;
        /// <summary>
        /// Отрисовка отладки
        /// </summary>
        public static bool RenderDebug = true;
        /// <summary>
        /// Общая громкость
        /// </summary>
        public static byte GlobalVolume = 100;
        /// <summary>
        /// Режим отладки
        /// </summary>
        public static bool DevMode = true;

        public static bool MultiThreadLight = false;
        public static bool VerticalSync = true;

        public static bool LightHQuality = false;

        public static bool SaveLogs = false;

        public static string ToString()
        {
            return $"\r\nSettings : \r\n" +
                $"FullScreenMode = {FullScreenMode.ToString()};\r\n" +
                $"RenderLight = {RenderLight.ToString()};\r\n" +
                $"RenderDebug = {RenderDebug.ToString()};\r\n" +
                $"GlobalVolume = {GlobalVolume.ToString()};\r\n" +
                $"DevMode = {DevMode.ToString()};\r\n" +
                $"MultiThreadLight = {MultiThreadLight.ToString()};\r\n" +
                $"VerticalSync = {VerticalSync.ToString()};\r\n" +
                $"LightHQuality = {LightHQuality.ToString()};\r\n" +
                $"SaveLogs = {SaveLogs.ToString()}.\r\n";
        }
    }
}
