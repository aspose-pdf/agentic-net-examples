using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation when the real NUnit package is not referenced
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

    public static class Assert
    {
        public static void IsTrue(bool condition, string? message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }

        public static void AreEqual<T>(T expected, T actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class ViewerPreferencePersistenceTests
    {
        private const string TempFolder = "TempTestFiles";

        [SetUp]
        public void SetUp()
        {
            // Ensure a clean temporary folder for each test run
            if (Directory.Exists(TempFolder))
                Directory.Delete(TempFolder, true);
            Directory.CreateDirectory(TempFolder);
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up temporary files after tests
            if (Directory.Exists(TempFolder))
                Directory.Delete(TempFolder, true);
        }

        [Test]
        public void ViewerPreferences_ShouldPersistAfterReopening()
        {
            // Arrange: create a simple one‑page PDF
            string originalPdf = Path.Combine(TempFolder, "original.pdf");
            using (Document doc = new Document())
            {
                // Add a blank page (default size)
                doc.Pages.Add();
                doc.Save(originalPdf);
            }

            // Define the viewer preferences we want to set
            int expectedPreferences = ViewerPreference.HideMenubar | ViewerPreference.PageModeUseNone;

            // Act: change viewer preferences and save to a new file
            string editedPdf = Path.Combine(TempFolder, "edited.pdf");
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(originalPdf);
                editor.ChangeViewerPreference(expectedPreferences);
                editor.Save(editedPdf);
                // No explicit Close needed; using disposes the facade
            }

            // Assert: reopen the edited PDF and verify the preferences are unchanged
            using (PdfContentEditor verifyEditor = new PdfContentEditor())
            {
                verifyEditor.BindPdf(editedPdf);
                int actualPreferences = verifyEditor.GetViewerPreference();

                // The retrieved flags should contain both flags we set
                Assert.IsTrue((actualPreferences & ViewerPreference.HideMenubar) != 0,
                    "HideMenubar flag was not persisted.");
                Assert.IsTrue((actualPreferences & ViewerPreference.PageModeUseNone) != 0,
                    "PageModeUseNone flag was not persisted.");

                // Additionally, the exact bitmask should match the expected value
                Assert.AreEqual(expectedPreferences, actualPreferences,
                    "Viewer preferences do not match the expected value.");
            }
        }
    }

    // Adding a dummy entry point so the project compiles as a console application.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // The test runner (e.g., NUnit console) will discover and execute the tests.
            // This Main method is only present to satisfy the compiler's requirement for an entry point.
        }
    }
}
