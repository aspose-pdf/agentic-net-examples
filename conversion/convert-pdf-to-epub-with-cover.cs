using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputEpub = "output.epub";
        const string coverImagePath = "cover.jpg";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(coverImagePath))
        {
            Console.Error.WriteLine($"Cover image not found: {coverImagePath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Optional: set PDF metadata (these values are also used by the EPUB output)
                doc.Info.Title = "My EPUB Title";
                doc.Info.Author = "John Doe";
                doc.Info.Subject = "Converted from PDF";
                doc.Info.Keywords = "PDF,EPUB,Conversion";

                // Insert a cover page as the first page of the document
                doc.Pages.Insert(1);
                Image cover = new Image { File = coverImagePath };
                doc.Pages[1].Paragraphs.Add(cover);

                // Configure EPUB save options (Author and CoverImage are not properties of EpubSaveOptions)
                EpubSaveOptions epubOpts = new EpubSaveOptions
                {
                    Title = "My EPUB Title",
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };

                // Save the document as EPUB using the explicit options
                doc.Save(outputEpub, epubOpts);
            }

            Console.WriteLine($"PDF successfully converted to EPUB: {outputEpub}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
