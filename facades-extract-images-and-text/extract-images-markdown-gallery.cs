// ------------------------------------------------------------
// File: AsposePdfApi.GlobalUsings.g.cs
// ------------------------------------------------------------
// This file satisfies the missing generated global‑usings source file.
// It can be empty or contain the required global using directives.
// Adding the file prevents the CS2001 error during compilation.

global using System;
global using System.IO;
global using Aspose.Pdf.Facades;

// ------------------------------------------------------------
// File: Program.cs
// ------------------------------------------------------------
using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string imagesFolder   = "extracted_images";
        const string markdownPath   = "ImageGallery.md";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the folder for extracted images exists
        Directory.CreateDirectory(imagesFolder);

        // Create a PdfExtractor and bind it to the source PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);

            // Extract all images from the document
            extractor.ExtractImage();

            // Prepare the markdown file
            using (StreamWriter mdWriter = new StreamWriter(markdownPath, false))
            {
                mdWriter.WriteLine("# Image Gallery");
                mdWriter.WriteLine();

                int imageIndex = 1;
                // Iterate over all extracted images
                while (extractor.HasNextImage())
                {
                    // Save each image as PNG (Aspose.Pdf extracts in original format; PNG works for most cases)
                    string imageFileName = $"image{imageIndex}.png";
                    string imageFullPath = Path.Combine(imagesFolder, imageFileName);

                    // Save the current image to disk
                    extractor.GetNextImage(imageFullPath);

                    // Write a markdown entry linking to the image
                    mdWriter.WriteLine($"![Image {imageIndex}]({Path.Combine(imagesFolder, imageFileName)})");
                    mdWriter.WriteLine();

                    imageIndex++;
                }

                // If no images were found, note it in the markdown
                if (imageIndex == 1)
                {
                    mdWriter.WriteLine("_No images were found in the PDF._");
                }
            }
        }

        Console.WriteLine($"Image extraction complete. Gallery written to '{markdownPath}'.");
    }
}
