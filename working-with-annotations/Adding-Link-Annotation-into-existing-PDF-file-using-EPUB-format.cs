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
        const string linkUrl = "https://www.example.com";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the existing PDF (lifecycle: load)
            using (Document doc = new Document(inputPdf))
            {
                // Get the first page (1‑based indexing)
                Page page = doc.Pages[1];

                // Define the annotation rectangle (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a link annotation (creation rule)
                LinkAnnotation link = new LinkAnnotation(page, rect)
                {
                    // Use GoToURIAction for an external URL (hyperlink‑property‑is‑not‑a‑string rule)
                    Action = new GoToURIAction(linkUrl),
                    Color = Aspose.Pdf.Color.Blue,   // visual cue
                    Contents = "Open Example"
                };

                // Add the annotation to the page (lifecycle: add)
                page.Annotations.Add(link);

                // Prepare EPUB save options (explicit save options required)
                EpubSaveOptions epubOptions = new EpubSaveOptions
                {
                    Title = "PDF with Link Annotation"
                };

                // Save the modified document as EPUB (lifecycle: save)
                doc.Save(outputEpub, epubOptions);
            }

            Console.WriteLine($"EPUB saved to '{outputEpub}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}