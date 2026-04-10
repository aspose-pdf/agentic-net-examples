using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input PDF, output EPUB and cover image paths
        const string inputPdfPath  = "input.pdf";
        const string outputEpubPath = "output.epub";
        const string coverImagePath = "cover.jpg";

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(coverImagePath))
        {
            Console.Error.WriteLine($"Cover image not found: {coverImagePath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // ----- Set PDF metadata (will be carried over to EPUB) -----
            pdfDocument.Info.Title   = "Custom EPUB Title";
            pdfDocument.Info.Author  = "Author Name";
            pdfDocument.Info.Subject = "Subject of the EPUB";

            // ----- Insert a new page at the beginning to act as cover -----
            // Insert page at position 1 (Aspose.Pdf uses 1‑based indexing)
            Page coverPage = pdfDocument.Pages.Insert(1);

            // Create an Image object and point it to the cover file
            Image coverImg = new Image();
            coverImg.File = coverImagePath;

            // Optionally, scale the image to fit the page dimensions
            // (Here we simply add it; Aspose.Pdf will render it respecting page size)
            coverPage.Paragraphs.Add(coverImg);

            // ----- Prepare EPUB save options -----
            EpubSaveOptions epubOptions = new EpubSaveOptions();

            // Set the EPUB title (separate from PDF metadata)
            epubOptions.Title = "Custom EPUB Title";

            // Choose content recognition mode – Flow gives best reflow for e‑readers
            epubOptions.ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow;

            // Save the document as EPUB using the options
            pdfDocument.Save(outputEpubPath, epubOptions);
        }

        Console.WriteLine($"PDF successfully converted to EPUB with cover image: '{outputEpubPath}'");
    }
}