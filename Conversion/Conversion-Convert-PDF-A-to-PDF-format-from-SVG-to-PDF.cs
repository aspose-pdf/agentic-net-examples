using System;
using System.IO;
using Aspose.Pdf; // SvgLoadOptions resides directly in this namespace

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        string svgInputPath = "input.svg";
        string pdfFromSvgPath = "output_from_svg.pdf";

        string pdfAInputPath = "input_pdfa.pdf";
        string pdfOutputPath = "output_converted.pdf";

        try
        {
            // ---------- Convert SVG to PDF ----------
            if (!File.Exists(svgInputPath))
                throw new FileNotFoundException($"SVG source file not found: {svgInputPath}");

            // Load the SVG using SvgLoadOptions. The ConversionEngine can be set to NewEngine or LegacyEngine.
            var svgLoadOptions = new SvgLoadOptions
            {
                // Use the newer conversion engine for better quality (optional)
                ConversionEngine = SvgLoadOptions.ConversionEngines.NewEngine
            };

            // Load the SVG document
            Document svgDoc = new Document(svgInputPath, svgLoadOptions);

            // Save as PDF (standard PDF format)
            svgDoc.Save(pdfFromSvgPath);
            Console.WriteLine($"SVG successfully converted to PDF: {pdfFromSvgPath}");

            // ---------- Convert PDF/A to regular PDF ----------
            if (!File.Exists(pdfAInputPath))
                throw new FileNotFoundException($"PDF/A source file not found: {pdfAInputPath}");

            // Load the PDF/A document. No special load options are required.
            Document pdfADoc = new Document(pdfAInputPath);

            // Optionally, you can enforce conversion to a specific PDF version using PdfFormatConversionOptions.
            // Here we simply save the document as a regular PDF.
            pdfADoc.Save(pdfOutputPath);
            Console.WriteLine($"PDF/A successfully converted to regular PDF: {pdfOutputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
