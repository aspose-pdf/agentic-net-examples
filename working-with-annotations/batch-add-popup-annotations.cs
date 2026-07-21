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
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Text annotation (sticky note) rectangle
                Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);
                TextAnnotation textAnn = new TextAnnotation(page, textRect)
                {
                    Title = $"Note {i}",
                    Contents = $"This is a text annotation on page {i}",
                    Icon = TextIcon.Comment,
                    Color = Aspose.Pdf.Color.Yellow,
                    Open = false
                };

                // Popup annotation rectangle (larger window)
                Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(130, 650, 400, 800);
                PopupAnnotation popupAnn = new PopupAnnotation(page, popupRect)
                {
                    Contents = $"Popup for page {i}",
                    Open = false,
                    Color = Aspose.Pdf.Color.LightGray
                };

                // Link the popup to the text annotation
                textAnn.Popup = popupAnn;
                popupAnn.Parent = textAnn;

                // Add both annotations to the page
                page.Annotations.Add(textAnn);
                page.Annotations.Add(popupAnn);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved annotated PDF to '{outputPath}'.");
    }
}