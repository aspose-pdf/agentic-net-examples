using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string tempPdf   = "temp_pref.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // 1. Set viewer preferences to hide UI elements (including zoom controls)
            using (PdfContentEditor viewerEditor = new PdfContentEditor())
            {
                viewerEditor.BindPdf(inputPdf);
                // Hide the entire window UI (toolbars, menus, scrollbars, etc.)
                viewerEditor.ChangeViewerPreference(ViewerPreference.HideWindowUI);
                // Additionally hide the toolbar if a more granular control is desired
                viewerEditor.ChangeViewerPreference(ViewerPreference.HideToolbar);
                viewerEditor.Save(tempPdf);
            }

            // 2. Set a fixed zoom level (100% = 1.0) for all pages
            using (PdfPageEditor zoomEditor = new PdfPageEditor())
            {
                zoomEditor.BindPdf(tempPdf);
                zoomEditor.Zoom = 1.0f; // Fixed zoom (no scaling)
                zoomEditor.Save(outputPdf);
            }

            // Optional: clean up the intermediate file
            if (File.Exists(tempPdf))
                File.Delete(tempPdf);

            Console.WriteLine($"Viewer preferences applied and fixed zoom set. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}