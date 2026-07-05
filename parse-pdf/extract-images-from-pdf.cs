using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (using block ensures proper disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            int imageIndex = 1;

            // Pages are 1‑based in Aspose.Pdf
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Iterate over the image resources of the page
                foreach (XImage img in page.Resources.Images)
                {
                    // Build a unique file name for each extracted image
                    string outputPath = Path.Combine(outputFolder, $"image_{imageIndex}.png");

                    // Save the image using a FileStream (XImage.Save expects a Stream)
                    using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }

                    Console.WriteLine($"Extracted image saved to: {outputPath}");
                    imageIndex++;
                }
            }
        }
    }
}
