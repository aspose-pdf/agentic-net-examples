using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "callout.pdf";
        const string xfdfPath = "callout.xfdf";
        const string outputPdfPath = "callout_imported.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define rectangle for the free‑text annotation (fully qualified)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // DefaultAppearance requires System.Drawing.Color
            DefaultAppearance da = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the FreeText annotation
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, da)
            {
                // Set visual properties (fully qualified Aspose.Pdf.Color)
                Color = Aspose.Pdf.Color.Yellow,
                // Set the intent to FreeTextCallout
                Intent = FreeTextIntent.FreeTextCallout,
                // Set the ending style of the callout line
                EndingStyle = LineEnding.OpenArrow,
                // Define the three points of the callout line
                Callout = new Aspose.Pdf.Point[]
                {
                    new Aspose.Pdf.Point(150, 540), // start point (inside the annotation)
                    new Aspose.Pdf.Point(200, 600), // knee point (bend)
                    new Aspose.Pdf.Point(250, 650)  // end point (arrow tip)
                },
                // Optional rich text content
                RichText = "<b>Callout annotation</b>"
            };

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the PDF with the annotation
            doc.Save(pdfPath);
        }

        // Export the annotation to XFDF using PdfAnnotationEditor
        PdfAnnotationEditor exporter = new PdfAnnotationEditor();
        exporter.BindPdf(pdfPath);
        using (FileStream xfdfStream = File.Create(xfdfPath))
        {
            exporter.ExportAnnotationsToXfdf(xfdfStream);
        }
        exporter.Close();

        // Import the XFDF back into a new PDF document
        using (Document importedDoc = new Document())
        {
            // Add a blank page (required before binding)
            importedDoc.Pages.Add();

            PdfAnnotationEditor importer = new PdfAnnotationEditor();
            importer.BindPdf(importedDoc);
            importer.ImportAnnotationsFromXfdf(xfdfPath);
            importer.Save(outputPdfPath);
            importer.Close();
        }

        Console.WriteLine("PDF with callout annotation created, exported to XFDF, and re‑imported successfully.");
    }
}