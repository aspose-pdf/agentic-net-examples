using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the encrypted PDF and the resulting HTML file
        const string pdfPath = "encrypted.pdf";
        const string userPassword = "userPassword"; // replace with the actual password
        const string htmlPath = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the encrypted PDF using the password constructor
            Document pdfDocument = new Document(pdfPath, userPassword);

            // Configure HTML conversion options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Generate only the content inside the <body> tag
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent,
                // Embed raster images as PNGs inside SVG wrappers (adjust as needed)
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            // Save the PDF as HTML using the configured options
            pdfDocument.Save(htmlPath, htmlOptions);

            Console.WriteLine($"PDF successfully converted to HTML at '{htmlPath}'.");
        }
        catch (InvalidPasswordException)
        {
            Console.Error.WriteLine("Error: The provided password is incorrect.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}
