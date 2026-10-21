using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace as requested (not directly used here)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string stampImg  = "stamp.png";
        const string outputPdf = "stamped_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Create a single stamp instance
                ImageStamp stamp = new ImageStamp(stampImg)
                {
                    // Position the stamp (coordinates are in points; lower‑left origin)
                    // Example: place stamp at (100, 100) with its original size
                    BottomMargin = 100,
                    LeftMargin   = 100,
                    // Make the stamp appear on top of page content
                    Background   = false,
                    // Optional: set opacity (0.0 = fully transparent, 1.0 = opaque)
                    Opacity      = 0.8
                };

                // Apply the same stamp to every page efficiently
                // Pages collection is 1‑based; foreach abstracts the indexing
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(stamp);
                }

                // Save the modified document
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}