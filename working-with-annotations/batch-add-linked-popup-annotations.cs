using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_popups.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate through all annotations on the current page
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation ann = page.Annotations[annIndex];

                    // Process only TextAnnotation (sticky‑note) objects
                    if (ann is TextAnnotation textAnn)
                    {
                        // Create a PopupAnnotation on the same page using the same rectangle
                        Aspose.Pdf.Rectangle rect = textAnn.Rect;
                        PopupAnnotation popup = new PopupAnnotation(page, rect);

                        // Optionally copy the contents from the text annotation
                        popup.Contents = textAnn.Contents;

                        // Link the popup to the text annotation
                        textAnn.Popup = popup;

                        // Add the popup to the page's annotation collection
                        page.Annotations.Add(popup);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with linked pop‑up annotations saved to '{outputPath}'.");
    }
}