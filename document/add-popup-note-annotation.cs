using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;          // TextAnnotation, TextIcon
using Aspose.Pdf.Text;                // for Aspose.Pdf.Rectangle (fully qualified)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "pop_up_note.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotation will be placed (first page here)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury) in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a TextAnnotation (pop‑up note)
            TextAnnotation note = new TextAnnotation(page, rect)
            {
                Title    = "Additional Info",               // Title shown in the pop‑up header
                Contents = "This note appears when the user hovers over the icon.", // Text displayed in the pop‑up
                Open     = false,                           // Do not open by default; appears on hover/click
                Icon     = TextIcon.Note                     // Standard note icon
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(note);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pop‑up note annotation added and saved to '{outputPath}'.");
    }
}