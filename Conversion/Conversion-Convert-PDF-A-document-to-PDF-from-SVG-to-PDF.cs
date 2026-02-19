using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Convert SVG to PDF
        string svgInput = "input.svg";
        string pdfFromSvg = "output_from_svg.pdf";

        if (File.Exists(svgInput))
        {
            // Load SVG with default options (you can change the conversion engine if needed)
            var svgLoadOptions = new SvgLoadOptions
            {
                // Example: use the new conversion engine
                // ConversionEngine = SvgLoadOptions.ConversionEngines.NewEngine
            };

            Document svgDoc = new Document(svgInput, svgLoadOptions);
            svgDoc.Save(pdfFromSvg);
            Console.WriteLine($"SVG converted to PDF: {pdfFromSvg}");
        }
        else
        {
            Console.WriteLine($"SVG file not found: {svgInput}");
        }

        // Convert PDF/A to regular PDF
        string pdfaInput = "input_pdfa.pdf";
        string pdfFromPdfa = "output_from_pdfa.pdf";

        if (File.Exists(pdfaInput))
        {
            Document pdfaDoc = new Document(pdfaInput);
            pdfaDoc.Save(pdfFromPdfa);
            Console.WriteLine($"PDF/A converted to PDF: {pdfFromPdfa}");
        }
        else
        {
            Console.WriteLine($"PDF/A file not found: {pdfaInput}");
        }
    }
}