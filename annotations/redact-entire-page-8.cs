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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 8 pages
            if (doc.Pages.Count < 8)
            {
                Console.Error.WriteLine("The document does not contain page 8.");
                return;
            }

            // Page indexing in Aspose.Pdf is 1‑based
            Page page = doc.Pages[8];

            // Use the page's rectangle (full page size) for the redaction area
            Aspose.Pdf.Rectangle rect = page.Rect;

            // Create the redaction annotation covering the whole page
            RedactionAnnotation redaction = new RedactionAnnotation(page, rect)
            {
                // Do not repeat the redaction on subsequent pages
                Repeat = false
            };

            // Apply the redaction (removes the content within the rectangle)
            redaction.Redact();

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(redaction);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Redaction applied and saved to '{outputPath}'.");
    }
}