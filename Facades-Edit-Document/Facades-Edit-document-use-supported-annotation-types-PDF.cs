using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Drawing; // BorderStyle enum

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Use PdfAnnotationEditor (Facades) to edit annotations
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the loaded document to the editor
            editor.BindPdf(pdfDocument);

            // -------------------------------------------------
            // Add a TextAnnotation (sticky note) on the first page
            // -------------------------------------------------
            Page firstPage = pdfDocument.Pages[1];
            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);
            TextAnnotation textAnnot = new TextAnnotation(firstPage, textRect);
            textAnnot.Contents = "Sample note";
            textAnnot.Color = Color.Yellow;          // Annotation background color
            textAnnot.Icon = TextIcon.Comment;       // Icon displayed for the note

            // Initialize the border using the provided rule
            // {AnnotationVar}.Border = new Border({AnnotationVar}) { Style = BorderStyle.{Style}, Width = {Width} };
            textAnnot.Border = new Border(textAnnot)
            {
                Style = BorderStyle.Solid,
                Width = 1
            };

            // Add the annotation to the page
            firstPage.Annotations.Add(textAnnot);

            // -------------------------------------------------
            // Add a RedactionAnnotation on the second page (if it exists)
            // -------------------------------------------------
            if (pdfDocument.Pages.Count >= 2)
            {
                Page secondPage = pdfDocument.Pages[2];
                Aspose.Pdf.Rectangle redactRect = new Aspose.Pdf.Rectangle(200, 400, 300, 500);
                RedactionAnnotation redAnnot = new RedactionAnnotation(secondPage, redactRect);
                redAnnot.Color = Color.LightGray;      // Border color when not active
                redAnnot.FillColor = Color.LightGray;  // Fill color
                redAnnot.OverlayText = "Redacted";

                // Initialize the border for the redaction annotation
                redAnnot.Border = new Border(redAnnot)
                {
                    Style = BorderStyle.Dashed,
                    Width = 2
                };

                // Add the redaction annotation to the page
                secondPage.Annotations.Add(redAnnot);
            }

            // Save the modified document using the provided document-save rule
            // {DocumentVar}.Save({OutputPath});
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to: {outputPath}");
    }
}