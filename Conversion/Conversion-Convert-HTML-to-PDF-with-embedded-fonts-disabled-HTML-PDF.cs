using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load the HTML file with font embedding turned off
        HtmlLoadOptions loadOptions = new HtmlLoadOptions
        {
            IsEmbedFonts = false
        };

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(htmlPath, loadOptions))
        {
            // Save the document as PDF (no SaveOptions needed)
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Conversion completed: '{pdfPath}' (fonts not embedded).");
    }
}