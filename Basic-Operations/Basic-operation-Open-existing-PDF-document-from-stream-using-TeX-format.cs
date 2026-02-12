using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the TeX source file
        const string texFilePath = "sample.tex";
        // Desired output PDF file path
        const string outputPdfPath = "output.pdf";

        // Verify that the TeX file exists
        if (!File.Exists(texFilePath))
        {
            Console.Error.WriteLine($"Error: TeX file not found at '{texFilePath}'.");
            return;
        }

        // Open the TeX file as a read‑only stream
        using (FileStream texStream = File.OpenRead(texFilePath))
        {
            // Configure TeX loading options (default settings are sufficient here)
            TeXLoadOptions texLoadOptions = new TeXLoadOptions();

            // Load the TeX content from the stream and convert it to a PDF document
            using (Document pdfDocument = new Document(texStream, texLoadOptions))
            {
                // Save the resulting PDF to the specified output path
                pdfDocument.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"PDF successfully created at '{outputPdfPath}'.");
    }
}