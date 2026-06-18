using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_annotation.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Get page three
            Page page = doc.Pages[3];

            // Define the annotation rectangle (left, bottom, right, top)
            // Fully qualify the Rectangle type to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextAnnotation on the specified page and rectangle
            TextAnnotation textAnn = new TextAnnotation(page, rect)
            {
                Title    = "Note",                     // Title shown in the annotation's popup window
                Contents = "This is a text annotation added to page 3.", // The note text
                Icon     = TextIcon.Note,              // Choose an icon (e.g., Note)
                Open     = true                        // Open the popup by default
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(textAnn);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with annotation: {outputPath}");
    }
}