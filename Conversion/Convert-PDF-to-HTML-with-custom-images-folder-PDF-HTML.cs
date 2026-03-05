using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfToHtmlConverter
{
    static void Main()
    {
        // Input PDF file
        const string pdfPath = "input.pdf";

        // Output HTML file
        const string htmlPath = "output.html";

        // Folder where all extracted images will be saved
        const string imagesFolder = "Images";

        // Validate input file
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        // Ensure the images folder exists
        Directory.CreateDirectory(imagesFolder);

        try
        {
            // Load the PDF document (lifecycle: create & load)
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Configure HTML save options
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Save each page as a separate HTML file if needed:
                    // SplitIntoPages = false,

                    // Store all extracted images in the custom folder
                    SpecialFolderForAllImages = imagesFolder,

                    // Example: embed raster images as external PNG files referenced via SVG
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg
                };

                // Save the PDF as HTML using the configured options
                pdfDocument.Save(htmlPath, htmlOptions);
                Console.WriteLine($"PDF successfully converted to HTML: {htmlPath}");
                Console.WriteLine($"All images saved to folder: {imagesFolder}");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}