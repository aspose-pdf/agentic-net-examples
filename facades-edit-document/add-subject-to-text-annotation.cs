using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // Ensure the source PDF exists – create a minimal placeholder if it does not.
        // ---------------------------------------------------------------------
        if (!System.IO.File.Exists(inputPath))
        {
            using (var placeholder = new Document())
            {
                placeholder.Pages.Add(); // add a single blank page
                placeholder.Save(inputPath);
            }
        }

        // Load the PDF document to obtain a Page reference (Aspose.Pdf uses the Document class)
        Document pdfDoc = new Document(inputPath);
        Page referencePage = pdfDoc.Pages[1];
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 200, 720);

        // Create a TextAnnotation and set its Subject for easy searching/categorization
        TextAnnotation newAnnot = new TextAnnotation(referencePage, rect)
        {
            Subject  = "ReviewNote",                     // Subject used for searching/categorization
            Title    = "Reviewer",                       // Optional: author name
            Contents = "Please review this section.",    // Optional: visible comment text
            Color    = Aspose.Pdf.Color.Yellow,           // Optional: annotation colour
            Open     = true                               // Optional: display the annotation open by default
        };

        // Initialize the annotation editor facade and bind the source PDF
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Apply the annotation to the desired page range (here: all pages)
            editor.ModifyAnnotations(1, editor.Document.Pages.Count, newAnnot);

            // Save the updated PDF
            editor.Save(outputPath);
        }
    }
}
