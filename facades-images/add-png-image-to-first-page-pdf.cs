using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath = "image.png";

        // Verify required files exist
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

        // Initialize PdfFileMend and bind the existing PDF
        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(inputPdfPath);

        // Load the PNG image from a stream and add it to page 1 within the specified rectangle
        using (FileStream imgStream = File.OpenRead(imagePath))
        {
            // PdfFileMend.AddImage expects a single page number (int) and float coordinates.
            // Rectangle defined by lower‑left (50,50) and upper‑right (300,300)
            mend.AddImage(imgStream, 1, 50f, 50f, 300f, 300f);
        }

        // Save the modified PDF to the output path
        mend.Save(outputPdfPath);
    }
}
