using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define the rectangle for the annotation (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a text annotation
                TextAnnotation txtAnn = new TextAnnotation(page, rect)
                {
                    Title    = $"Note {i}",
                    Contents = $"This is a popup linked to page {i}.",
                    Open     = false,
                    Icon     = TextIcon.Note
                };

                // Create a popup annotation and link it to the text annotation
                PopupAnnotation popup = new PopupAnnotation(page, rect);
                txtAnn.Popup = popup;               // associate popup with the text annotation

                // Add the text annotation (the popup is added automatically)
                page.Annotations.Add(txtAnn);
            }

            // Save the modified PDF while the Document is still alive
            doc.Save(outputPath);
        }

        Console.WriteLine($"Popup‑linked text annotations saved to '{outputPath}'.");
    }
}