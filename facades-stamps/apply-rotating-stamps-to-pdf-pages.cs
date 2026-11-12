using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string stampPath  = "stamp.png";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(stampPath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= pdfDoc.Pages.Count; i++)
                {
                    // Create an image stamp for the current page
                    ImageStamp imgStamp = new ImageStamp(stampPath);

                    // Rotate each stamp by an angle that depends on the page index
                    // Example: 10 degrees per page index (page 1 → 10°, page 2 → 20°, etc.)
                    imgStamp.RotateAngle = i * 10;

                    // Center the stamp on the page
                    imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
                    imgStamp.VerticalAlignment   = VerticalAlignment.Center;

                    // Apply the stamp to the current page
                    pdfDoc.Pages[i].AddStamp(imgStamp);
                }

                // Save the modified document
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}