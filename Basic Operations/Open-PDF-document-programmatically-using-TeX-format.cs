using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Define the directory that contains the source TeX file.
        // Adjust this path as needed for your environment.
        string dataDir = Path.Combine(Environment.CurrentDirectory, "Data");

        // Path to the TeX source file.
        string texFile = Path.Combine(dataDir, "TeX-to-PDF.tex");

        // Path for the resulting PDF file.
        string pdfFile = Path.Combine(dataDir, "Tex-to-PDF.pdf");

        // Verify that the TeX source file exists before attempting to load it.
        if (!File.Exists(texFile))
        {
            Console.Error.WriteLine($"Error: TeX source file not found at '{texFile}'.");
            return;
        }

        // Initialize TeX load options (default settings are sufficient for most cases).
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();

        // Load the TeX file and convert it to a PDF document.
        using (Document pdfDocument = new Document(texFile, texLoadOptions))
        {
            // Save the generated PDF.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"Conversion completed. PDF saved to '{pdfFile}'.");
    }
}