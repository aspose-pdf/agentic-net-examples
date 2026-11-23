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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based; get page 2
            Page page = doc.Pages[2];

            // Create a TextAnnotation on page 2
            Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation textAnn = new TextAnnotation(page, textRect)
            {
                Title    = "Sample Note",
                Contents = "This is a linked text annotation."
            };

            // Create a PopupAnnotation that will be linked to the TextAnnotation
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(120, 480, 280, 460);
            PopupAnnotation popupAnn = new PopupAnnotation(page, popupRect);

            // Link the popup to the text annotation
            textAnn.Popup = popupAnn;

            // Add both annotations to the page's annotation collection
            page.Annotations.Add(textAnn);
            page.Annotations.Add(popupAnn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with linked annotations to '{outputPath}'.");
    }
}