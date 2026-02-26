using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string mhtPath   = "input.mht";
        const string pdfPath   = "output.pdf";
        const string xfdfPath  = "annotations.xfdf";

        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtPath}");
            return;
        }

        // Load the MHT file into a PDF document
        using (Document pdfDoc = new Document(mhtPath, new MhtLoadOptions()))
        {
            // Export any existing annotations to an XFDF file
            using (PdfAnnotationEditor exporter = new PdfAnnotationEditor())
            {
                exporter.BindPdf(pdfDoc);
                using (FileStream xfdfOut = File.Create(xfdfPath))
                {
                    exporter.ExportAnnotationsToXfdf(xfdfOut);
                }
            }

            // Import the annotations back from the XFDF file
            using (PdfAnnotationEditor importer = new PdfAnnotationEditor())
            {
                importer.BindPdf(pdfDoc);
                importer.ImportAnnotationsFromXfdf(xfdfPath);
                // Save the final PDF document
                importer.Save(pdfPath);
            }
        }

        Console.WriteLine($"PDF saved to '{pdfPath}'. XFDF saved to '{xfdfPath}'.");
    }
}