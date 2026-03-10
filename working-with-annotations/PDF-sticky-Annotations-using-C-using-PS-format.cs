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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create a sticky note (TextAnnotation) on the page
            TextAnnotation sticky = new TextAnnotation(page, rect)
            {
                Title    = "Note",                                 // Title shown in the popup window
                Contents = "This is a sticky annotation.",         // Text displayed in the note
                Icon     = TextIcon.Note,                          // Standard note icon
                Open     = true,                                   // Open the note by default
                Color    = Aspose.Pdf.Color.Yellow                // Background color of the note
            };

            // Add the annotation to the page
            page.Annotations.Add(sticky);

            // NOTE: Aspose.Pdf does NOT support saving/exporting to PostScript (PS) format.
            // The PsSaveOptions class does not exist. Save the document as PDF instead.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with sticky annotation saved to '{outputPath}'.");
    }
}