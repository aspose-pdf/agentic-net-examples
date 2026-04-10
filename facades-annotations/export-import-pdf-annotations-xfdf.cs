using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // <-- added namespace for TextFragment

class Program
{
    static void Main()
    {
        // Paths for source PDF, target PDF, intermediate XFDF file, and final output PDF
        const string sourcePdfPath   = "source.pdf";
        const string targetPdfPath   = "target.pdf";
        const string xfdfPath        = "annotations.xfdf";
        const string outputPdfPath   = "target_with_annotations.pdf";

        // ------------------------------------------------------------
        // 1. Create sample PDFs (source with an annotation, target empty)
        // ------------------------------------------------------------
        CreateSampleSourcePdf(sourcePdfPath);
        CreateSampleTargetPdf(targetPdfPath);

        // ------------------------------------------------------------
        // 2. Export all annotations from the source PDF to an XFDF file
        // ------------------------------------------------------------
        using (PdfAnnotationEditor exportEditor = new PdfAnnotationEditor())
        {
            // Bind the editor to the source PDF document
            exportEditor.BindPdf(sourcePdfPath);

            // Export annotations into the XFDF file (overwrites if it exists)
            using (FileStream xfdfStream = File.Create(xfdfPath))
            {
                exportEditor.ExportAnnotationsToXfdf(xfdfStream);
            }
        }

        // ------------------------------------------------------------
        // 3. Import the exported XFDF annotations into another PDF
        // ------------------------------------------------------------
        using (PdfAnnotationEditor importEditor = new PdfAnnotationEditor())
        {
            // Bind the editor to the target PDF (the document that will receive the annotations)
            importEditor.BindPdf(targetPdfPath);

            // Import all annotations from the XFDF file
            importEditor.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the modified PDF to a new file (original target PDF remains unchanged)
            importEditor.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations exported to '{xfdfPath}' and imported into '{outputPdfPath}'.");
    }

    /// <summary>
    /// Creates a simple source PDF containing one page and a text annotation.
    /// </summary>
    private static void CreateSampleSourcePdf(string path)
    {
        // Create a new document with a single blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Sample source PDF with an annotation."));

            // Add a text annotation so there is something to export
            TextAnnotation txtAnn = new TextAnnotation(page, new Aspose.Pdf.Rectangle(100, 600, 300, 650))
            {
                Title = "Sample",
                Subject = "Demo",
                Contents = "This is a sample annotation."
            };
            page.Annotations.Add(txtAnn);

            doc.Save(path);
        }
    }

    /// <summary>
    /// Creates a simple target PDF containing one empty page (no annotations).
    /// </summary>
    private static void CreateSampleTargetPdf(string path)
    {
        using (Document doc = new Document())
        {
            doc.Pages.Add(); // just an empty page
            doc.Save(path);
        }
    }
}
