using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string sourcePdfPath = "source.pdf";          // PDF with annotations to export
        const string xfdfPath = "annotations.xfdf";        // XFDF file for export/import
        const string epubPath = "book.epub";               // EPUB to be converted to PDF
        const string outputPdfPath = "final_output.pdf";   // Resulting PDF

        // Verify input files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubPath}");
            return;
        }

        // 1. Export annotations from the source PDF to XFDF
        using (Document pdfWithAnnotations = new Document(sourcePdfPath))
        {
            pdfWithAnnotations.ExportAnnotationsToXfdf(xfdfPath);
        }

        // 2. Load the EPUB file and convert it to a PDF document
        using (Document epubAsPdf = new Document(epubPath, new EpubLoadOptions()))
        {
            // 3. Import the previously exported annotations into the EPUB‑derived PDF
            epubAsPdf.ImportAnnotationsFromXfdf(xfdfPath);

            // 4. Save the final PDF
            epubAsPdf.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations exported to '{xfdfPath}' and imported into PDF saved as '{outputPdfPath}'.");
    }
}