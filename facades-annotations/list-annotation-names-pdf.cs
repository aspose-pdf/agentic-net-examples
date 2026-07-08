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

        // Ensure a PDF exists – if not, create a simple one with a sample annotation
        if (!File.Exists(inputPath))
        {
            CreateSamplePdfWithAnnotation(inputPath);
            Console.WriteLine($"Sample PDF created at '{inputPath}'.");
        }

        // Initialize the annotation editor and bind the PDF file
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Access the underlying Document object
            Document doc = editor.Document;

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                // Get the annotation collection for the current page
                AnnotationCollection annotations = doc.Pages[pageIndex].Annotations;

                // Output each annotation's Name property
                foreach (Annotation annotation in annotations)
                {
                    Console.WriteLine($"Page {pageIndex} - Annotation Name: {annotation.Name}");
                }
            }
        }
    }

    private static void CreateSamplePdfWithAnnotation(string path)
    {
        // Create a new PDF document with a single page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Add a simple text annotation
            TextAnnotation txtAnn = new TextAnnotation(page, new Rectangle(100, 600, 200, 650))
            {
                Title = "Sample",
                Contents = "This is a sample annotation",
                Name = "SampleAnnotation"
            };
            page.Annotations.Add(txtAnn);

            // Save the document to the specified path
            doc.Save(path);
        }
    }
}
