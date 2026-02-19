using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input TeX file (PDF/A source is generated from TeX)
        const string texPath = "input.tex";
        // Output PDF file
        const string pdfPath = "output.pdf";

        // Verify that the TeX source file exists
        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"Error: TeX file not found at '{texPath}'.");
            return;
        }

        try
        {
            // Load the TeX file using default TeXLoadOptions
            Document pdfDocument = new Document(texPath, new TeXLoadOptions());

            // Save the document as a regular PDF
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"Conversion completed successfully. PDF saved to '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}