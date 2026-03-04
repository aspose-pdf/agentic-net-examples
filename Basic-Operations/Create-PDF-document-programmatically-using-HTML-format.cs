using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source HTML file and where the PDF will be written.
        const string dataDir = "YOUR_DATA_DIRECTORY";

        // Path to the input HTML file.
        string htmlPath = Path.Combine(dataDir, "input.html");

        // Path to the output PDF file.
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the HTML source exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load the HTML file into a Document using HtmlLoadOptions.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions(); // empty base path
        using (Document pdfDoc = new Document(htmlPath, loadOptions))
        {
            // Save the loaded document as a PDF.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
    }
}