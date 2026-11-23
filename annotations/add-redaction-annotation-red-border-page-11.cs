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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least 11 pages (pages are 1‑based)
            if (doc.Pages.Count < 11)
            {
                Console.Error.WriteLine("The document contains fewer than 11 pages.");
                return;
            }

            // Retrieve page 11
            Page page = doc.Pages[11];

            // Define the rectangle area for the redaction (example coordinates)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the redaction annotation on the specified page and rectangle
            RedactionAnnotation redaction = new RedactionAnnotation(page, rect);

            // Set only the border color to red (use Aspose.Pdf.Color, not System.Drawing)
            redaction.BorderColor = Aspose.Pdf.Color.Red;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(redaction);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Redaction annotation added and saved to '{outputPath}'.");
    }
}