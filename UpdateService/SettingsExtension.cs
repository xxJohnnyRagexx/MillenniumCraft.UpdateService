namespace UpdateService
{
    public static class SettingsExtension
    {
        public static string BuildDatabaseConnectionString(string path, string connectionMode)
        {
            return string.Format($"Filename={path};Connection={connectionMode}");
        }
    }
}
