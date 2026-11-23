using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "PopupLinked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            HighlightAnnotation highlight = null;
            Page highlightPage = null;

            // Locate the first HighlightAnnotation in the document
            foreach (Page page in doc.Pages)
            {
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is HighlightAnnotation ha)
                    {
                        highlight = ha;
                        highlightPage = page;
                        break;
                    }
                }
                if (highlight != null) break;
            }

            if (highlight == null || highlightPage == null)
            {
                Console.Error.WriteLine("No HighlightAnnotation found in the document.");
                doc.Save(outputPath); // Save unchanged
                return;
            }

            // Define a rectangle for the popup annotation (left, bottom, right, top)
            var popupRect = new Rectangle(100, 500, 300, 550);

            // Create the popup annotation and link it to the highlight
            var popup = new PopupAnnotation(highlightPage, popupRect)
            {
                Contents = "Review later",
                Parent = highlight
            };

            // Add the popup to the same page as the highlight annotation
            highlightPage.Annotations.Add(popup);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with linked popup annotation to '{outputPath}'.");
    }
}
