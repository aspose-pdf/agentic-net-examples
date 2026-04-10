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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the annotation editor with the loaded document
            PdfAnnotationEditor editor = new PdfAnnotationEditor(doc);

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Each page has an AnnotationCollection
                foreach (Annotation annotation in page.Annotations)
                {
                    // The Name property holds the annotation's identifier
                    string name = annotation.Name ?? "(no name)";
                    Console.WriteLine($"Page {pageNum} - Annotation Name: {name}");
                }
            }

            // No changes are made; we only output names for debugging.
        }
    }
}