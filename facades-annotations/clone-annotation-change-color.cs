using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Clones an annotation from one page, changes its color, and adds it to another page.
    // sourcePageIdx, targetPageIdx, and annotationIdx are 1‑based indexes.
    public static void CloneAnnotationAndChangeColor(
        string inputPdf,
        string outputPdf,
        int sourcePageIdx,
        int annotationIdx,
        int targetPageIdx,
        Color newColor)
    {
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Validate page indexes
            if (sourcePageIdx < 1 || sourcePageIdx > doc.Pages.Count ||
                targetPageIdx < 1 || targetPageIdx > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page index.");
                return;
            }

            Page sourcePage = doc.Pages[sourcePageIdx];
            Page targetPage = doc.Pages[targetPageIdx];

            // Validate annotation index on the source page
            if (annotationIdx < 1 || annotationIdx > sourcePage.Annotations.Count)
            {
                Console.Error.WriteLine("Invalid annotation index.");
                return;
            }

            // Retrieve the original annotation
            Annotation original = sourcePage.Annotations[annotationIdx];

            // Currently we handle TextAnnotation cloning; other types can be added similarly.
            if (original is TextAnnotation textAnn)
            {
                // Create a new TextAnnotation on the target page with the same rectangle.
                TextAnnotation clone = new TextAnnotation(targetPage, textAnn.Rect)
                {
                    Title    = textAnn.Title,
                    Contents = textAnn.Contents,
                    Color    = newColor,          // Apply the new color
                    Open     = textAnn.Open,
                    Subject  = textAnn.Subject
                };

                // Add the cloned annotation to the target page.
                targetPage.Annotations.Add(clone);
            }
            else
            {
                Console.Error.WriteLine($"Cloning for annotation type '{original.GetType().Name}' is not implemented.");
                return;
            }

            // Save the modified document (lifecycle rule: save inside using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Annotation cloned and saved to '{outputPdf}'.");
    }

    // Example entry point
    public static void Main(string[] args)
    {
        // Example usage:
        // Clone the first annotation from page 1, change its color to Red, and place it on page 2.
        string inputPath  = "input.pdf";
        string outputPath = "output.pdf";

        CloneAnnotationAndChangeColor(
            inputPath,
            outputPath,
            sourcePageIdx: 1,
            annotationIdx: 1,
            targetPageIdx: 2,
            newColor: Aspose.Pdf.Color.Red);
    }
}