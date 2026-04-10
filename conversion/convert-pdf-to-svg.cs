using System;
using System.IO;
using Aspose.Pdf;   // Document, SvgSaveOptions

class Program
{
    static void Main()
    {
        // Directory that contains the source PDF and where the SVG will be written.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Input PDF file.
        string pdfPath = Path.Combine(dataDir, "input.pdf");

        // Output SVG file.
        string svgPath = Path.Combine(dataDir, "output.svg");

        // Verify that the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block to ensure proper disposal.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Initialize SvgSaveOptions – this replaces the deprecated SvgDevice.
            SvgSaveOptions saveOptions = new SvgSaveOptions();

            // Example option: scale the output from points to pixels.
            // (There is no explicit CSS‑embedding option for SVG; this demonstrates
            //  enabling a useful feature while using SvgSaveOptions.)
            saveOptions.ScaleToPixels = true;

            // Save the document as SVG using the configured options.
            pdfDoc.Save(svgPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to SVG: {svgPath}");
    }
}