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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Rectangle for the TextAnnotation (sticky note)
                Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);
                TextAnnotation txtAnn = new TextAnnotation(page, textRect)
                {
                    Title = $"Note {i}",
                    Contents = $"This is a text annotation on page {i}.",
                    Icon = TextIcon.Note,
                    Open = false
                };

                // Rectangle for the PopupAnnotation (larger area)
                Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(130, 700, 300, 800);
                PopupAnnotation popup = new PopupAnnotation(page, popupRect)
                {
                    // Link the popup to its parent text annotation
                    Parent = txtAnn,
                    Open = false
                };

                // Add both annotations to the page's annotation collection
                page.Annotations.Add(txtAnn);
                page.Annotations.Add(popup);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with pop‑up annotations to '{outputPath}'.");
    }
}