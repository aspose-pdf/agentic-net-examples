using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using NUnit.Framework; // Added to reference the stubbed NUnit types

// Minimal NUnit stubs to allow compilation without the NUnit package
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

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
    [TestFixture]
    public class ViewerPreferenceTests
    {
        private const string OriginalFile = "original.pdf";
        private const string EditedFile = "edited.pdf";

        [Test]
        public void ViewerPreference_ShouldPersistAfterReopen()
        {
            // Create a simple PDF document
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                TextFragment fragment = new TextFragment("Sample");
                page.Paragraphs.Add(fragment);
                doc.Save(OriginalFile);
            }

            // Apply viewer preferences using PdfContentEditor
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(OriginalFile);
            int setPreference = (int)(ViewerPreference.HideMenubar | ViewerPreference.PageModeUseNone);
            editor.ChangeViewerPreference(setPreference);
            editor.Save(EditedFile);
            editor.Close();

            // Reopen the edited PDF and verify the preferences persisted
            PdfContentEditor verifier = new PdfContentEditor();
            verifier.BindPdf(EditedFile);
            int retrievedPreference = verifier.GetViewerPreference();
            verifier.Close();

            Assert.IsTrue((retrievedPreference & setPreference) == setPreference,
                "Viewer preferences were not persisted.");
        }
    }
}

// Dummy entry point to satisfy the compiler when building as an executable.
public static class Program
{
    public static void Main()
    {
        // No operation – the project is intended for unit testing.
    }
}
