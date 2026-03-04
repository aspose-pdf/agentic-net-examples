using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, PsLoadOptions, etc.)

class Program
{
    static void Main()
    {
        // Input PostScript file and desired PDF output file.
        const string psInputPath  = "input.ps";
        const string pdfOutputPath = "output.pdf";

        // Verify that the source file exists.
        if (!File.Exists(psInputPath))
        {
            Console.Error.WriteLine($"File not found: {psInputPath}");
            return;
        }

        // Load the PS file using PsLoadOptions (PS is input‑only).
        // The Document constructor that accepts a filename and LoadOptions is the correct way.
        using (Document pdfDoc = new Document(psInputPath, new PsLoadOptions()))
        {
            // Save the loaded content as a PDF.
            pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"PostScript file '{psInputPath}' successfully converted to PDF '{pdfOutputPath}'.");
    }
}