using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Iterate through all pages
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];
                if (page.Annotations == null) continue;

                foreach (Annotation annotation in page.Annotations)
                {
                    // Use a reliable identifier (Name if set, otherwise the annotation type)
                    string identifier = !string.IsNullOrEmpty(annotation.Name) ? annotation.Name : annotation.AnnotationType.ToString();

                    // Only markup annotations have a modification timestamp and color property
                    if (annotation is MarkupAnnotation markup)
                    {
                        // Log the existing modification timestamp (if any)
                        Console.WriteLine($"Page {pageNum}, Annotation {identifier}: Modified = {markup.Modified}");

                        // Example modification: change annotation color to yellow
                        markup.Color = Color.Yellow;

                        // Update the modification timestamp to the current UTC time
                        markup.Modified = DateTime.UtcNow;

                        // Log the update
                        Console.WriteLine($"Updated annotation {identifier}: Modified = {markup.Modified}");
                    }
                    else
                    {
                        // Non‑markup annotations do not expose a Modified property
                        Console.WriteLine($"Page {pageNum}, Annotation {identifier}: (non‑markup, no Modified property)");
                    }
                }
            }

            // Save the updated PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}
