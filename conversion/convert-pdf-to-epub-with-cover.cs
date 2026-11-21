using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string coverImagePath = "cover.jpg";
        const string outputEpubPath = "output.epub";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(coverImagePath))
        {
            Console.Error.WriteLine($"Cover image not found: {coverImagePath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Set PDF metadata – this information is transferred to the EPUB
                pdfDoc.Info.Title   = "My EPUB Title";
                pdfDoc.Info.Author  = "John Doe";
                pdfDoc.Info.Subject = "Sample EPUB conversion with cover image";
                pdfDoc.Info.Keywords = "Aspose.Pdf, EPUB, Cover";

                // Insert a new page at the beginning to act as the cover
                pdfDoc.Pages.Insert(1);
                Page coverPage = pdfDoc.Pages[1];

                // Create the cover image using a stream (parameter‑less constructor)
                Image coverImg = new Image();
                using (FileStream imgStream = new FileStream(coverImagePath, FileMode.Open, FileAccess.Read))
                {
                    coverImg.ImageStream = imgStream;
                    // Optionally stretch the image to fill the page
                    coverImg.FixWidth  = coverPage.PageInfo.Width;
                    coverImg.FixHeight = coverPage.PageInfo.Height;
                    coverPage.Paragraphs.Add(coverImg);
                }

                // Configure EPUB save options (no CoverImage property needed)
                EpubSaveOptions epubOpts = new EpubSaveOptions
                {
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };

                // Save as EPUB using the explicit options
                pdfDoc.Save(outputEpubPath, epubOpts);
            }

            Console.WriteLine($"PDF successfully converted to EPUB: {outputEpubPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
