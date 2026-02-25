using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Path to the source TeX file (could be any .tex file)
        const string texPath = "input.tex";
        // Path where the resulting PDF will be saved
        const string pdfPath = "output.pdf";

        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"TeX file not found: {texPath}");
            return;
        }

        // Open the TeX file as a read‑only stream
        using (FileStream texStream = File.OpenRead(texPath))
        {
            // Optional: configure loading options for TeX processing
            TeXLoadOptions texLoadOptions = new TeXLoadOptions
            {
                // Example: disable font license checks if needed
                // DisableFontLicenseVerifications = true,
                // Show terminal output for debugging
                // ShowTerminalOutput = true
            };

            // Load the TeX content into a PDF Document using the stream and load options
            using (Document pdfDoc = new Document(texStream, texLoadOptions))
            {
                // Save the generated PDF to the desired location
                pdfDoc.Save(pdfPath);
            }
        }

        Console.WriteLine($"TeX file '{texPath}' successfully converted to PDF '{pdfPath}'.");
    }
}