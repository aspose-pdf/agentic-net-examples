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

        // Load the PDF with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left x/y, upper‑right x/y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create a sticky note (TextAnnotation) and set its properties
            TextAnnotation sticky = new TextAnnotation(page, rect)
            {
                Title = "Note",
                Contents = "This is a sticky annotation.",
                Open = true,
                Icon = TextIcon.Comment // alternative: TextIcon.Note
            };

            // Add the annotation to the page
            page.Annotations.Add(sticky);

            // Retrieve and display the annotation contents
            string retrieved = sticky.Contents;
            Console.WriteLine($"Added annotation contents: {retrieved}");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}