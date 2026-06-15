using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "filtered.pdf";
        const string currentUser = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Iterate through all pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    AnnotationCollection annotations = page.Annotations;

                    // Gather annotations that do NOT belong to the current user
                    List<Annotation> toRemove = new List<Annotation>();
                    foreach (Annotation ann in annotations)
                    {
                        // Only markup annotations have a Title (the /T entry)
                        if (ann is MarkupAnnotation markup)
                        {
                            // Compare author name (case‑insensitive)
                            if (!string.Equals(markup.Title, currentUser, StringComparison.OrdinalIgnoreCase))
                            {
                                toRemove.Add(ann);
                            }
                        }
                        else
                        {
                            // Non‑markup annotations are removed as they lack an author
                            toRemove.Add(ann);
                        }
                    }

                    // Remove the collected annotations
                    foreach (Annotation ann in toRemove)
                    {
                        annotations.Delete(ann);
                    }
                }

                // Save the filtered document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Filtered PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}