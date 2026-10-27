using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "filtered.pdf";

        // The name of the user whose annotations should be kept
        string currentUser = Environment.UserName; // or any other source

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Collect annotations that do NOT belong to the current user
                List<Annotation> toRemove = new List<Annotation>();
                foreach (Annotation annot in page.Annotations)
                {
                    bool keep = false;
                    // Author information is stored in the Subject property of MarkupAnnotation
                    if (annot is MarkupAnnotation markup && !string.IsNullOrEmpty(markup.Subject))
                    {
                        if (string.Equals(markup.Subject, currentUser, StringComparison.OrdinalIgnoreCase))
                            keep = true;
                    }
                    // If the annotation is not a MarkupAnnotation or its Subject does not match, remove it
                    if (!keep)
                        toRemove.Add(annot);
                }

                // Remove the collected annotations
                foreach (Annotation annot in toRemove)
                {
                    page.Annotations.Remove(annot);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations filtered. Output saved to '{outputPath}'.");
    }
}
