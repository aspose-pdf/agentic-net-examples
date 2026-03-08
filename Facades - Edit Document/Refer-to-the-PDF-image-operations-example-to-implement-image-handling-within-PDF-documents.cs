using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and image to be added
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath     = "picture.jpg";

        // Verify that the required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the existing PDF document inside a using block (lifecycle rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialise the PdfFileMend facade with the loaded document
            PdfFileMend mend = new PdfFileMend(pdfDoc);

            // Open the image as a stream (also inside a using block)
            using (Stream imgStream = File.OpenRead(imagePath))
            {
                // Add the image to page 1.
                // Coordinates are expressed in points (1 inch = 72 points).
                // Example places the image at lower‑left (10,10) and upper‑right (100,100).
                bool success = mend.AddImage(
                    imgStream,          // image stream
                    1,                  // page number (1‑based)
                    10f,                // lower left X
                    10f,                // lower left Y
                    100f,               // upper right X
                    100f);              // upper right Y

                if (!success)
                {
                    Console.Error.WriteLine("Failed to add image to the PDF.");
                }
            }

            // Save the modified document to the desired output file.
            // PdfFileMend inherits SaveableFacade, so we use its Save method.
            mend.Save(outputPdfPath);

            // Close the facade to release internal resources.
            mend.Close();
        }

        Console.WriteLine($"Image added and PDF saved to '{outputPdfPath}'.");
    }
}