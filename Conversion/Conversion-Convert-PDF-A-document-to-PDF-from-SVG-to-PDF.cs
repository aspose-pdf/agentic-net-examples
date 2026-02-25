using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input files
        const string pdfaInputPath = "input_pdfa.pdf";   // PDF/A document
        const string svgInputPath  = "input.svg";        // SVG file

        // Output files
        const string pdfFromPdfaPath = "output_from_pdfa.pdf";
        const string pdfFromSvgPath  = "output_from_svg.pdf";

        // Verify input files exist
        if (!File.Exists(pdfaInputPath))
        {
            Console.Error.WriteLine($"PDF/A source not found: {pdfaInputPath}");
            return;
        }
        if (!File.Exists(svgInputPath))
        {
            Console.Error.WriteLine($"SVG source not found: {svgInputPath}");
            return;
        }

        // -------------------------------------------------
        // 1) Convert PDF/A to regular PDF (remove compliance)
        // -------------------------------------------------
        using (Document pdfaDoc = new Document(pdfaInputPath))
        {
            // Remove PDF/A compliance flags – results in a standard PDF
            pdfaDoc.RemovePdfaCompliance();

            // Save as regular PDF
            pdfaDoc.Save(pdfFromPdfaPath);
        }
        Console.WriteLine($"PDF/A converted to PDF: {pdfFromPdfaPath}");

        // -------------------------------------------------
        // 2) Convert SVG to PDF
        // -------------------------------------------------
        // Load the SVG using SvgLoadOptions (source format is SVG)
        using (Document svgDoc = new Document(svgInputPath, new SvgLoadOptions()))
        {
            // Save directly as PDF (no SaveOptions needed for PDF output)
            svgDoc.Save(pdfFromSvgPath);
        }
        Console.WriteLine($"SVG converted to PDF: {pdfFromSvgPath}");
    }
}