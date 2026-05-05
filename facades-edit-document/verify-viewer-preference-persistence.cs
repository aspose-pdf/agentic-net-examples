using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Minimal MSTest stubs – added because the real Microsoft.VisualStudio.TestTools
// assembly is not referenced in the project. They provide the attributes and the
// Assert.IsTrue method used by the test.
// ---------------------------------------------------------------------------
namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestClassAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestMethodAttribute : Attribute { }

    public static class Assert
    {
        public static void IsTrue(bool condition, string? message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }
    }
}

namespace AsposePdfTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ViewerPreferencePersistenceTests
    {
        private const string HideMenubarFlag = "HideMenubar";

        /// <summary>
        /// Creates a simple one‑page PDF, changes a viewer preference,
        /// saves the document and verifies that the preference is still set
        /// after the file is reopened.
        /// </summary>
        [TestMethod]
        public void ViewerPreference_ShouldPersistAfterReopen()
        {
            // Arrange – create temporary file names
            string tempDir = Path.GetTempPath();
            string originalPdf = Path.Combine(tempDir, $"original_{Guid.NewGuid()}.pdf");
            string editedPdf   = Path.Combine(tempDir, $"edited_{Guid.NewGuid()}.pdf");

            // Create a minimal PDF (one blank page) and save it
            using (Document doc = new Document())
            {
                doc.Pages.Add();                     // add a blank page
                doc.Save(originalPdf);               // save the source PDF
            }

            // Act – change a viewer preference using PdfContentEditor
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(originalPdf);                                 // load the source PDF
            editor.ChangeViewerPreference(ViewerPreference.HideMenubar); // set the HideMenubar flag
            editor.Save(editedPdf);                                      // persist the change
            editor.Close();                                              // release resources

            // Assert – reopen the edited PDF and verify the flag is still set
            PdfContentEditor verifyEditor = new PdfContentEditor();
            verifyEditor.BindPdf(editedPdf);
            int prefValue = (int)verifyEditor.GetViewerPreference();

            // The ViewerPreference constants are bit flags; use bitwise AND to test
            bool isHideMenubarSet = (prefValue & (int)ViewerPreference.HideMenubar) != 0;
            Assert.IsTrue(isHideMenubarSet, $"Viewer preference '{HideMenubarFlag}' was not persisted.");

            verifyEditor.Close();

            // Cleanup temporary files
            try { File.Delete(originalPdf); } catch { /* ignore */ }
            try { File.Delete(editedPdf);   } catch { /* ignore */ }
        }
    }
}

// Dummy entry point to satisfy the compiler when building as an executable.
public static class Program
{
    public static void Main() { }
}