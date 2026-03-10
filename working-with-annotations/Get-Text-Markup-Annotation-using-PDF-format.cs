using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based indexed
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Annotations collection is also 1‑based indexed
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];

                    // TextMarkupAnnotation is the base class for Highlight, Underline, StrikeOut, etc.
                    if (annotation is TextMarkupAnnotation markupAnnotation)
                    {
                        // Get the text that is covered by the markup annotation
                        string markedText = markupAnnotation.GetMarkedText();

                        Console.WriteLine($"Page {pageIndex}, Annotation {annIndex} ({markupAnnotation.AnnotationType}): \"{markedText}\"");
                    }
                }
            }
        }
    }
}