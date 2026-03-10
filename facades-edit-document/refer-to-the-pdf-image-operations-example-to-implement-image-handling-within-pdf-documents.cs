using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // destination PDF
        const string imagePath = "picture.jpg"; // image to embed

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

        // Initialize the PdfFileMend facade and bind the source PDF
        PdfFileMend pdfMend = new PdfFileMend();
        pdfMend.BindPdf(inputPdf);

        // Add the image to page 1.
        // Coordinates are in points (1/72 inch). Here we place the image
        // with lower‑left corner at (50,50) and upper‑right corner at (300,300).
        using (FileStream imgStream = File.OpenRead(imagePath))
        {
            pdfMend.AddImage(imgStream, 1, 50, 50, 300, 300);
        }

        // Save the modified document and release resources
        pdfMend.Save(outputPdf);
        pdfMend.Close();

        Console.WriteLine($"Image added successfully. Output saved to '{outputPdf}'.");
    }
}