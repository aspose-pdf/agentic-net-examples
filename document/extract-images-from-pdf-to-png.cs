using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            int imageIndex = 0;

            // Pages are 1‑based in Aspose.Pdf
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Iterate over all images on the page
                foreach (XImage img in page.Resources.Images)
                {
                    imageIndex++;
                    string outputPath = Path.Combine(outputFolder,
                        $"Image_Page{pageNum}_{imageIndex}.png");

                    // Save the image using a FileStream (preserves original resolution)
                    using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }
                }
            }
        }

        Console.WriteLine("All images have been extracted successfully.");
    }
}