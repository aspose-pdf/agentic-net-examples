using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder; // Needed for RuntimeBinderException

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

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Attempt to set viewer preferences. The ViewerPreferences property
                // exists only in newer Aspose.Pdf versions (>= 20.9). To keep the code
                // compatible with older versions we access it via dynamic and handle
                // the possible RuntimeBinderException gracefully.
                try
                {
                    dynamic d = doc;
                    d.ViewerPreferences.PageLayout = Aspose.Pdf.PageLayout.OneColumn; // continuous scrolling (single‑page view)
                    d.ViewerPreferences.Zoom = 100; // default zoom 100%
                }
                catch (RuntimeBinderException)
                {
                    Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version. Skipping viewer‑preference configuration.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with viewer preferences to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
