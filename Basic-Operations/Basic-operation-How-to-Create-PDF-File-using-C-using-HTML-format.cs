using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class CreatePdfFromHtml
{
    static void Main()
    {
        // Directory that contains the input HTML and where the PDF will be saved
        string dataDir = "data";
        string htmlPath = Path.Combine(dataDir, "sample.html");
        string pdfPath  = Path.Combine(dataDir, "output.pdf");

        // Verify that the HTML source file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML file into a PDF Document using HtmlLoadOptions
            HtmlLoadOptions loadOptions = new HtmlLoadOptions(); // default options
            Document pdfDocument = new Document(htmlPath, loadOptions);

            // Save the Document as a PDF file
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"PDF created successfully at: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF creation: {ex.Message}");
        }
    }
}
