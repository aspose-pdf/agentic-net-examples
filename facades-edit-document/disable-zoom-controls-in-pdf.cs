using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // Hide the zoom controls in the viewer preferences.
            // The ViewerPreferences property is only available in newer Aspose.Pdf
            // versions. To keep the source compatible with older versions we access
            // it via dynamic and swallow the RuntimeBinderException if the property
            // does not exist.
            // -----------------------------------------------------------------
            try
            {
                dynamic dynDoc = doc; // bypass compile‑time check
                dynDoc.ViewerPreferences.HideZoomControls = true;
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences.HideZoomControls is not supported by the current Aspose.Pdf version.");
            }

            // -----------------------------------------------------------------
            // Set a fixed zoom level (100 %) when the document is opened.
            // Using a JavaScript open action is the most version‑agnostic way to
            // enforce a specific zoom because the Destination class may be absent
            // in older releases.
            // -----------------------------------------------------------------
            const int zoomPercent = 100; // 100 % = 1.0 scale
            string js = $"this.zoom = {zoomPercent};";
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with disabled zoom controls and fixed zoom to '{outputPath}'.");
    }
}
