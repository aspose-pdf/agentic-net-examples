using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string firstPdf = "first.pdf";
        const string secondPdf = "second.pdf";
        const string outputPdf = "merged.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or more input PDF files were not found.");
            return;
        }

        // Temporary file to hold the first PDF after adding annotations
        string tempAnnotated = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

        // -------------------------------------------------
        // Step 1: Load the first PDF, add an annotation, and save it to a temporary file
        // -------------------------------------------------
        using (Document doc = new Document(firstPdf))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The first PDF contains no pages.");
                return;
            }

            // Define the annotation rectangle (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text annotation
            TextAnnotation txtAnn = new TextAnnotation(doc.Pages[1], rect)
            {
                Title = "Note",
                Contents = "Added via Aspose.Pdf",
                Color = Aspose.Pdf.Color.Yellow,
                Open = true,
                Icon = TextIcon.Note
            };

            // Add the annotation to the first page
            doc.Pages[1].Annotations.Add(txtAnn);

            // Save the annotated document to the temporary file
            doc.Save(tempAnnotated);
        }

        // -------------------------------------------------
        // Step 2: Merge the annotated first PDF with the second PDF using PdfFileEditor (Facades API)
        // -------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        try
        {
            // Concatenate: first (annotated) + second -> outputPdf
            bool merged = editor.Concatenate(tempAnnotated, secondPdf, outputPdf);
            if (!merged)
            {
                Console.Error.WriteLine("Failed to concatenate PDFs.");
                return;
            }
        }
        finally
        {
            // Clean up the temporary annotated file
            if (File.Exists(tempAnnotated))
            {
                File.Delete(tempAnnotated);
            }
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }
}