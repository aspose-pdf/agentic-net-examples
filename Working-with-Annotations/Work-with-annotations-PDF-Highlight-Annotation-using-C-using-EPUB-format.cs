using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputEpub = "output.epub";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle area to be highlighted (coordinates in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a highlight annotation on the specified page and rectangle
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect);
            highlight.Color = Aspose.Pdf.Color.Yellow;   // Set highlight color
            highlight.Contents = "Highlighted text";     // Optional tooltip text

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(highlight);

            // Save the modified document as EPUB using explicit save options
            EpubSaveOptions epubOptions = new EpubSaveOptions();
            doc.Save(outputEpub, epubOptions);
        }

        Console.WriteLine($"EPUB file saved to '{outputEpub}'.");
    }
}