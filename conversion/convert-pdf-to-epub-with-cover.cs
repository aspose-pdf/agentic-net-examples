using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the PDF inside a using block (ensures proper disposal)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // ----- Set PDF metadata (will be transferred to EPUB) -----
            pdfDoc.Info.Title   = "My EPUB Title";
            pdfDoc.Info.Author  = "Author Name";
            pdfDoc.Info.Subject = "Subject of the EPUB";

            // ----- Insert a cover page with the custom image -----
            // Create a new page at the beginning of the document
            Page coverPage = pdfDoc.Pages.Insert(1);
            // Add the image to the page
            Aspose.Pdf.Image coverImg = new Aspose.Pdf.Image();
            coverImg.File = coverPath;
            // Optionally scale the image to fit the page
            coverImg.FixWidth = coverPage.PageInfo.Width;
            coverImg.FixHeight = coverPage.PageInfo.Height;
            coverPage.Paragraphs.Add(coverImg);

            // ----- Prepare EPUB save options -----
            EpubSaveOptions epubOptions = new EpubSaveOptions
            {
                // Set the EPUB title (metadata)
                Title = "My EPUB Title",
                // Choose a content recognition mode (optional, default is Flow)
                ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
            };

            // Save the document as EPUB using the options
            pdfDoc.Save(epubPath, epubOptions);
        }

        Console.WriteLine($"PDF successfully converted to EPUB: {epubPath}");
    }
}