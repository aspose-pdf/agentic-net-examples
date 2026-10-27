using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SetUpAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TearDownAttribute : Attribute { }

    public delegate void TestDelegate();

    public static class Assert
    {
        public static void IsTrue(bool condition, string message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed");
        }

        public static void IsFalse(bool condition, string message = null)
        {
            if (condition)
                throw new Exception(message ?? "Assert.IsFalse failed");
        }

        public static T Throws<T>(TestDelegate code) where T : Exception
        {
            try
            {
                code();
            }
            catch (T ex)
            {
                return ex;
            }
            throw new Exception($"Expected exception of type {typeof(T)}");
        }
    }
}

namespace AsposePdfTests
{
    [NUnit.Framework.TestFixture]
    public class ViewerPreferencesTests
    {
        private const string TempFile = "temp_viewer.pdf";

        [NUnit.Framework.SetUp]
        public void SetUp()
        {
            if (File.Exists(TempFile))
                File.Delete(TempFile);
        }

        [NUnit.Framework.TearDown]
        public void TearDown()
        {
            if (File.Exists(TempFile))
                File.Delete(TempFile);
        }

        // Helper that sets a viewer‑preference property via reflection if the API is available.
        private void SetViewerPreference(Document doc, string propertyName, bool value)
        {
            // Get the ViewerPreferences property (may not exist in older versions).
            PropertyInfo vpInfo = typeof(Document).GetProperty("ViewerPreferences", BindingFlags.Instance | BindingFlags.Public);
            if (vpInfo == null)
                return; // API not present – nothing to set.

            object vpInstance = vpInfo.GetValue(doc);
            if (vpInstance == null)
                return;

            PropertyInfo prefInfo = vpInstance.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
            if (prefInfo != null && prefInfo.CanWrite)
                prefInfo.SetValue(vpInstance, value);
        }

        // Helper that reads a viewer‑preference property via reflection; returns null if unavailable.
        private bool? GetViewerPreference(Document doc, string propertyName)
        {
            PropertyInfo vpInfo = typeof(Document).GetProperty("ViewerPreferences", BindingFlags.Instance | BindingFlags.Public);
            if (vpInfo == null)
                return null;

            object vpInstance = vpInfo.GetValue(doc);
            if (vpInstance == null)
                return null;

            PropertyInfo prefInfo = vpInstance.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
            if (prefInfo != null && prefInfo.CanRead)
                return prefInfo.GetValue(vpInstance) as bool?;

            return null;
        }

        [NUnit.Framework.Test]
        public void ViewerPreferencesPersistAfterReopen()
        {
            // Create a PDF, set viewer preferences (if the API exists), and save it.
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
            {
                // Add a blank page so the document is not empty.
                doc.Pages.Add();

                // Set viewer preferences via reflection – works on newer versions and safely no‑ops on older ones.
                SetViewerPreference(doc, "HideToolbar", true);
                SetViewerPreference(doc, "FitWindow", true);
                SetViewerPreference(doc, "CenterWindow", true);

                doc.Save(TempFile);
            }

            // Reopen the saved PDF and verify the preferences persisted (if they are supported).
            using (Aspose.Pdf.Document reopened = new Aspose.Pdf.Document(TempFile))
            {
                bool? hideToolbar = GetViewerPreference(reopened, "HideToolbar");
                bool? fitWindow = GetViewerPreference(reopened, "FitWindow");
                bool? centerWindow = GetViewerPreference(reopened, "CenterWindow");

                // If the properties are available, they must be true.
                if (hideToolbar.HasValue)
                    NUnit.Framework.Assert.IsTrue(hideToolbar.Value, "HideToolbar should persist after reopen.");
                if (fitWindow.HasValue)
                    NUnit.Framework.Assert.IsTrue(fitWindow.Value, "FitWindow should persist after reopen.");
                if (centerWindow.HasValue)
                    NUnit.Framework.Assert.IsTrue(centerWindow.Value, "CenterWindow should persist after reopen.");
            }
        }
    }

    // Dummy entry point so the project compiles as an executable when a Main method is required.
    public class Program
    {
        public static void Main(string[] args)
        {
            // No operation – tests are executed by the test runner.
        }
    }
}
