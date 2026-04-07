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

        // Load the PDF document (using ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document has fewer than 3 pages.");
                return;
            }

            // Get page three (1‑based indexing)
            Page page = doc.Pages[3];

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the text annotation on page three
            TextAnnotation textAnn = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "This is a text annotation on page 3.",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true,               // Show the popup initially
                Icon     = TextIcon.Note        // Use the standard note icon
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(textAnn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text annotation added and saved to '{outputPath}'.");
    }
}