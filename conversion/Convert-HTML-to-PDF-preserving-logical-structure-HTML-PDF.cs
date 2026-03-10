using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class HtmlToPdfConverter
{
    static void Main()
    {
        // Input HTML file and output PDF file paths
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify that the source HTML file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Configure HTML loading options (default constructor is sufficient for most cases)
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Load the HTML document into a PDF Document object
        using (Document pdfDoc = new Document(htmlPath, loadOptions))
        {
            // Enable automatic tagging so that the resulting PDF contains a logical structure
            AutoTaggingSettings.Default.EnableAutoTagging = true;

            // Optionally, configure heading detection (auto‑detect headings based on font size)
            // AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto;

            // Save the document as PDF – logical structure is preserved because auto‑tagging is enabled
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF with logical structure: {pdfPath}");
    }
}