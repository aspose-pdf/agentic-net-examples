using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string cgmPath      = "input.cgm";      // CGM source file (input‑only format)
        const string fdfPath      = "annotations.fdf";// FDF file containing annotations
        const string outputPdfPath = "output.pdf";    // Resulting PDF with imported annotations

        // Verify input files exist
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Load the CGM file as a PDF document (CGM is input‑only, so we convert it on load)
        using (Document doc = new Document(cgmPath, new CgmLoadOptions()))
        {
            // Open the FDF stream and import its annotations into the document
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                FdfReader.ReadAnnotations(fdfStream, doc);
            }

            // Save the annotated document as a PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations imported and PDF saved to '{outputPdfPath}'.");
    }
}