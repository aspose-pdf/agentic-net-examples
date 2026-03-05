using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string svgPath = "input.svg";          // Source SVG file
        const string pdfEPath = "output_pdfe1.pdf"; // Destination PDF/E‑1 file
        const string logPath = "conversion.log";    // Log file for conversion messages

        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        // Load the SVG using SvgLoadOptions.
        // The ConversionEngine field lets you choose the conversion engine (LegacyEngine or NewEngine).
        SvgLoadOptions loadOptions = new SvgLoadOptions();
        loadOptions.ConversionEngine = SvgLoadOptions.ConversionEngines.NewEngine; // optional, choose engine

        // Wrap the Document in a using block for deterministic disposal.
        using (Document doc = new Document(svgPath, loadOptions))
        {
            // Convert the loaded document to PDF/E‑1.
            // PdfFormat.PDF_E_1 is the enum value for PDF/E‑1.
            // ConvertErrorAction.Delete tells the converter to skip objects it cannot convert.
            doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

            // Save the converted document as a PDF/E‑1 file.
            doc.Save(pdfEPath);
        }

        Console.WriteLine($"SVG successfully converted to PDF/E‑1: {pdfEPath}");
    }
}