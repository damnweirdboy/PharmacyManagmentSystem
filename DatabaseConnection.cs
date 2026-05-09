using System.Configuration;

namespace PharmacyManagementSystem.Data
{
    /// <summary>
    /// Provides the ADO.NET connection string from App.config.
    /// Used by AdminLoginForm, SalesForm, and ReportsForm.
    /// </summary>
    public static class DatabaseConnection
    {
        /// <summary>
        /// Returns the connection string named "PharmacyDBConnection" from App.config.
        /// </summary>
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["PharmacyDBConnection"].ConnectionString;
        }
    }
}
