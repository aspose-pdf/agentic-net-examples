using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string annotatedPdfPath = "first_annotated.pdf";
        const string outputPdfPath = "merged.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        // Load the first PDF, add a text annotation, and save to a temporary file
        using (Document doc = new Document(firstPdfPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("First PDF contains no pages.");
                return;
            }

            // Define annotation rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the annotation and set its properties
            TextAnnotation annotation = new TextAnnotation(doc.Pages[1], rect)
            {
                Title = "Note",
                Contents = "Added by Aspose.Pdf.Facades example.",
                Open = true,
                Icon = TextIcon.Note
            };

            // Attach the annotation to the first page
            doc.Pages[1].Annotations.Add(annotation);

            // Save the annotated document to a temporary file
            doc.Save(annotatedPdfPath);
        }

        // Merge the annotated first PDF with the second PDF using PdfFileEditor (Facades)
        PdfFileEditor editor = new PdfFileEditor();
        string[] filesToMerge = { annotatedPdfPath, secondPdfPath };
        editor.Concatenate(filesToMerge, outputPdfPath);

        // Clean up the temporary annotated file
        try { File.Delete(annotatedPdfPath); } catch { }

        Console.WriteLine($"Merged PDF saved to '{outputPdfPath}'.");
    }
}
