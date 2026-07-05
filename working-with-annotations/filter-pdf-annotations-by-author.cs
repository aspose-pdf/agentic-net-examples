using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "filtered.pdf";
        const string currentUser = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages in the document
            foreach (Page page in doc.Pages)
            {
                // Collect annotations that do NOT belong to the current user
                var toRemove = new List<Annotation>();
                foreach (Annotation ann in page.Annotations)
                {
                    // Title is defined only on markup annotations
                    if (ann is MarkupAnnotation markup)
                    {
                        // If the Title (author) does not match the current user, mark for removal
                        if (!string.Equals(markup.Title, currentUser, StringComparison.Ordinal))
                            toRemove.Add(ann);
                    }
                    else
                    {
                        // Non‑markup annotations have no author information – remove them as well
                        toRemove.Add(ann);
                    }
                }

                // Remove the collected annotations from the page
                foreach (var ann in toRemove)
                {
                    page.Annotations.Delete(ann);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Filtered PDF saved to '{outputPath}'.");
    }
}
