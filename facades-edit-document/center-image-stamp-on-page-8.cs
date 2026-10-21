using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
            {
                // Verify that page 8 exists (pages are 1‑based)
                if (doc.Pages.Count < 8)
                {
                    Console.Error.WriteLine("The document contains fewer than 8 pages.");
                    return;
                }

                // Get page 8
                Aspose.Pdf.Page page = doc.Pages[8];

                // Create an image stamp from the specified image file
                Aspose.Pdf.ImageStamp stamp = new Aspose.Pdf.ImageStamp(stampImagePath);

                // Retrieve page dimensions
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Retrieve stamp dimensions (automatically set to the image size)
                double stampWidth = stamp.Width;
                double stampHeight = stamp.Height;

                // Center the stamp horizontally and vertically
                stamp.XIndent = (pageWidth - stampWidth) / 2;
                stamp.YIndent = (pageHeight - stampHeight) / 2;

                // Add the stamp to page 8
                page.AddStamp(stamp);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Stamp centered on page 8 saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}