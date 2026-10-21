using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // Use the modern ImageStamp class

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stamped_output.pdf";
        const string stampPath = "stamp.png";

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
                // Create a single ImageStamp instance; its properties will be adjusted per page
                ImageStamp imgStamp = new ImageStamp(stampPath)
                {
                    // Position the stamp at the centre of each page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,
                    // Make the stamp semi‑transparent
                    Opacity = 0.5
                };

                // Iterate pages (1‑based indexing)
                int pageIndex = 1;
                foreach (Page page in pdfDoc.Pages)
                {
                    // Calculate rotation angle based on page index (e.g., 0°, 45°, 90°, …)
                    float rotationAngle = ((pageIndex - 1) * 45) % 360;
                    // Use RotateAngle (not a non‑existent Rotation property)
                    imgStamp.RotateAngle = rotationAngle;

                    // Apply the stamp to the current page
                    page.AddStamp(imgStamp);

                    pageIndex++;
                }

                // Save the modified PDF
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