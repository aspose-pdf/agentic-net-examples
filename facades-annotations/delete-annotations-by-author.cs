using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string targetAuthor = "John Doe";

        // ------------------------------------------------------------
        // Ensure a PDF exists – create a minimal sample if it does not.
        // ------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            CreateSamplePdf(inputPath, targetAuthor);
            Console.WriteLine($"Sample PDF created at '{inputPath}'.");
        }

        // Use PdfAnnotationEditor (facade) to work with annotations.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document.
            editor.BindPdf(inputPath);

            // Access the underlying Document to inspect annotations.
            Document doc = editor.Document;

            // Collect names of annotations whose Title (author) matches the target.
            List<string> namesToDelete = new List<string>();
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                foreach (Annotation annotation in page.Annotations)
                {
                    // Title is defined only on markup annotations, so cast first.
                    if (annotation is MarkupAnnotation markup &&
                        markup.Title == targetAuthor &&
                        !string.IsNullOrEmpty(annotation.Name))
                    {
                        namesToDelete.Add(annotation.Name);
                    }
                }
            }

            // Delete each matching annotation by its unique name.
            foreach (string name in namesToDelete)
            {
                editor.DeleteAnnotation(name);
            }

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations authored by '{targetAuthor}' have been removed. Output saved to '{outputPath}'.");
    }

    /// <summary>
    /// Creates a very small PDF containing a single markup annotation whose Title is set to <paramref name="author"/>.
    /// This helper is only used when the expected input file is missing, allowing the sample to run without external resources.
    /// </summary>
    private static void CreateSamplePdf(string path, string author)
    {
        // Create a new empty document.
        using (Document doc = new Document())
        {
            // Add a page.
            Page page = doc.Pages.Add();

            // Create a simple text annotation.
            TextAnnotation txtAnn = new TextAnnotation(page, new Rectangle(100, 600, 200, 650))
            {
                Title = author,               // Author name – this is what we will filter on.
                Name = "SampleAnnotation",   // Unique name required for DeleteAnnotation.
                Contents = "Sample annotation created for demo purposes."
            };

            // Add the annotation to the page.
            page.Annotations.Add(txtAnn);

            // Save the document.
            doc.Save(path);
        }
    }
}
