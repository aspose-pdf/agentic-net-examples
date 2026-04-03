using System;
using System.IO;
using Aspose.Pdf;

class ReplaceJpegWithPng
{
    static void Main()
    {
        // Paths to the source PDF, the PNG image to use as replacement, and the output PDF.
        const string inputPdfPath = "input.pdf";
        const string pngImagePath = "replacement.png";
        const string outputPdfPath = "output.pdf";

        // Verify that the PNG replacement file exists before attempting to read it.
        if (!File.Exists(pngImagePath))
        {
            Console.Error.WriteLine($"Error: Replacement image file '{pngImagePath}' was not found.");
            Console.Error.WriteLine("Make sure the file exists in the working directory or provide a correct path.");
            return; // Exit gracefully instead of throwing an unhandled exception.
        }

        // Load the PNG image once into memory.
        byte[] pngData = File.ReadAllBytes(pngImagePath);

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages in the document.
            foreach (Page page in pdfDoc.Pages)
            {
                // Absorb all image placements on the current page.
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                // Replace each image (originally JPEG) with the PNG data.
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    using (MemoryStream pngStream = new MemoryStream(pngData))
                    {
                        // The Replace method updates the image while preserving its position,
                        // size, rotation, and other transformation attributes.
                        placement.Replace(pngStream);
                    }
                }
            }

            // Save the modified PDF to the specified output path.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"All JPEG images have been replaced with PNG equivalents and saved to '{outputPdfPath}'.");
    }
}
