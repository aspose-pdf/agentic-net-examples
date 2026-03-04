using System;
using System.IO;
using Aspose.Pdf;   // Aspose.Pdf namespace contains Document, HtmlLoadOptions, etc.

class Program
{
    static void Main()
    {
        // -----------------------------------------------------------------
        // Define the directory that holds the source HTML and the target PDF.
        // Adjust the path as needed for your environment.
        // -----------------------------------------------------------------
        string dataDir = Path.Combine(Environment.CurrentDirectory, "Data");
        Directory.CreateDirectory(dataDir); // Ensure the folder exists.

        // Input HTML file (must exist before running the program).
        string htmlFile = Path.Combine(dataDir, "HTML-to-PDF.html");

        // Output PDF file.
        string pdfFile = Path.Combine(dataDir, "HTML-to-PDF.pdf");

        // -----------------------------------------------------------------
        // Verify that the source HTML file is present; otherwise exit gracefully.
        // -----------------------------------------------------------------
        if (!File.Exists(htmlFile))
        {
            Console.Error.WriteLine($"Error: Input HTML file not found at '{htmlFile}'.");
            return;
        }

        // -----------------------------------------------------------------
        // Load the HTML document using HtmlLoadOptions (default options are fine).
        // The Document constructor with (string filename, LoadOptions options) is the
        // correct lifecycle method for loading non‑PDF sources.
        // -----------------------------------------------------------------
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Use a using block to ensure the Document is disposed properly.
        using (Document pdfDocument = new Document(htmlFile, loadOptions))
        {
            // -----------------------------------------------------------------
            // Save the loaded content as a PDF file.
            // For PDF output, Document.Save(string) without SaveOptions is sufficient.
            // -----------------------------------------------------------------
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfFile}");
    }
}