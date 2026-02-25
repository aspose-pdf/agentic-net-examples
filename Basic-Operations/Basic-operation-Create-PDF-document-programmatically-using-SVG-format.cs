using System;
using System.IO;
using Aspose.Pdf;                 // Core PDF API
using Aspose.Pdf.Facades;        // Facade namespace (included as required)

class Program
{
    static void Main()
    {
        const string inputSvg   = "input.svg";   // Path to the source SVG file
        const string outputPdf  = "output.pdf";  // Desired PDF output path

        // Verify that the SVG file exists before proceeding
        if (!File.Exists(inputSvg))
        {
            Console.Error.WriteLine($"Error: SVG file not found – {inputSvg}");
            return;
        }

        try
        {
            // Load the SVG using the dedicated load options.
            // This avoids any GDI+ dependencies and works cross‑platform.
            SvgLoadOptions loadOptions = new SvgLoadOptions();

            // Wrap the Document in a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputSvg, loadOptions))
            {
                // Save the loaded SVG as a PDF file.
                pdfDoc.Save(outputPdf);
            }

            Console.WriteLine($"SVG successfully converted to PDF: '{outputPdf}'");
        }
        catch (Exception ex)
        {
            // Catch any conversion‑related exceptions and report them.
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}