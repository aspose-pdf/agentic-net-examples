using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outFolder = "output_images";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outFolder);

        // Initialize the extractor and extract images
        var extractor = new PdfExtractor();
        extractor.BindPdf(pdfPath);
        extractor.ExtractImage();

        int imageIndex = 0;
        while (extractor.HasNextImage())
        {
            using (var imgStream = new MemoryStream())
            {
                extractor.GetNextImage(imgStream);
                imgStream.Position = 0;

                // Save each image as a separate file (JPEG by default)
                string outPath = Path.Combine(outFolder, $"image_{++imageIndex}.jpg");
                File.WriteAllBytes(outPath, imgStream.ToArray());
                Console.WriteLine($"Saved image {imageIndex} to {outPath}");
            }
        }

        extractor.Close();
        Console.WriteLine($"Extracted {imageIndex} image(s) from '{pdfPath}' into folder '{outFolder}'.");
    }
}
