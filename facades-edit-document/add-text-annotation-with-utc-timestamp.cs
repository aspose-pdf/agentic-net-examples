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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            Page page = doc.Pages[1];

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a TextAnnotation (sticky note) on the specified page
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Contents = "Sample annotation",
                // Set the creation and modification dates to the current UTC time
                CreationDate = DateTime.UtcNow,
                Modified     = DateTime.UtcNow,
                // Optional visual settings
                Icon   = TextIcon.Note,
                Open   = true,
                Color  = Aspose.Pdf.Color.Yellow
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation added with UTC creation date. Saved to '{outputPath}'.");
    }
}