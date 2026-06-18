using System;
using System.IO;
using System.Collections.Generic;
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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfAnnotationEditor facade (also disposable)
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Collect annotation names whose Title (author) matches the target
                List<string> namesToDelete = new List<string>();

                // Pages are 1‑based in Aspose.Pdf
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    AnnotationCollection annots = doc.Pages[pageNum].Annotations;

                    // Annotation collection is also 1‑based
                    for (int i = 1; i <= annots.Count; i++)
                    {
                        Annotation ann = annots[i];
                        // Title exists only on markup annotations; cast accordingly
                        if (ann is MarkupAnnotation markup &&
                            !string.IsNullOrEmpty(markup.Title) &&
                            markup.Title.Equals(targetAuthor, StringComparison.OrdinalIgnoreCase))
                        {
                            // Name uniquely identifies the annotation; ensure it's not empty
                            if (!string.IsNullOrEmpty(ann.Name))
                                namesToDelete.Add(ann.Name);
                        }
                    }
                }

                // Delete each matching annotation by its name using the facade method
                foreach (string name in namesToDelete)
                {
                    editor.DeleteAnnotation(name);
                }

                // Save the modified PDF (lifecycle rule: use Save with explicit path)
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Annotations authored by '{targetAuthor}' have been removed. Saved to '{outputPath}'.");
    }
}
