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

        // Configure load options: use Print media type for rendering
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();
        loadOptions.HtmlMediaType = HtmlMediaType.Print;

        // Load the HTML file into a PDF document using the configured options
        using (Document pdfDoc = new Document(htmlPath, loadOptions))
        {
            // Save the resulting PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"Conversion completed: {pdfPath}");
    }
}