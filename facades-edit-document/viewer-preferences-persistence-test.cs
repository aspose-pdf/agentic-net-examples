using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// Minimal NUnit stubs to allow compilation without the real NUnit package
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OneTimeSetUpAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OneTimeTearDownAttribute : Attribute { }

    public static class Assert
    {
        // Allow nullable message parameter for nullable reference type compliance
        public static void IsTrue(bool condition, string? message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }

        // Additional helpers can be added as needed (AreEqual, Throws, etc.)
    }
}

namespace AsposePdfTests
{
    [NUnit.Framework.TestFixture]
    public class ViewerPreferencePersistenceTests
    {
        private const string TempFolder = "TempTestFiles";

        [NUnit.Framework.OneTimeSetUp]
        public void GlobalSetup()
        {
            // Ensure a clean temporary folder for test files
            if (Directory.Exists(TempFolder))
                Directory.Delete(TempFolder, true);
            Directory.CreateDirectory(TempFolder);
        }

        [NUnit.Framework.OneTimeTearDown]
        public void GlobalTeardown()
        {
            // Cleanup temporary files after all tests run
            if (Directory.Exists(TempFolder))
                Directory.Delete(TempFolder, true);
        }

        [NUnit.Framework.Test]
        public void ViewerPreferences_ShouldPersistAfterReopen()
        {
            // Arrange: create a simple one‑page PDF
            string originalPdf = Path.Combine(TempFolder, "original.pdf");
            using (Document doc = new Document())
            {
                // Add a blank page
                doc.Pages.Add();
                doc.Save(originalPdf);
            }

            // Define the output PDF path
            string editedPdf = Path.Combine(TempFolder, "edited.pdf");

            // Act: set viewer preferences using PdfContentEditor
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(originalPdf);
                // Combine two preferences using bitwise OR (cast to int for safety)
                int prefs = (int)Aspose.Pdf.Facades.ViewerPreference.HideMenubar |
                            (int)Aspose.Pdf.Facades.ViewerPreference.FitWindow;
                editor.ChangeViewerPreference(prefs);
                editor.Save(editedPdf);
            }

            // Assert: reopen the edited PDF and verify the preferences are still set
            using (PdfContentEditor verifyEditor = new PdfContentEditor())
            {
                verifyEditor.BindPdf(editedPdf);
                int retrievedPrefs = verifyEditor.GetViewerPreference();

                // Both flags should be present in the retrieved value
                NUnit.Framework.Assert.IsTrue((retrievedPrefs & (int)Aspose.Pdf.Facades.ViewerPreference.HideMenubar) != 0,
                    "HideMenubar flag was not persisted.");
                NUnit.Framework.Assert.IsTrue((retrievedPrefs & (int)Aspose.Pdf.Facades.ViewerPreference.FitWindow) != 0,
                    "FitWindow flag was not persisted.");
            }

            // Cleanup individual test files
            File.Delete(originalPdf);
            File.Delete(editedPdf);
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    // In a real test project this would be compiled as a library, but adding a Main method
    // removes the CS5001 error without affecting test execution.
    public static class Program
    {
        public static void Main() { /* No‑op */ }
    }
}
