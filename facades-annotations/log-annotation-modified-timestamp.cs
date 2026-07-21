using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Determine the total number of pages (Aspose.Pdf uses 1‑based indexing)
            int pageCount = doc.Pages.Count;

            // Create a PdfAnnotationEditor bound to the loaded document
            PdfAnnotationEditor editor = new PdfAnnotationEditor(doc);

            // Prepare a prototype annotation with the new Modified timestamp.
            // TextAnnotation must be instantiated with a (Page, Rectangle) constructor.
            // Use the first page and a zero‑size rectangle as a placeholder.
            Page firstPage = doc.Pages[1];
            Aspose.Pdf.Rectangle placeholderRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            TextAnnotation prototype = new TextAnnotation(firstPage, placeholderRect)
            {
                Modified = DateTime.Now
            };

            // Apply the Modified timestamp to all annotations on all pages.
            editor.ModifyAnnotations(1, pageCount, prototype);

            // Save the updated PDF
            doc.Save(outputPath);
            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");

            // Log each annotation's name and its Modified timestamp
            Console.WriteLine("Annotation modification log:");
            for (int i = 1; i <= pageCount; i++)
            {
                Page page = doc.Pages[i];
                if (page.Annotations != null)
                {
                    foreach (Annotation ann in page.Annotations)
                    {
                        string name = ann.Name ?? "(unnamed)";
                        Console.WriteLine($"Page {i}, Annotation Name: {name}, Modified: {ann.Modified}");
                    }
                }
            }
        }
    }
}
