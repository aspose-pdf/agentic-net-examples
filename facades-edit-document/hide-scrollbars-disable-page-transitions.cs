using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, remove any page transition effects, set viewer preferences and save.
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Remove page transition effects.
            //    The Page.Transition property does not exist in older
            //    Aspose.Pdf versions. Use PdfPageEditor to clear the
            //    transition by setting TransitionType to "None" (0).
            // ------------------------------------------------------------
            var editor = new PdfPageEditor(doc);
            editor.ProcessPages = Enumerable.Range(1, doc.Pages.Count).ToArray();
            editor.TransitionType = 0;          // 0 = TransitionType.None
            editor.TransitionDuration = 0;      // no duration
            editor.ApplyChanges();

            // ------------------------------------------------------------
            // 2. Set viewer preferences (hide UI elements & scrollbars).
            //    ViewerPreferences was added in later library versions.
            //    Access it via dynamic to keep the code compiling against
            //    older versions; if the property is missing, the catch block
            //    logs a friendly message and continues.
            // ------------------------------------------------------------
            dynamic dynDoc = doc;
            try
            {
                dynDoc.ViewerPreferences.HideScrollbars = true;   // hide scrollbars
                dynDoc.ViewerPreferences.HideToolbar   = true;   // hide toolbar (optional)
                dynDoc.ViewerPreferences.HideMenubar   = true;   // hide menubar (optional)
                dynDoc.ViewerPreferences.HideWindowUI = true;   // hide window UI (optional)
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
            }

            // Save the final PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
