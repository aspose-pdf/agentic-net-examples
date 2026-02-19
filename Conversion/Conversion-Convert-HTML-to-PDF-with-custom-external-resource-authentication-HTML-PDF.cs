using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source HTML and the resulting PDF
        string htmlPath = "input.html";
        string pdfPath = "output.pdf";

        // Verify that the HTML file exists before proceeding
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Determine the base directory of the HTML file; this helps resolve
            // relative URLs for external resources.
            string baseDir = Path.GetDirectoryName(Path.GetFullPath(htmlPath));

            // Create HtmlLoadOptions with the base path.
            var loadOptions = new HtmlLoadOptions(baseDir);

            // Load the HTML content into an Aspose.Pdf Document using the configured options.
            Document pdfDocument = new Document(htmlPath, loadOptions);

            // Save the generated PDF to the specified output file.
            pdfDocument.Save(pdfPath);
            Console.WriteLine($"PDF successfully created at: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
