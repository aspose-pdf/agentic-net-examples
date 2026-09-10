using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation without the NUnit package.
// These provide the attributes and Assert methods used in the test.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        // Accept a nullable message to silence CS8625 warnings under nullable reference types.
        public static void IsTrue(bool condition, string? message = null)
        {
            if (!condition)
            {
                throw new Exception(message ?? "Assert.IsTrue failed.");
            }
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class ViewerPreferencePersistenceTests
    {
        private const string OriginalPdf = "original.pdf";
        private const string EditedPdf = "edited.pdf";

        // Helper to create a minimal PDF with a single blank page
        private void CreateSimplePdf(string path)
        {
            using (Document doc = new Document())
            {
                // Add an empty page (Aspose.Pdf adds a default page automatically)
                doc.Pages.Add();
                doc.Save(path);
            }
        }

        [Test]
        public void ViewerPreferences_ShouldPersistAfterReopen()
        {
            // Ensure any previous files are removed
            if (File.Exists(OriginalPdf)) File.Delete(OriginalPdf);
            if (File.Exists(EditedPdf)) File.Delete(EditedPdf);

            // 1. Create a simple PDF
            CreateSimplePdf(OriginalPdf);
            Assert.IsTrue(File.Exists(OriginalPdf), "Original PDF was not created.");

            // 2. Apply viewer preferences using PdfContentEditor
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(OriginalPdf);
                // Combine two preferences using bitwise OR
                int combinedPrefs = ViewerPreference.HideMenubar | ViewerPreference.PageModeUseNone;
                editor.ChangeViewerPreference(combinedPrefs);
                editor.Save(EditedPdf);
            }

            Assert.IsTrue(File.Exists(EditedPdf), "Edited PDF was not saved.");

            // 3. Reopen the edited PDF and verify the preferences
            using (PdfContentEditor verifier = new PdfContentEditor())
            {
                verifier.BindPdf(EditedPdf);
                int retrievedPrefs = verifier.GetViewerPreference();

                // Verify that both flags are present
                Assert.IsTrue((retrievedPrefs & ViewerPreference.HideMenubar) != 0,
                    "HideMenubar flag was not persisted.");
                Assert.IsTrue((retrievedPrefs & ViewerPreference.PageModeUseNone) != 0,
                    "PageModeUseNone flag was not persisted.");
            }

            // Cleanup temporary files
            File.Delete(OriginalPdf);
            File.Delete(EditedPdf);
        }
    }

    // Dummy entry point to satisfy the compiler for a console‑type project.
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // No operation – tests are executed by the test runner.
        }
    }
}