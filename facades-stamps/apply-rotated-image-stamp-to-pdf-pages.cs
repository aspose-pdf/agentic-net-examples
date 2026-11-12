using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string stampPath  = "stamp.png";
        const string outputPath = "output.pdf";

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
            // Load the PDF with the high‑level Document API (cross‑platform and fully featured)
            using (Document doc = new Document(inputPath))
            {
                // Create the image stamp with the required rotation and alignment
                ImageStamp imgStamp = new ImageStamp(stampPath)
                {
                    RotateAngle        = 30,                     // 30° rotation
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    BottomMargin        = 10,
                    RightMargin         = 10
                };

                // Apply the stamp to every page
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(imgStamp);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Rotated image stamp applied to all pages. Saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
