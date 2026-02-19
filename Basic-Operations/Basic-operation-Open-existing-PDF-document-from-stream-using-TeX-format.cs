using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source TeX file and the output PDF file
        string texFilePath = "sample.tex";
        string pdfOutputPath = "output.pdf";

        // Verify that the TeX file exists before attempting to load it
        if (!File.Exists(texFilePath))
        {
            Console.Error.WriteLine($"Error: TeX file not found at '{texFilePath}'.");
            return;
        }

        // Create TeX load options (customize if needed)
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();
        // Example: texLoadOptions.ShowTerminalOutput = true;

        // Open the TeX file as a stream and load it into a PDF Document
        using (FileStream texStream = File.OpenRead(texFilePath))
        {
            // Document constructor that accepts a stream and LoadOptions
            using (Document pdfDocument = new Document(texStream, texLoadOptions))
            {
                // Save the generated PDF to the specified path
                pdfDocument.Save(pdfOutputPath);
            }
        }

        Console.WriteLine($"PDF successfully created at '{pdfOutputPath}'.");
    }
}