using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Insert a blank page at position 3 (pages are 1‑based)
            Page insertedPage = doc.Pages.Insert(3);

            // Ensure the new page has the same size as the first page
            insertedPage.MediaBox = doc.Pages[1].MediaBox;

            // Define a rectangle for the header annotation (top of the page)
            // Rectangle constructor: (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle headerRect = new Aspose.Pdf.Rectangle(
                0,                                 // llx
                insertedPage.Rect.Height - 50,     // lly (50 units from top)
                insertedPage.Rect.Width,           // urx
                insertedPage.Rect.Height);         // ury (top edge)

            // Create a text annotation to serve as the header
            TextAnnotation headerAnnotation = new TextAnnotation(insertedPage, headerRect)
            {
                Title    = "Header",
                Contents = "This is a header annotation",
                Color    = Aspose.Pdf.Color.LightGray,
                Open     = true,
                Icon     = TextIcon.Note
            };

            // Add the annotation to the page
            insertedPage.Annotations.Add(headerAnnotation);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}