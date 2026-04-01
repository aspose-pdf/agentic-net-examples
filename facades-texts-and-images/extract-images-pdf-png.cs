using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }

        try
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPath);
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage() && imageIndex <= 4)
                {
                    string outputFile = "image-" + imageIndex + ".png";
                    bool success = extractor.GetNextImage(outputFile, ImageFormat.Png);
                    if (success)
                    {
                        Console.WriteLine("Extracted: " + outputFile);
                    }
                    else
                    {
                        Console.Error.WriteLine("Failed to extract image " + imageIndex);
                    }
                    imageIndex++;
                }

                // Note: Evaluation mode allows a maximum of 4 extracted images.
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error: " + ex.Message);
        }
    }
}
