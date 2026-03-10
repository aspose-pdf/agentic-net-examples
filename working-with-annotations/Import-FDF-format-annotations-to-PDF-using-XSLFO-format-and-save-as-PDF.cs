using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source XSL-FO file, the FDF annotation file, and the output PDF.
        const string xslFoPath   = "input.fo";
        const string fdfPath     = "annotations.fdf";
        const string outputPdf   = "output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL-FO file not found: {xslFoPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Load the XSL-FO file into a PDF document.
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();
        using (Document pdfDoc = new Document(xslFoPath, loadOptions))
        {
            // Import annotations from the FDF file into the loaded PDF.
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                FdfReader.ReadAnnotations(fdfStream, pdfDoc);
            }

            // Save the resulting PDF with the imported annotations.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with imported annotations saved to '{outputPdf}'.");
    }
}