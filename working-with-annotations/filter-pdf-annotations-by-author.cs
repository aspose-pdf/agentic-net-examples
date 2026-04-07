using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "filtered.pdf";
        const string currentUser = "John Doe";

        // ------------------------------------------------------------
        // Ensure a PDF exists – create a simple one with a couple of
        // markup annotations (one for the current user, one for another).
        // ------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (Document sample = new Document())
            {
                // Add a blank page
                Page page = sample.Pages.Add();

                // Annotation belonging to the current user
                TextAnnotation ann1 = new TextAnnotation(page, new Rectangle(100, 700, 200, 750))
                {
                    Title = currentUser,
                    Contents = "Annotation for current user"
                };
                page.Annotations.Add(ann1);

                // Annotation belonging to a different user
                TextAnnotation ann2 = new TextAnnotation(page, new Rectangle(100, 600, 200, 650))
                {
                    Title = "Jane Smith",
                    Contents = "Annotation for another user"
                };
                page.Annotations.Add(ann2);

                sample.Save(inputPath);
            }
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                AnnotationCollection annots = page.Annotations;

                // Iterate backwards to safely delete by index
                for (int i = annots.Count; i >= 1; i--)
                {
                    Annotation ann = annots[i];

                    // Only markup annotations have a Title (author) field
                    if (ann is MarkupAnnotation markup)
                    {
                        // Remove annotation if its author does not match the current user
                        if (!string.Equals(markup.Title, currentUser, StringComparison.Ordinal))
                        {
                            annots.Delete(i);
                        }
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations filtered. Output saved to '{outputPath}'.");
    }
}
