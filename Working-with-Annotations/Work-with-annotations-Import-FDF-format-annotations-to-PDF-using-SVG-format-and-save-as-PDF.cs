using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string svgInputPath   = "input.svg";   // SVG source file
        const string fdfAnnotations = "annotations.fdf"; // FDF file containing annotations
        const string pdfOutputPath  = "output.pdf";   // Resulting PDF with annotations

        // Verify input files exist
        if (!File.Exists(svgInputPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgInputPath}");
            return;
        }
        if (!File.Exists(fdfAnnotations))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfAnnotations}");
            return;
        }

        // Load SVG as a PDF document
        using (Document pdfDoc = new Document(svgInputPath, new SvgLoadOptions()))
        {
            // Import annotations from the FDF file into the document
            using (FileStream fdfStream = File.OpenRead(fdfAnnotations))
            {
                FdfReader.ReadAnnotations(fdfStream, pdfDoc);
            }

            // Save the annotated document as PDF
            pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"SVG converted to PDF with imported annotations saved to '{pdfOutputPath}'.");
    }
}