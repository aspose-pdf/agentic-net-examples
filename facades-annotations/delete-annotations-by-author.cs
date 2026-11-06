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
        const string outputPath = "output.pdf";
        const string targetAuthor = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDoc = new Document(inputPath);

        // Iterate over each page.
        foreach (Page page in pdfDoc.Pages)
        {
            // Gather annotations whose Author matches the target.
            var toRemove = new List<Annotation>();

            foreach (Annotation annot in page.Annotations)
            {
                // The Author property is defined on many annotation types.
                // Use reflection to avoid compile‑time errors on versions where it may be missing.
                var authorProp = annot.GetType().GetProperty("Author");
                if (authorProp != null)
                {
                    string author = authorProp.GetValue(annot) as string;
                    if (string.Equals(author, targetAuthor, StringComparison.OrdinalIgnoreCase))
                    {
                        toRemove.Add(annot);
                    }
                }
            }

            // Remove the collected annotations.
            foreach (Annotation annot in toRemove)
            {
                // AnnotationCollection implements IList<T>, so Remove works safely.
                page.Annotations.Remove(annot);
            }
        }

        // Save the modified PDF.
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Annotations authored by \"{targetAuthor}\" have been removed.");
        Console.WriteLine($"Result saved to \"{outputPath}\".");
    }
}
