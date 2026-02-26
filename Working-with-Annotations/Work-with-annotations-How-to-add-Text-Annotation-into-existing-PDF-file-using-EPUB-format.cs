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
        const string annotationTitle = "Note";
        const string annotationText = "This is a note added to the PDF.";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a TextAnnotation (sticky note) on the specified page
            TextAnnotation textAnnot = new TextAnnotation(page, rect)
            {
                Title = annotationTitle,
                Contents = annotationText,
                Open = true,                     // Show the annotation popup when opened
                Icon = TextIcon.Note,            // Use the standard note icon
                Color = Aspose.Pdf.Color.Yellow  // Optional background color
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(textAnnot);

            // Prepare EPUB save options (must be passed explicitly)
            EpubSaveOptions epubOptions = new EpubSaveOptions
            {
                Title = "PDF with Text Annotation"
                // Additional options can be set here if needed
            };

            // Save the modified document as EPUB using the explicit options
            doc.Save(outputEpub, epubOptions);
        }

        Console.WriteLine($"PDF with text annotation saved as EPUB: {outputEpub}");
    }
}