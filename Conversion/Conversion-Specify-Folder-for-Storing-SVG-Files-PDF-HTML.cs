using System;
using System.IO;
using Aspose.Pdf; // HtmlSaveOptions is a type inside this namespace, not a separate namespace

class PdfToHtmlWithSvgFolder
{
    static void Main(string[] args)
    {
        // Input PDF file path (adjust as needed)
        const string pdfPath = "input.pdf";

        // Desired output folder for HTML and generated SVG files
        const string outputFolder = "HtmlOutput";

        // Ensure the input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        // Create the output folder if it does not exist
        Directory.CreateDirectory(outputFolder);

        // Full path of the main HTML file; SVG files will be placed in the same folder
        string htmlPath = Path.Combine(outputFolder, "output.html");

        try
        {
            // Load the source PDF document
            Document pdfDocument = new Document(pdfPath);

            // Configure HTML conversion options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Each raster image will be saved as an external PNG file
                // and referenced through a wrapping SVG image.
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,

                // Optional: keep layout fixed (true) or flow (false)
                FixedLayout = true
            };

            // Perform the conversion
            pdfDocument.Save(htmlPath, htmlOptions);

            Console.WriteLine($"Conversion completed. HTML saved to: {htmlPath}");
            Console.WriteLine($"Generated SVG wrappers and PNG images are located in: {outputFolder}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}