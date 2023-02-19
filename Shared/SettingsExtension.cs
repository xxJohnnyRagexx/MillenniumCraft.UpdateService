namespace Shared
{
    public static class SettingsExtension
    {
        public static string BuildDatabaseConnectionString(string filename, string connectionMode)
        {
            var path = Path.Combine(getUserHomeDir(), filename);
            return string.Format($"Filename={path};Connection={connectionMode}");
        }

        private static string getUserHomeDir()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }
    }
}
