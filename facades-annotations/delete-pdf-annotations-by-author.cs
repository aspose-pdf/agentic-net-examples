using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string authorToDelete = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the annotation editor, load the PDF, filter and delete annotations, then save.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document into the editor.
            editor.BindPdf(inputPath);

            // Collect the names of annotations whose Title (author) matches the target.
            List<string> namesToDelete = new List<string>();

            // The underlying Document is accessible via the Document property.
            Document doc = editor.Document;

            // Iterate through all pages (1‑based indexing).
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                AnnotationCollection annots = page.Annotations;

                // Iterate through the annotations collection (1‑based indexing).
                for (int i = 1; i <= annots.Count; i++)
                {
                    Annotation annot = annots[i];

                    // Title is defined only on markup annotations, so cast first.
                    if (annot is MarkupAnnotation markup &&
                        !string.IsNullOrEmpty(markup.Title) &&
                        markup.Title.Equals(authorToDelete, StringComparison.OrdinalIgnoreCase))
                    {
                        // Name property uniquely identifies the annotation for DeleteAnnotation.
                        if (!string.IsNullOrEmpty(annot.Name))
                        {
                            namesToDelete.Add(annot.Name);
                        }
                    }
                }
            }

            // Delete each matching annotation by its name.
            foreach (string name in namesToDelete)
            {
                editor.DeleteAnnotation(name);
            }

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations authored by '{authorToDelete}' have been removed. Saved to '{outputPath}'.");
    }
}
