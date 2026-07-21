using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// Minimal NUnit stubs so the test project can compile without the real NUnit package.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

namespace AsposePdfTests
{
    [NUnit.Framework.TestFixture]
    public class ViewerPreferenceTests
    {
        [NUnit.Framework.Test]
        public void ViewerPreferencePersistenceTest()
        {
            // Create a temporary working directory
            string workDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(workDir);

            string originalPdf = Path.Combine(workDir, "original.pdf");
            string editedPdf   = Path.Combine(workDir, "edited.pdf");

            // -----------------------------------------------------------------
            // Step 1: Generate a minimal PDF document (one blank page)
            // -----------------------------------------------------------------
            using (Document doc = new Document())
            {
                doc.Pages.Add();               // add a blank page
                doc.Save(originalPdf);         // save to disk
            }

            // -----------------------------------------------------------------
            // Step 2: Apply viewer preferences using PdfContentEditor
            // -----------------------------------------------------------------
            // Combine two flags as an example (hide menu bar + fit window)
            int setPreference = ViewerPreference.HideMenubar | ViewerPreference.FitWindow;

            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(originalPdf);                 // load the source PDF
                editor.ChangeViewerPreference(setPreference); // modify preferences
                editor.Save(editedPdf);                      // persist changes
            }

            // -----------------------------------------------------------------
            // Step 3: Re-open the edited PDF and read back the preferences
            // -----------------------------------------------------------------
            int readPreference;
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(editedPdf);                   // load the edited PDF
                readPreference = editor.GetViewerPreference(); // retrieve flags
            }

            // -----------------------------------------------------------------
            // Step 4: Verify that the persisted preferences match the set value
            // -----------------------------------------------------------------
            NUnit.Framework.Assert.AreEqual(setPreference, readPreference,
                "Viewer preferences were not persisted correctly.");

            // Clean up temporary files
            Directory.Delete(workDir, true);
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public static class Program
{
    public static void Main(string[] args)
    {
        // No operation – the real work is performed by the unit tests.
    }
}