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
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // 1‑based page indexing – get the first page
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury) in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 400, 720);

            // Create a highlight annotation (concrete TextMarkupAnnotation)
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                Color    = Aspose.Pdf.Color.Yellow,   // Use Aspose.Pdf.Color (cross‑platform)
                Contents = "Important note",          // Popup text
                Title    = "Reviewer"                 // Title shown in the popup
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(highlight);

            // Prepare EPUB save options – must be passed explicitly for non‑PDF output
            EpubSaveOptions epubOptions = new EpubSaveOptions
            {
                ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
            };

            // Save the document as EPUB
            doc.Save(outputEpub, epubOptions);
        }

        Console.WriteLine($"EPUB saved to '{outputEpub}'.");
    }
}