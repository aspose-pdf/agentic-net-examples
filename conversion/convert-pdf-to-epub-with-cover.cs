using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // Source PDF
        const string coverImagePath = "cover.jpg";      // Cover image file
        const string outputEpubPath = "output.epub";    // Destination EPUB

        // Verify input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(coverImagePath))
        {
            Console.Error.WriteLine($"Error: Cover image not found – {coverImagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // Set PDF metadata – this information is carried over to the EPUB
            // -----------------------------------------------------------------
            pdfDoc.Info.Title  = "My EPUB Title";
            pdfDoc.Info.Author = "Author Name";

            // ---------------------------------------------------------------
            // Insert a new page at the beginning to act as the cover page
            // ---------------------------------------------------------------
            Page coverPage = pdfDoc.Pages.Insert(1);

            // Add the cover image so that it fills the entire page
            using (FileStream imgStream = File.OpenRead(coverImagePath))
            {
                double pageWidth  = coverPage.PageInfo.Width;
                double pageHeight = coverPage.PageInfo.Height;

                // Rectangle uses coordinates: llx, lly, urx, ury
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, pageWidth, pageHeight);
                coverPage.AddImage(imgStream, rect);
            }

            // ---------------------------------------------------------------
            // Configure EPUB save options (title and content recognition mode)
            // ---------------------------------------------------------------
            EpubSaveOptions epubOptions = new EpubSaveOptions
            {
                Title = "My EPUB Title",
                ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
            };

            // Save the document as EPUB using the explicit save options
            pdfDoc.Save(outputEpubPath, epubOptions);
        }

        Console.WriteLine($"EPUB file created successfully at '{outputEpubPath}'.");
    }
}