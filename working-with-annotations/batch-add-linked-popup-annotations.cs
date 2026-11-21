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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a TextAnnotation on the page
                // Rectangle coordinates: llx, lly, urx, ury (lower‑left, upper‑right)
                Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);
                TextAnnotation textAnn = new TextAnnotation(page, textRect)
                {
                    Title    = $"Note {i}",
                    Contents = $"This is a text annotation on page {i}.",
                    Open     = false,
                    Icon     = TextIcon.Note
                };
                page.Annotations.Add(textAnn);

                // Create a PopupAnnotation linked to the TextAnnotation
                // Position the popup near the text annotation
                Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(320, 700, 520, 850);
                PopupAnnotation popupAnn = new PopupAnnotation(page, popupRect)
                {
                    // The Parent property creates the link between popup and text annotation
                    Parent   = textAnn,
                    // Optional: set the popup's contents (appears when opened)
                    Contents = $"Details for note {i}."
                };
                page.Annotations.Add(popupAnn);
            }

            // Save the modified PDF (no PreSave needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with linked popup annotations saved to '{outputPath}'.");
    }
}