using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string svgInputPath = "input.svg";
        const string pdfOutputPath = "output.pdf";

        if (!File.Exists(svgInputPath))
        {
            Console.Error.WriteLine($"Input file not found: {svgInputPath}");
            return;
        }

        // Load the SVG file. Optionally select the conversion engine.
        var svgLoadOptions = new SvgLoadOptions
        {
            // Choose the newer conversion engine; legacy engine is also available.
            ConversionEngine = SvgLoadOptions.ConversionEngines.NewEngine
        };

        // Load the SVG into a Document object.
        using (Document doc = new Document(svgInputPath, svgLoadOptions))
        {
            // If the source SVG was generated from a PDF/A document,
            // remove any PDF/A compliance flags to produce a regular PDF.
            doc.RemovePdfaCompliance();

            // Save the document as a standard PDF.
            doc.Save(pdfOutputPath);
        }

        Console.WriteLine($"SVG successfully converted to PDF: {pdfOutputPath}");
    }
}