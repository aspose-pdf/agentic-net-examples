using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least six pages.
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("Document must contain at least 6 pages.");
                return;
            }

            // Page 3 (Aspose.Pdf uses 1‑based indexing).
            Page sourcePage = doc.Pages[3];

            // Index of the annotation to copy (1‑based). Adjust as needed.
            int annotationIndex = 1;

            if (sourcePage.Annotations.Count < annotationIndex)
            {
                Console.Error.WriteLine("Annotation index out of range on page 3.");
                return;
            }

            // Retrieve the annotation.
            Annotation srcAnno = sourcePage.Annotations[annotationIndex];

            // Page 6 – where the annotation will be duplicated.
            Page targetPage = doc.Pages[6];

            // Handle TextAnnotation explicitly; other types fall back to adding the same instance.
            if (srcAnno is TextAnnotation textAnno)
            {
                // Create a new TextAnnotation with the same rectangle.
                TextAnnotation copy = new TextAnnotation(
                    targetPage,
                    new Aspose.Pdf.Rectangle(
                        textAnno.Rect.LLX,
                        textAnno.Rect.LLY,
                        textAnno.Rect.URX,
                        textAnno.Rect.URY));

                // Copy common properties.
                copy.Title    = textAnno.Title;
                copy.Contents = textAnno.Contents;
                copy.Open     = textAnno.Open;
                copy.Icon     = textAnno.Icon;
                copy.Color    = textAnno.Color;

                // Add the duplicated annotation to page 6.
                targetPage.Annotations.Add(copy);
            }
            else
            {
                // For other annotation types, add the original instance to the target page.
                // This is a simple fallback; for full cloning a specific type handler would be needed.
                targetPage.Annotations.Add(srcAnno);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Duplicated annotation saved to '{outputPath}'.");
    }
}