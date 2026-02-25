using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facade namespace is available, though not required for this conversion

class Program
{
    static void Main()
    {
        const string svgInputPath  = "input.svg";
        const string pdfOutputPath = "output.pdf";
        const string logPath       = "conversion_log.xml";

        if (!File.Exists(svgInputPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgInputPath}");
            return;
        }

        try
        {
            // Load the SVG file using SvgLoadOptions (no ambiguous using directives)
            SvgLoadOptions loadOptions = new SvgLoadOptions();

            // Wrap the Document in a using block for deterministic disposal
            using (Document doc = new Document(svgInputPath, loadOptions))
            {
                // Convert the loaded document to PDF/E format.
                // PdfFormat.PDF_E_1 (or PDF_E_1B depending on the Aspose.Pdf version) represents the engineering PDF format.
                doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

                // Save the converted PDF/E document.
                doc.Save(pdfOutputPath);
            }

            Console.WriteLine($"SVG successfully converted to PDF/E: {pdfOutputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}