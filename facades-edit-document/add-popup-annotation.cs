using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "popup_annotation.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the popup will appear (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create the popup annotation on the page
            PopupAnnotation popup = new PopupAnnotation(page, rect)
            {
                // Multi‑line text with bullet points (Unicode bullet character U+2022)
                Contents = "\u2022 First comment\r\n\u2022 Second comment\r\n\u2022 Third comment",
                Open = false // The popup will be closed initially
            };

            // Add the annotation to the page
            page.Annotations.Add(popup);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with popup annotation saved to '{outputPath}'.");
    }
}