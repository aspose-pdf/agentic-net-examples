using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document contains fewer than 3 pages.");
                return;
            }

            // Retrieve page three
            Page page = doc.Pages[3];

            // Define the annotation rectangle (left, bottom, right, top)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextAnnotation on the specified page and rectangle
            TextAnnotation textAnn = new TextAnnotation(page, rect)
            {
                Title = "Note",                                   // Title shown in the annotation popup
                Contents = "This is a text annotation on page three.", // The note text
                Open = true,                                      // Show the popup open by default
                Icon = TextIcon.Note,                             // Use the standard note icon
                Color = Aspose.Pdf.Color.Yellow                  // Border color of the annotation
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(textAnn);

            // Save the modified PDF (PDF format is the default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text annotation added and saved to '{outputPath}'.");
    }
}