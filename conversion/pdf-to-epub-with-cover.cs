using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string coverImage = "cover.jpg";
        const string outputEpub = "output.epub";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(coverImage))
        {
            Console.Error.WriteLine($"Cover image not found: {coverImage}");
            return;
        }

        using (Document doc = new Document(inputPdf))
        {
            // Insert a new page at the beginning to act as the cover.
            doc.Pages.Insert(1);
            // Add the cover image to the newly inserted page.
            Image img = new Image { File = coverImage };
            doc.Pages[1].Paragraphs.Add(img);

            // Set PDF metadata – this information is also written to the EPUB.
            doc.Info.Title = "Sample EPUB Title";
            doc.Info.Author = "John Doe";

            // Configure EPUB save options.
            EpubSaveOptions epubOptions = new EpubSaveOptions
            {
                Title = "Sample EPUB Title"
                // ContentRecognitionMode can be set if a specific mode is required, e.g.:
                // ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
            };

            // Save the document as EPUB.
            doc.Save(outputEpub, epubOptions);
        }

        Console.WriteLine($"EPUB created: {outputEpub}");
    }
}