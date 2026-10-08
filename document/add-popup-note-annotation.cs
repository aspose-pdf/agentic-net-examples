using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_popup.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotation will appear (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle (llx, lly, urx, ury) for the annotation appearance
            // Use fully qualified type to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a TextAnnotation (pop‑up note) and set its properties
            TextAnnotation popup = new TextAnnotation(page, rect)
            {
                Title    = "Note Title",                     // Title shown in the pop‑up window
                Contents = "This is the additional information displayed when the user hovers over the note.", // Text shown in the pop‑up
                Open     = false,                           // Do not open automatically; appears on hover/click
                Icon     = TextIcon.Note                     // Standard note icon
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(popup);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pop‑up note annotation added and saved to '{outputPath}'.");
    }
}