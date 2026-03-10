using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string svgInputPath = "input.svg";
        const string pdfOutputPath = "output.pdf";
        const string xfdfPath = "annotations.xfdf";

        if (!File.Exists(svgInputPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgInputPath}");
            return;
        }

        // Load the SVG file and convert it to a PDF document
        using (Document pdfDoc = new Document(svgInputPath, new SvgLoadOptions()))
        {
            // Save the initial PDF (optional, can be omitted if not needed)
            pdfDoc.Save(pdfOutputPath);

            // Export all annotations from the PDF to an XFDF file
            using (PdfAnnotationEditor exporter = new PdfAnnotationEditor())
            {
                exporter.BindPdf(pdfDoc);
                using (FileStream xfdfOut = File.Create(xfdfPath))
                {
                    exporter.ExportAnnotationsToXfdf(xfdfOut);
                }
            }

            // (Optional) modify the PDF or its annotations here

            // Import annotations back from the XFDF file into the PDF
            using (PdfAnnotationEditor importer = new PdfAnnotationEditor())
            {
                importer.BindPdf(pdfDoc);
                importer.ImportAnnotationsFromXfdf(xfdfPath);
                // Save the final PDF with imported annotations
                pdfDoc.Save(pdfOutputPath);
            }
        }

        Console.WriteLine($"PDF saved to '{pdfOutputPath}' with annotations imported from '{xfdfPath}'.");
    }
}