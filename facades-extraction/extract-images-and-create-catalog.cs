using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string imagesDir = "ExtractedImages";
        const string catalogPath = "ImageCatalog.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(imagesDir);

        // PdfExtractor implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);
            // Optional: extract images defined in resources (more reliable for some PDFs)
            // NOTE: The ExtractImageMode property is not available in the referenced Aspose.Pdf version.
            // The default behavior extracts all images, so we omit this setting.
            // extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            // Enable image extraction
            extractor.ExtractImage();

            // Prepare the catalog file
            using (StreamWriter catalogWriter = new StreamWriter(catalogPath, false))
            {
                int imageIndex = 1;

                // Iterate over all extracted images
                while (extractor.HasNextImage())
                {
                    // Since PdfExtractor does not expose the original image name, use a deterministic name
                    string imageFileName = $"Image_{imageIndex}.png"; // default to PNG
                    string imagePath = Path.Combine(imagesDir, imageFileName);

                    // Save the current image to disk (using a stream)
                    using (FileStream imgStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                    {
                        extractor.GetNextImage(imgStream);
                    }

                    // Record the file name in the catalog
                    catalogWriter.WriteLine(imageFileName);
                    imageIndex++;
                }
            }
        }

        Console.WriteLine($"Images extracted to '{imagesDir}'. Catalog written to '{catalogPath}'.");
    }
}
