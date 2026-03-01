using System;
using System.IO;
using Aspose.Pdf; // TeXLoadOptions and Document are in this namespace

class Program
{
    static void Main()
    {
        // Path to the source TeX file
        const string texFilePath = "input.tex";

        // Path where the resulting PDF will be saved
        const string pdfOutputPath = "output.pdf";

        // Verify that the TeX source file exists
        if (!File.Exists(texFilePath))
        {
            Console.Error.WriteLine($"TeX file not found: {texFilePath}");
            return;
        }

        // Open the TeX file as a read‑only stream
        using (FileStream texStream = File.OpenRead(texFilePath))
        {
            // Initialize load options for TeX conversion
            TeXLoadOptions texLoadOptions = new TeXLoadOptions();

            // Load the TeX content from the stream and convert it to a PDF Document
            using (Document pdfDocument = new Document(texStream, texLoadOptions))
            {
                // Save the resulting PDF to disk
                pdfDocument.Save(pdfOutputPath);
            }
        }

        Console.WriteLine($"TeX file '{texFilePath}' has been converted to PDF '{pdfOutputPath}'.");
    }
}