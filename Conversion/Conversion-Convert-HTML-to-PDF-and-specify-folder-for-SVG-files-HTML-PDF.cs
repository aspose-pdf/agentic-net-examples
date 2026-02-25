using System;
using System.IO;
using Aspose.Pdf; // All Aspose.Pdf types are in this namespace

class Program
{
    static void Main()
    {
        // Input HTML file and desired PDF output file
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify that the source HTML file exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // HtmlLoadOptions are used when loading an HTML document into Aspose.Pdf
        // No SVG‑folder property exists on HtmlLoadOptions; the property belongs to HtmlSaveOptions
        Aspose.Pdf.HtmlLoadOptions loadOptions = new Aspose.Pdf.HtmlLoadOptions();

        // Wrap the Document in a using block for deterministic disposal (lifecycle rule)
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(htmlPath, loadOptions))
        {
            // Save the loaded document as PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
    }
}