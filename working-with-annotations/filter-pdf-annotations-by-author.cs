using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "filtered.pdf";
        const string currentUser = "John Doe"; // author name to keep

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                AnnotationCollection annots = page.Annotations;

                // Collect annotations to remove (cannot modify collection while iterating)
                List<Annotation> toRemove = new List<Annotation>();

                foreach (Annotation annot in annots)
                {
                    // Only markup annotations have a Title (the /T entry)
                    if (annot is MarkupAnnotation markup)
                    {
                        // If the annotation's author (Title) does not match the current user, mark for removal
                        if (!string.Equals(markup.Title, currentUser, StringComparison.OrdinalIgnoreCase))
                        {
                            toRemove.Add(annot);
                        }
                    }
                    else
                    {
                        // Non‑markup annotations are removed as they have no author information
                        toRemove.Add(annot);
                    }
                }

                // Remove the unwanted annotations
                foreach (Annotation unwanted in toRemove)
                {
                    annots.Delete(unwanted);
                }
            }

            // Save the filtered PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Filtered PDF saved to '{outputPath}'.");
    }
}