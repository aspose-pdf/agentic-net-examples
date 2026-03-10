using System;
using System.IO;
using Aspose.Pdf; // All Aspose.Pdf types (Document, PsLoadOptions, etc.) are in this namespace

class Program
{
    static void Main()
    {
        const string psInputPath  = "input.ps";   // Path to the source PostScript file
        const string pdfOutputPath = "output.pdf"; // Desired PDF output path

        // Verify that the PS file exists before attempting to load it
        if (!File.Exists(psInputPath))
        {
            Console.Error.WriteLine($"File not found: {psInputPath}");
            return;
        }

        // Load the PS file using PsLoadOptions (PS is input‑only in Aspose.Pdf)
        // The Document is wrapped in a using block for deterministic disposal
        using (Document doc = new Document(psInputPath, new PsLoadOptions()))
        {
            // Save the loaded document as PDF.
            // Document.Save(string) without SaveOptions always writes PDF regardless of extension.
            doc.Save(pdfOutputPath);
        }

        Console.WriteLine($"Successfully converted '{psInputPath}' to PDF at '{pdfOutputPath}'.");
    }
}