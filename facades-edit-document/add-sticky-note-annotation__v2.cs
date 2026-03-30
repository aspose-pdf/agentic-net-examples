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
        const string comment = "This is a user comment added as a sticky note.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page firstPage = doc.Pages[1];

            // Define the rectangle where the sticky note will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextAnnotation (sticky note) on the first page
            TextAnnotation stickyNote = new TextAnnotation(firstPage, rect)
            {
                Title = "User Comment",
                Contents = comment,
                Icon = TextIcon.Comment,
                Open = false
            };

            // Add the annotation to the page's annotation collection
            firstPage.Annotations.Add(stickyNote);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sticky note added and saved to '{outputPath}'.");
    }
}