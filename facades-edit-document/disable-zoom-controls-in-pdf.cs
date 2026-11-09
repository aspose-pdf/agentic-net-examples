using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_fixed_zoom.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPath);

            // ---------------------------------------------------------------------
            // Attempt to use ViewerPreferences (available in newer Aspose.Pdf versions).
            // The property may not exist in older versions, so we access it via
            // dynamic and gracefully handle the absence.
            // ---------------------------------------------------------------------
            dynamic docDynamic = pdfDoc;
            try
            {
                var prefs = docDynamic.ViewerPreferences;
                // Hide toolbar and window UI – this removes the built‑in zoom controls.
                prefs.HideToolbar = true;
                prefs.HideWindowUI = true;
            }
            catch (RuntimeBinderException)
            {
                // Fallback for older Aspose.Pdf versions that lack ViewerPreferences.
                // We set a fixed zoom level (100%) using JavaScript and hide UI
                // elements that can be controlled via JavaScript.
                // Note: Not all viewers respect these settings, but it provides a
                // reasonable degradation path.
                pdfDoc.OpenAction = new JavascriptAction("this.zoom = 100; app.hideToolbar = true; app.hideMenuBar = true;");
            }

            // Save the modified PDF
            pdfDoc.Save(outputPath);

            Console.WriteLine($"PDF saved with disabled zoom controls: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
