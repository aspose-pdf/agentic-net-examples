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

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document pdfDoc = new Document(inputPdf))
        {
            int imageIndex = 1;

            // Iterate through each page (1‑based indexing)
            foreach (Page page in pdfDoc.Pages)
            {
                // Iterate over the XImage collection (rule: foreach, not dictionary)
                foreach (XImage img in page.Resources.Images)
                {
                    // Build a unique file name for each extracted image
                    string outputPath = Path.Combine(outputFolder, $"image_{imageIndex}.png");

                    // Save the image as PNG using the stream overload (required by Aspose.Pdf)
                    using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }

                    Console.WriteLine($"Extracted image {imageIndex} → {outputPath}");
                    imageIndex++;
                }
            }
        }
    }
}
