using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Included as requested

class HtmlToPdfConverter
{
    static void Main()
    {
        // Directory that contains the source HTML file and where the PDF will be written
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Build full paths for input HTML and output PDF
        string htmlPath = Path.Combine(dataDir, "HTML-to-PDF.html");
        string pdfPath  = Path.Combine(dataDir, "HTML-to-PDF.pdf");

        // Verify that the source HTML file exists to avoid FileNotFoundException
        if (!File.Exists(htmlPath))
        {
            Console.WriteLine($"Source HTML file not found: {htmlPath}");
            return;
        }

        // Create load options for HTML conversion (base path is optional here)
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Load the HTML file into a PDF Document using the provided load rule
        using (Document pdfDocument = new Document(htmlPath, loadOptions))
        {
            // Save the resulting PDF using the provided save rule
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF. Output file: {pdfPath}");
    }
}