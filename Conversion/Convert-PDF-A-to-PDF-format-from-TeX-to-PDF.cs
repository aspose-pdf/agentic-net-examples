using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // -----------------------------------------------------------------
        // 1. Convert a TeX source file to a regular PDF document.
        // -----------------------------------------------------------------
        const string texInputPath  = "input.tex";   // Path to the TeX file
        const string pdfFromTexPath = "output_from_tex.pdf";

        if (!File.Exists(texInputPath))
        {
            Console.Error.WriteLine($"TeX source not found: {texInputPath}");
            return;
        }

        // Load the TeX file using TeXLoadOptions and save it as PDF.
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();   // default options
        using (Document texDoc = new Document(texInputPath, texLoadOptions))
        {
            // The document is saved as a standard PDF (non‑PDF/A).
            texDoc.Save(pdfFromTexPath);
        }

        Console.WriteLine($"TeX → PDF conversion completed: {pdfFromTexPath}");

        // -----------------------------------------------------------------
        // 2. Convert an existing PDF/A file to a regular PDF (remove PDF/A compliance).
        // -----------------------------------------------------------------
        const string pdfaInputPath = "input_pdfa.pdf";   // Path to the PDF/A file
        const string pdfOutputPath = "output_from_pdfa.pdf";

        if (!File.Exists(pdfaInputPath))
        {
            Console.Error.WriteLine($"PDF/A source not found: {pdfaInputPath}");
            return;
        }

        // Load the PDF/A document (no special load options required).
        using (Document pdfaDoc = new Document(pdfaInputPath))
        {
            // Remove PDF/A compliance flags if present.
            pdfaDoc.RemovePdfaCompliance();

            // Save as a regular PDF.
            pdfaDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"PDF/A → PDF conversion completed: {pdfOutputPath}");
    }
}