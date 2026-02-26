using System;
using System.IO;
using Aspose.Pdf; // Document, XslFoLoadOptions, etc.

class Program
{
    static void Main()
    {
        // Paths to the source XSL‑FO file, the FDF annotation file and the output PDF.
        const string xslFoPath = "input.xslfo";
        const string fdfPath   = "annotations.fdf";
        const string outputPdf = "output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Load the XSL‑FO document and convert it to a PDF.
        XslFoLoadOptions loadOptions = new XslFoLoadOptions(); // default options
        using (Document pdfDoc = new Document(xslFoPath, loadOptions)) // deterministic disposal
        {
            // Import annotations from the FDF file into the PDF document.
            pdfDoc.ImportAnnotationsFromXfdf(fdfPath);

            // Save the resulting PDF with the imported annotations.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with imported annotations saved to '{outputPdf}'.");
    }
}