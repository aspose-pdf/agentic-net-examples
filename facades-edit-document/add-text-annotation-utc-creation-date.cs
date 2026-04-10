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

        // Load the PDF document. If the file does not exist, create a new blank document.
        Document doc;
        if (File.Exists(inputPath))
        {
            doc = new Document(inputPath);
        }
        else
        {
            doc = new Document();
            // Ensure there is at least one page to attach the annotation to.
            doc.Pages.Add();
        }

        // Wrap the document in a using block for proper disposal.
        using (doc)
        {
            // Aspose.Pdf uses 1‑based indexing for pages.
            Page page = doc.Pages[1];

            // Define the annotation rectangle (fully qualified to avoid ambiguity).
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextAnnotation (a subclass of MarkupAnnotation).
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title = "Note",
                Contents = "Annotation created with UTC timestamp",
                Color = Aspose.Pdf.Color.Yellow,
                // Set the creation date to the current UTC time.
                CreationDate = DateTime.UtcNow
            };

            // Add the annotation to the page.
            page.Annotations.Add(annotation);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation with UTC creation date added and saved to '{outputPath}'.");
    }
}
