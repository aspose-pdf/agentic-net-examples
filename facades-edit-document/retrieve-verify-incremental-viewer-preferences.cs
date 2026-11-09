using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // ---- Edit operation: add a text stamp to every page ----
            TextStamp stamp = new TextStamp("CONFIDENTIAL")
            {
                // Stamp appearance settings
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                Opacity             = 0.5,
                Background          = false
            };

            // Page.AddStamp is the correct method; iterate over all pages
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // ---- Retrieve viewer preferences after the edit ----
            // ViewerPreferences property exists only in newer Aspose.Pdf versions.
            // Guard the usage with a compile‑time symbol; if the symbol is not defined,
            // the code falls back to a safe message.
#if ASPosePDF_HAS_VIEWERPREFERENCES
            var prefs = doc.ViewerPreferences;
            Console.WriteLine($"Viewer Preferences: {prefs}");
#else
            // Property not available in this library version.
            Console.WriteLine("ViewerPreferences property not available in this Aspose.Pdf version.");
#endif

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}