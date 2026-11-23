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
            // Verify the document has at least 7 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 7)
            {
                Console.Error.WriteLine("Document contains fewer than 7 pages.");
                return;
            }

            // Retrieve page 7
            Page page = doc.Pages[7];

            // Define the annotation rectangle (lower‑left x/y, upper‑right x/y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text annotation (custom note) and set its properties
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title = "Custom Note",
                Contents = "This is an extra custom annotation on page 7.",
                Open = true,
                Icon = TextIcon.Note
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom annotation to '{outputPath}'.");
    }
}