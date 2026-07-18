using System;
using System.IO;
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

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Annotations collection uses 1‑based indexing.
                // Iterate backwards to safely delete items.
                for (int idx = page.Annotations.Count; idx >= 1; idx--)
                {
                    Annotation ann = page.Annotations[idx];

                    // Only markup annotations have a Title (the /T entry).
                    if (ann is MarkupAnnotation markup)
                    {
                        // Remove annotation if its author (Title) does not match the current user.
                        if (!string.Equals(markup.Title, currentUser, StringComparison.Ordinal))
                        {
                            page.Annotations.Delete(idx);
                        }
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Filtered PDF saved to '{outputPath}'.");
    }
}