using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_popups.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define a small rectangle for the sticky‑note (TextAnnotation)
                Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);
                TextAnnotation textAnn = new TextAnnotation(page, textRect)
                {
                    Title    = $"Note {i}",
                    Contents = $"This is a text annotation on page {i}.",
                    Icon     = TextIcon.Note,          // visual icon
                    Color    = Aspose.Pdf.Color.Yellow // background color of the note icon
                };

                // Define a larger rectangle for the popup window
                Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(130, 500, 300, 600);
                PopupAnnotation popupAnn = new PopupAnnotation(page, popupRect)
                {
                    Contents = $"Detailed information for annotation on page {i}.",
                    Open     = false                     // initially closed
                };

                // Link the popup to the text annotation
                textAnn.Popup = popupAnn;
                popupAnn.Parent = textAnn; // optional, makes the parent relationship explicit

                // Add both annotations to the page
                page.Annotations.Add(textAnn);
                page.Annotations.Add(popupAnn);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with popup annotations saved to '{outputPath}'.");
    }
}