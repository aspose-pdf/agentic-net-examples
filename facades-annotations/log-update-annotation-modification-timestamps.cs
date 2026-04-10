using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_timestamped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Log existing annotation modification timestamps
            Console.WriteLine("Existing annotation timestamps:");
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];
                    Console.WriteLine($"Page {pageNum}, Annotation {annIdx}, Name='{ann.Name}', Modified={ann.Modified}");
                }
            }

            // Create a PdfAnnotationEditor and bind it to the loaded document
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);

                // Prepare a dummy TextAnnotation with the required (Page, Rectangle) constructor.
                // The rectangle can be zero‑size because we only need the Modified property.
                Page firstPage = doc.Pages[1];
                Aspose.Pdf.Rectangle dummyRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
                TextAnnotation timestampAnnotation = new TextAnnotation(firstPage, dummyRect)
                {
                    Modified = DateTime.Now
                };

                // Apply the new Modified value to all annotations on all pages (1‑based indexing)
                editor.ModifyAnnotations(1, doc.Pages.Count, timestampAnnotation);

                // Save the modified document via the editor
                editor.Save(outputPath);
            }
        }

        // Reload the saved PDF to verify that timestamps were updated
        using (Document updatedDoc = new Document(outputPath))
        {
            Console.WriteLine("\nUpdated annotation timestamps:");
            for (int pageNum = 1; pageNum <= updatedDoc.Pages.Count; pageNum++)
            {
                Page page = updatedDoc.Pages[pageNum];
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];
                    Console.WriteLine($"Page {pageNum}, Annotation {annIdx}, Name='{ann.Name}', Modified={ann.Modified}");
                }
            }
        }

        Console.WriteLine($"\nAnnotation timestamps logged and PDF saved to '{outputPath}'.");
    }
}
