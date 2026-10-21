using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Included as requested

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

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Create a text stamp; TextStamp derives from Stamp and provides the needed properties
                TextStamp stamp = new TextStamp("CONFIDENTIAL")
                {
                    // Rotate 45 degrees for diagonal placement
                    RotateAngle = 45,

                    // Center the stamp on each page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,

                    // No additional offset
                    XIndent = 0,
                    YIndent = 0,

                    // Optional visual tweaks
                    Opacity   = 0.5,   // semi‑transparent
                    Background = false // transparent background
                };

                // Apply the stamp to every page in the document
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(stamp);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Stamp applied and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}