using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facades namespace as requested

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputSvg = "output.svg";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Optional: demonstrate usage of a Facades class (PdfConverter)
            // Here we simply bind the document; no conversion is performed by the converter.
            PdfConverter converter = new PdfConverter();
            converter.BindPdf(pdfDoc);
            // The converter can be closed when no longer needed.
            converter.Close();

            // Initialize SVG save options (no extra configuration needed for a basic conversion)
            SvgSaveOptions svgOptions = new SvgSaveOptions();

            // Save the PDF as SVG using the options object.
            pdfDoc.Save(outputSvg, svgOptions);
        }

        Console.WriteLine($"PDF successfully converted to SVG: {outputSvg}");
    }
}