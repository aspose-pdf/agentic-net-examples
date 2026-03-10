using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the source XPS file, the FDF annotation file, and the output PDF.
        const string xpsPath   = "source.xps";
        const string fdfPath   = "annotations.fdf";
        const string outputPdf = "result.pdf";

        // Verify that the input files exist.
        if (!File.Exists(xpsPath))
        {
            Console.Error.WriteLine($"XPS file not found: {xpsPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Load the XPS document into a PDF Document instance.
        XpsLoadOptions xpsLoadOptions = new XpsLoadOptions();
        using (Document pdfDoc = new Document(xpsPath, xpsLoadOptions))
        {
            // Import annotations from the FDF file into the PDF document.
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                // FdfReader reads the FDF stream and adds the annotations to the document.
                FdfReader.ReadAnnotations(fdfStream, pdfDoc);
            }

            // Save the resulting PDF with the imported annotations.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with imported FDF annotations saved to '{outputPdf}'.");
    }
}