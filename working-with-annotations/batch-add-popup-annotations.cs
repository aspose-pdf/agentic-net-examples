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

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through each page in the document
            foreach (Page page in doc.Pages)
            {
                // Loop through annotations on the page (1‑based indexing)
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];

                    // Process only TextAnnotation instances
                    if (ann is TextAnnotation textAnn)
                    {
                        // Create a PopupAnnotation positioned over the same rectangle as the text annotation
                        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                            textAnn.Rect.LLX,
                            textAnn.Rect.LLY,
                            textAnn.Rect.URX,
                            textAnn.Rect.URY);

                        PopupAnnotation popup = new PopupAnnotation(page, rect);

                        // Link the popup to its parent text annotation
                        popup.Parent = textAnn;

                        // Optional: set initial visibility (closed)
                        popup.Open = false;

                        // Add the popup annotation to the page
                        page.Annotations.Add(popup);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with pop‑up annotations to '{outputPath}'.");
    }
}