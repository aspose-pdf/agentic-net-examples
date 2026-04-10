using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor to extract images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Set page range: start at 1, end at 0 (0 means all pages)
            extractor.StartPage = 1;
            extractor.EndPage = 0;

            // Perform the image extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each extracted image; format is inferred from the file extension
                string outputPath = Path.Combine(outputDir, $"image-{imageIndex}.png");
                extractor.GetNextImage(outputPath);
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}

/*
 * NOTE: To build this project successfully you must add the Aspose.Pdf NuGet package.
 * In a .NET SDK‑style project add the following PackageReference to the .csproj file:
 *   <PackageReference Include="Aspose.Pdf" Version="*desired version*" />
 * Then run `dotnet restore` before building.
 */