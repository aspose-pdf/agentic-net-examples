using System;
using System.IO;
using Aspose.Pdf;               // Document class for page count
using Aspose.Pdf.Facades;      // PdfFileMend facade

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // Source PDF
        const string tiffImage = "image.tiff"; // TIFF to embed
        const string outputPdf = "output.pdf"; // Result PDF

        // Validate input files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(tiffImage))
        {
            Console.Error.WriteLine($"TIFF image not found: {tiffImage}");
            return;
        }

        // PdfFileMend adds images/text to existing PDFs without altering existing content
        using (PdfFileMend mender = new PdfFileMend())
        {
            // Load the PDF file into the facade
            mender.BindPdf(inputPdf);

            // Determine the last page number (Aspose.Pdf uses 1‑based indexing)
            int lastPage = mender.Document.Pages.Count;

            // Define the rectangle where the image will be placed on the page
            // (lower‑left X/Y and upper‑right X/Y in points)
            float lowerLeftX  = 50f;
            float lowerLeftY  = 50f;
            float upperRightX = 200f;
            float upperRightY = 200f;

            // Add the TIFF image to the last page
            bool success = mender.AddImage(tiffImage, lastPage,
                                           lowerLeftX, lowerLeftY,
                                           upperRightX, upperRightY);
            if (!success)
            {
                Console.Error.WriteLine("Failed to add the image to the PDF.");
                return;
            }

            // Save the modified PDF to a new file
            mender.Save(outputPdf);
        }

        Console.WriteLine($"TIFF image added to the last page. Output saved as '{outputPdf}'.");
    }
}