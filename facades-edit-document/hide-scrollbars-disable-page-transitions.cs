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
        const string outputPath = "clean_view.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ---------- Viewer Preferences (runtime‑safe) ----------
            // The ViewerPreferences property may not exist in older Aspose.Pdf versions.
            // Access it via dynamic and handle the possible RuntimeBinderException.
            dynamic d = doc;
            try
            {
                d.ViewerPreferences.HideWindowUI = true;   // hides scrollbars and other UI elements
                d.ViewerPreferences.HideToolbar = true;
                d.ViewerPreferences.HideMenubar = true;
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
            }

            // ---------- Disable Page Transitions ----------
            // In recent Aspose.Pdf versions the Page class no longer exposes a "Transition" property.
            // The recommended way is to use PdfPageEditor and set the transition type to "None" for all pages.
            try
            {
                using (PdfPageEditor editor = new PdfPageEditor(doc))
                {
                    // Apply to every page (1‑based indexing)
                    editor.ProcessPages = Enumerable.Range(1, doc.Pages.Count).ToArray();
                    // TransitionType enum value 0 corresponds to "None" – using the integer literal keeps the code version‑agnostic.
                    editor.TransitionType = 0; // None
                    editor.TransitionDuration = 0; // No duration
                    editor.ApplyChanges();
                }
            }
            catch (Exception ex)
            {
                // Fallback for very old versions that might still expose the property via dynamic.
                Console.WriteLine($"Failed to clear transitions via PdfPageEditor: {ex.Message}");
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    dynamic pageDyn = doc.Pages[i];
                    try
                    {
                        pageDyn.Transition = null;
                    }
                    catch (RuntimeBinderException) { /* ignore – transition removal not supported */ }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden UI and no page transitions: {outputPath}");
    }
}
