using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string imagePath = "logo.png";    // image to insert
        const string outputPdf = "output.pdf";  // result PDF

        // Verify that required files exist
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

        // Create the facade, bind the PDF, modify, and save
        using (PdfFileMend pdfMend = new PdfFileMend())
        {
            // Load the PDF document into the facade
            pdfMend.BindPdf(inputPdf);

            // Add the image to page 1.
            // Parameters: image file, page number (1‑based), lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
            pdfMend.AddImage(imagePath, 1, 100f, 500f, 200f, 600f);

            // Persist the changes to a new file
            pdfMend.Save(outputPdf);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}