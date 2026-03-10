using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Enable System.Drawing support on non‑Windows platforms (required by Aspose.Pdf when saving to SVG).
        // This switch must be set **before** any Aspose.Pdf types are touched.
        AppContext.SetSwitch("System.Drawing.EnableUnixSupport", true);

        const string inputPdfPath  = "input.pdf";
        const string outputSvgPath = "output.svg";
        const string outputPdfPath = "output.pdf"; // fallback for non‑Windows platforms

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the existing PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Select the page where the caret annotation will be placed (first page in this example)
            Page page = pdfDoc.Pages[1];

            // Define the rectangle that bounds the caret annotation
            var rect = new Rectangle(100, 500, 120, 520);

            // Create the caret annotation
            var caret = new CaretAnnotation(page, rect)
            {
                Color    = Color.Red,          // Annotation border color
                Contents = "Caret annotation example", // Tooltip text
                Symbol   = CaretSymbol.Paragraph   // Optional symbol
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(caret);

            // Try to save as SVG on Windows. On Linux/macOS the SVG writer needs libgdiplus;
            // if it is not present the call throws a PdfException. We catch it and fall back to PDF.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var svgOptions = new SvgSaveOptions();
                pdfDoc.Save(outputSvgPath, svgOptions);
                Console.WriteLine($"Caret annotation added and saved as SVG to '{outputSvgPath}'.");
            }
            else
            {
                try
                {
                    var svgOptions = new SvgSaveOptions();
                    pdfDoc.Save(outputSvgPath, svgOptions);
                    Console.WriteLine($"Caret annotation added and saved as SVG to '{outputSvgPath}'.");
                }
                catch (Aspose.Pdf.PdfException ex)
                {
                    // Most likely libgdiplus is missing. Save as PDF instead.
                    Console.Error.WriteLine($"SVG export failed: {ex.Message}\nSaving as PDF instead.");
                    pdfDoc.Save(outputPdfPath);
                    Console.WriteLine($"Caret annotation added and saved as PDF to '{outputPdfPath}'.");
                }
            }
        }
    }
}
