using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

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
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Set PDF metadata (these will be transferred to the EPUB)
                pdfDoc.Info.Title  = "Custom EPUB Title";
                pdfDoc.Info.Author = "Author Name";

                // Prepare EPUB save options
                EpubSaveOptions epubOptions = new EpubSaveOptions();

                // Set EPUB metadata
                epubOptions.Title = "Custom EPUB Title";

                // OPTIONAL: set a cover image if the property exists.
                // The Image class has a parameterless constructor; set the file path.
                Image coverImg = new Image();
                coverImg.File = coverPath;

                // If EpubSaveOptions exposes a CoverImage property, assign it.
                // Uncomment the following line if the property is available in your version:
                // epubOptions.CoverImage = coverImg;

                // Save the PDF as EPUB using the configured options
                pdfDoc.Save(epubPath, epubOptions);
            }

            Console.WriteLine($"PDF successfully converted to EPUB: '{epubPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}