using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input HTML and output PDF
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify that the source HTML file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load options: disable embedding of fonts into the resulting PDF
        HtmlLoadOptions loadOptions = new HtmlLoadOptions
        {
            IsEmbedFonts = false   // fonts will not be embedded
        };

        // Load the HTML document and convert it to PDF
        using (Document pdfDoc = new Document(htmlPath, loadOptions))
        {
            // Save the document as PDF (no special SaveOptions required)
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"Conversion completed. PDF saved to '{pdfPath}'.");
    }
}