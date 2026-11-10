using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Use PdfExtractor to extract only images that are defined in the PDF resources
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPath);
                // The ExtractImageMode enum lives in the Aspose.Pdf namespace
                extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;
                extractor.ExtractImage(); // start the extraction process

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string outPath = Path.Combine(outputDir, $"image_{imageIndex}.png");
                    using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        extractor.GetNextImage(fs);
                    }
                    imageIndex++;
                }
            }

            Console.WriteLine($"Images extracted to '{outputDir}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
