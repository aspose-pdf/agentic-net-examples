using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // for metadata handling if needed (optional)

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Cover image file path (will be added as the first page image)
        const string coverImagePath = "cover.jpg";

        // Output EPUB file path
        const string epubPath = "output.epub";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(coverImagePath))
        {
            Console.Error.WriteLine($"Cover image not found: {coverImagePath}");
            return;
        }

        try
        {
            // Load the source PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // -----------------------------------------------------------------
                // Set document metadata (Title, Author, Subject, Keywords)
                // -----------------------------------------------------------------
                pdfDoc.Info.Title = "My Custom EPUB Title";
                pdfDoc.Info.Author = "John Doe";
                pdfDoc.Info.Subject = "Sample EPUB conversion with cover image";
                pdfDoc.Info.Keywords = "EPUB, Aspose.Pdf, conversion, cover image";

                // -----------------------------------------------------------------
                // Insert the cover image as the first page content.
                // This image will appear as the first visual element in the EPUB.
                // -----------------------------------------------------------------
                // Ensure there is at least one page
                if (pdfDoc.Pages.Count == 0)
                {
                    pdfDoc.Pages.Add();
                }

                // Create an Image object and set its source file
                Image coverImg = new Image
                {
                    File = coverImagePath
                };

                // Optionally, set the image dimensions (width, height) by scaling
                // Here we let the image keep its original size; you can adjust as needed.
                // Add the image to the first page's paragraph collection
                pdfDoc.Pages[1].Paragraphs.Add(coverImg);

                // -----------------------------------------------------------------
                // Prepare EPUB save options
                // -----------------------------------------------------------------
                EpubSaveOptions epubOptions = new EpubSaveOptions
                {
                    // Set the EPUB title (metadata)
                    Title = "My Custom EPUB Title",

                    // Choose a content recognition mode.
                    // Flow provides the most thorough layout analysis.
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };

                // Save the PDF as EPUB using the specified options
                pdfDoc.Save(epubPath, epubOptions);
            }

            Console.WriteLine($"PDF successfully converted to EPUB with cover image: '{epubPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}