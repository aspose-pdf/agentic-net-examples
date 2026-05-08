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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Rectangle for the TextAnnotation (sticky note)
                Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

                // Create a TextAnnotation on the current page
                TextAnnotation textAnn = new TextAnnotation(page, textRect)
                {
                    Title = $"Note {i}",
                    Contents = $"Popup linked to page {i}.",
                    Icon = TextIcon.Note,
                    Open = false // start closed
                };

                // Rectangle for the PopupAnnotation (larger area for the popup window)
                Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(130, 720, 300, 850);

                // Create a PopupAnnotation and associate it with the TextAnnotation
                PopupAnnotation popupAnn = new PopupAnnotation(page, popupRect)
                {
                    Open = false,
                    Color = Aspose.Pdf.Color.LightGray
                };
                popupAnn.Parent = textAnn;   // set parent relationship
                textAnn.Popup = popupAnn;    // link popup to the text annotation

                // Add both annotations to the page
                page.Annotations.Add(textAnn);
                page.Annotations.Add(popupAnn);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with popup annotations: {outputPath}");
    }
}