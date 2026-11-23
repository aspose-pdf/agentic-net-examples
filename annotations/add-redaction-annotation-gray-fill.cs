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
            // Ensure the document has at least 5 pages
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("Document does not contain page 5.");
                return;
            }

            // Page indexing in Aspose.Pdf is 1‑based
            Page page = doc.Pages[5];

            // Define the rectangle area for the redaction (llx, lly, urx, ury)
            // Adjust coordinates as needed; here we use an example region
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the redaction annotation
            RedactionAnnotation redaction = new RedactionAnnotation(page, rect)
            {
                // Gray fill color (e.g., 0.5 gray)
                FillColor = Aspose.Pdf.Color.Gray,
                // Black border color
                BorderColor = Aspose.Pdf.Color.Black
            };

            // Add the annotation to the page
            page.Annotations.Add(redaction);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Redaction annotation added and saved to '{outputPath}'.");
    }
}