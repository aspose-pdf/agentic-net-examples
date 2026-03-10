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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a sticky note (TextAnnotation) on the specified page and rectangle
            TextAnnotation sticky = new TextAnnotation(page, rect)
            {
                Title    = "Note",                                   // Title shown in the popup window title bar
                Contents = "This is a sticky note annotation.",      // Text displayed in the popup
                Icon     = TextIcon.Note,                            // Icon type (Note, Comment, etc.)
                Color    = Aspose.Pdf.Color.Yellow,                  // Border color of the annotation
                Open     = true                                      // Show the note open when the PDF is opened
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(sticky);

            // Save the modified PDF (PDF format does not require explicit SaveOptions)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sticky annotation added and saved to '{outputPath}'.");
    }
}