using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source TeX file and the desired PDF output.
        const string texFilePath = "input.tex";
        const string pdfOutputPath = "output.pdf";

        // Verify that the TeX file exists.
        if (!File.Exists(texFilePath))
        {
            Console.Error.WriteLine($"File not found: {texFilePath}");
            return;
        }

        // Open the TeX file as a read‑only stream.
        using (FileStream texStream = File.OpenRead(texFilePath))
        {
            // Create default load options for TeX conversion.
            TeXLoadOptions texLoadOptions = new TeXLoadOptions();

            // Load the TeX content into a PDF Document using the stream and options.
            using (Document pdfDocument = new Document(texStream, texLoadOptions))
            {
                // Save the generated PDF to disk.
                pdfDocument.Save(pdfOutputPath);
            }
        }

        Console.WriteLine($"Successfully converted '{texFilePath}' to PDF '{pdfOutputPath}'.");
    }
}