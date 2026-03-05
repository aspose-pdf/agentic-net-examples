using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for input and output PDF files
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block to ensure proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF has no pages.");
                return;
            }

            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextAnnotation (a sticky‑note style annotation)
            TextAnnotation textAnn = new TextAnnotation(page, rect)
            {
                Title = "Note",
                Contents = "This is a sample text annotation added programmatically.",
                Open = true,
                // Use Aspose.Pdf.Color for cross‑platform compatibility
                Color = Aspose.Pdf.Color.Yellow
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(textAnn);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF manipulation completed. Saved to '{outputPath}'.");
    }
}