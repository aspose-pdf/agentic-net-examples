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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least six pages
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("The document does not contain a page 6.");
                return;
            }

            // Get page six (Aspose.Pdf uses 1‑based indexing)
            Page pageSix = doc.Pages[6];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the TextAnnotation with the rectangle
            TextAnnotation annotation = new TextAnnotation(pageSix, rect)
            {
                Title    = "Note Title",                     // optional title
                Contents = "This is a custom text annotation with a custom icon and border.",
                Open     = true,                             // annotation window open by default
                Icon     = TextIcon.Comment                  // custom icon (e.g., Comment)
            };

            // Set the border color via the annotation's Color property
            annotation.Color = Aspose.Pdf.Color.Red;
            // Set custom border: solid style, width 2
            annotation.Border = new Border(annotation) { Width = 2, Style = BorderStyle.Solid };

            // Add the annotation to the page's annotation collection
            pageSix.Annotations.Add(annotation);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text annotation added to page 6 and saved as '{outputPath}'.");
    }
}
