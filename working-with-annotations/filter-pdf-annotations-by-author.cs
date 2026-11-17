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
        const string outputPath = "filtered_annotations.pdf";
        const string currentUser = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                var toRemove = new List<Annotation>();

                // Gather annotations whose author (stored in Subject) does not match the current user
                foreach (Annotation annot in page.Annotations)
                {
                    // Author information is stored in the Subject property of MarkupAnnotation
                    if (annot is MarkupAnnotation markup)
                    {
                        string author = markup.Subject ?? string.Empty;
                        if (!string.Equals(author, currentUser, StringComparison.OrdinalIgnoreCase))
                        {
                            toRemove.Add(annot);
                        }
                    }
                    else
                    {
                        // Non‑markup annotations have no author information – remove them
                        toRemove.Add(annot);
                    }
                }

                // Remove the unwanted annotations
                foreach (Annotation annot in toRemove)
                {
                    page.Annotations.Delete(annot);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Filtered PDF saved to '{outputPath}'.");
    }
}
