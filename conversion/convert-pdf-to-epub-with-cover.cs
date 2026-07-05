using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF, cover image and output EPUB paths
        const string pdfPath   = "input.pdf";
        const string coverPath = "cover.jpg";
        const string epubPath  = "output.epub";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(coverPath))
        {
            Console.Error.WriteLine($"Cover image not found: {coverPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(pdfPath))
            {
                // ---- Set PDF metadata (will be transferred to EPUB) ----
                doc.Info.Title   = "My EPUB Title";
                doc.Info.Author  = "John Doe";
                doc.Info.Subject = "Sample EPUB conversion with cover image";

                // ---- Add cover image as the first page content ----
                // Ensure there is at least one page
                if (doc.Pages.Count == 0)
                    doc.Pages.Add();

                // Create an Image object and point it to the cover file
                Aspose.Pdf.Image coverImg = new Aspose.Pdf.Image();
                coverImg.File = coverPath;

                // Insert the image at the top of the first page
                // (you may adjust positioning by adding a Rectangle if needed)
                doc.Pages[1].Paragraphs.Insert(0, coverImg);

                // ---- Prepare EPUB save options ----
                EpubSaveOptions epubOptions = new EpubSaveOptions
                {
                    // Set the EPUB title (optional, also set via doc.Info.Title)
                    Title = "My EPUB Title",

                    // Choose a content recognition mode; Flow gives the best results for most PDFs
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };

                // Save the document as EPUB
                doc.Save(epubPath, epubOptions);
            }

            Console.WriteLine($"PDF successfully converted to EPUB: {epubPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}