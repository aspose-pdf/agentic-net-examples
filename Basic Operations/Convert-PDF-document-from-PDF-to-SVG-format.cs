using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputSvg = "output.svg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Initialize SVG save options (default constructor)
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                // Example of optional settings (uncomment if needed)
                // svgOptions.CompressOutputToZipArchive = true;
                // svgOptions.ScaleToPixels = true;

                // Save the document as SVG using the options
                pdfDoc.Save(outputSvg, svgOptions);
            }

            Console.WriteLine($"PDF successfully converted to SVG: {outputSvg}");
        }
        // SVG conversion may rely on Windows‑only GDI+; handle platform limitation gracefully
        catch (TypeInitializationException)
        {
            Console.WriteLine("SVG conversion requires Windows GDI+ and cannot be performed on this platform.");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"Aspose.Pdf error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}