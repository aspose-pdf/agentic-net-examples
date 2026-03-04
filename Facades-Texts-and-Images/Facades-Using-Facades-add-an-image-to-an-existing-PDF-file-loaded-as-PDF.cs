using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "image.png";

        // Verify that the source PDF and image exist
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

        // Initialize the PdfFileMend facade (used for adding images/text)
        PdfFileMend pdfMend = new PdfFileMend();

        // Load the existing PDF document
        pdfMend.BindPdf(inputPdf);

        // Add the image to page 1.
        // Parameters: image file path, page number (1‑based), lower‑left X, lower‑left Y,
        // upper‑right X, upper‑right Y (coordinates are in points, 1 inch = 72 points)
        pdfMend.AddImage(imagePath, 1, 100f, 500f, 300f, 650f);

        // Save the modified PDF to a new file
        pdfMend.Save(outputPdf);

        // Close the facade and release resources
        pdfMend.Close();
    }
}