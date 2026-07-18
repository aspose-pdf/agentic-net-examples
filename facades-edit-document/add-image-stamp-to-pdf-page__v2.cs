using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "stampImage.png";

        // Verify that the required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize the PdfFileStamp facade
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Set source and destination PDF files
            fileStamp.InputFile  = inputPdf;
            fileStamp.OutputFile = outputPdf;

            // Create a stamp and bind the image to it
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(imagePath);

            // Position and size of the stamp (adjust as needed)
            stamp.SetOrigin(100, 500);          // X, Y coordinates on the page
            stamp.SetImageSize(150, 100);       // Width, Height

            // Make the stamp semi‑transparent
            stamp.Opacity = 0.7f;

            // Apply the stamp only to the second page (1‑based indexing)
            stamp.PageNumber = 2;

            // Add the stamp to the PDF
            fileStamp.AddStamp(stamp);

            // Persist changes
            fileStamp.Close();
        }

        Console.WriteLine($"Image stamp added to page 2 and saved as '{outputPdf}'.");
    }
}