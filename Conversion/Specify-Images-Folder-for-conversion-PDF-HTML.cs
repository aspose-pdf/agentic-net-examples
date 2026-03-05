using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string pdfPath = "input.pdf";

        // Output HTML file
        const string htmlPath = "output.html";

        // Folder where all extracted images will be saved
        const string imagesFolder = "output_images";

        // Ensure the images folder exists
        Directory.CreateDirectory(imagesFolder);

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions saveOptions = new HtmlSaveOptions
                {
                    // Save all raster and vector images into the specified folder
                    SpecialFolderForAllImages = imagesFolder,

                    // Optional: separate SVG images into another folder (if needed)
                    // SpecialFolderForSvgImages = Path.Combine(imagesFolder, "svg")
                };

                // Save the PDF as HTML using the options (rule: explicit SaveOptions for non‑PDF output)
                pdfDocument.Save(htmlPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to HTML: {htmlPath}");
            Console.WriteLine($"All extracted images are stored in: {imagesFolder}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}