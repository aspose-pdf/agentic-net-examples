using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, SvgLoadOptions, etc.)

class Program
{
    static void Main()
    {
        // ---------- Convert PDF/A to regular PDF ----------
        const string pdfaInputPath  = "input_pdfa.pdf";   // PDF/A source file
        const string pdfOutputPath  = "output.pdf";      // Desired regular PDF output

        if (!File.Exists(pdfaInputPath))
        {
            Console.Error.WriteLine($"PDF/A source not found: {pdfaInputPath}");
            return;
        }

        // Load the PDF/A document, remove its PDF/A compliance, and save as a normal PDF.
        using (Document pdfaDoc = new Document(pdfaInputPath))
        {
            // This method strips the PDF/A specific compliance flags,
            // effectively converting the document to a regular PDF.
            pdfaDoc.RemovePdfaCompliance();

            // Save without specifying SaveOptions because the target format is PDF.
            pdfaDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"PDF/A converted to PDF: '{pdfOutputPath}'");

        // ---------- Convert SVG to PDF ----------
        const string svgInputPath   = "input.svg";        // SVG source file
        const string svgPdfPath     = "svg_converted.pdf"; // PDF output for the SVG

        if (!File.Exists(svgInputPath))
        {
            Console.Error.WriteLine($"SVG source not found: {svgInputPath}");
            return;
        }

        // Load the SVG using SvgLoadOptions (required for SVG input).
        SvgLoadOptions svgLoadOptions = new SvgLoadOptions();

        using (Document svgDoc = new Document(svgInputPath, svgLoadOptions))
        {
            // Save the loaded SVG as a PDF document.
            svgDoc.Save(svgPdfPath);
        }

        Console.WriteLine($"SVG converted to PDF: '{svgPdfPath}'");
    }
}