using System;
using System.IO;
using Aspose.Pdf;               // Core API namespace

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string texFilePath   = "input.tex";
        const string pdfOutputPath = "output.pdf";

        // Verify source file exists
        if (!File.Exists(texFilePath))
        {
            Console.Error.WriteLine($"LaTeX source not found: {texFilePath}");
            return;
        }

        // Initialize TeX load options (default settings preserve equations as vector graphics)
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();

        // Load the .tex file and convert it to a PDF document
        using (Document pdfDocument = new Document(texFilePath, texLoadOptions))
        {
            // Save the resulting PDF
            pdfDocument.Save(pdfOutputPath);
        }

        Console.WriteLine($"LaTeX file converted successfully to '{pdfOutputPath}'.");
    }
}