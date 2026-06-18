using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

// ---------------------------------------------------------------------------
// Minimal NUnit stubs – used when the real NUnit package is not referenced.
// ---------------------------------------------------------------------------
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
        public static void IsTrue(bool condition, string message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }
    }
}

namespace AsposePdfTests
{
    // Dummy entry point to satisfy the console‑app requirement.
    public class Program
    {
        public static void Main(string[] args)
        {
            // No operation – tests are executed by the test runner.
        }
    }

    [NUnit.Framework.TestFixture]
    public class ViewerPreferencePersistenceTests
    {
        private const string OriginalPdf = "original.pdf";
        private const string EditedPdf   = "edited.pdf";

        [NUnit.Framework.SetUp]
        public void SetUp()
        {
            // Create a simple PDF with one page and some text.
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                // Add a text fragment to the page.
                TextFragment tf = new TextFragment("Sample PDF for viewer preference test.");
                page.Paragraphs.Add(tf);

                // Save the original PDF.
                doc.Save(OriginalPdf);
            }
        }

        [NUnit.Framework.TearDown]
        public void TearDown()
        {
            // Clean up temporary files.
            if (File.Exists(OriginalPdf))
                File.Delete(OriginalPdf);
            if (File.Exists(EditedPdf))
                File.Delete(EditedPdf);
        }

        [NUnit.Framework.Test]
        public void ViewerPreferences_ShouldPersistAfterReopen()
        {
            // Define the viewer preferences to apply.
            // PdfContentEditor.ChangeViewerPreference expects an int, not the enum type.
            int preferencesToSet = (int)ViewerPreference.HideMenubar | (int)ViewerPreference.PageModeUseNone;

            // Change viewer preferences using PdfContentEditor and save the result.
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(OriginalPdf);
                editor.ChangeViewerPreference(preferencesToSet);
                editor.Save(EditedPdf);
            }

            // Reopen the edited PDF and retrieve the stored preferences.
            int retrievedPreferences;
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(EditedPdf);
                retrievedPreferences = editor.GetViewerPreference(); // returns int
            }

            // Verify that the set preferences are present in the retrieved value.
            NUnit.Framework.Assert.IsTrue((retrievedPreferences & (int)ViewerPreference.HideMenubar) != 0,
                "HideMenubar flag was not persisted.");
            NUnit.Framework.Assert.IsTrue((retrievedPreferences & (int)ViewerPreference.PageModeUseNone) != 0,
                "PageModeUseNone flag was not persisted.");
        }
    }
}
