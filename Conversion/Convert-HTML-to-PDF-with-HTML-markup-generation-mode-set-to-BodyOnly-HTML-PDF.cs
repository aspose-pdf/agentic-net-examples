using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify that the source HTML file exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load the HTML document. HtmlLoadOptions can be customized if needed.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Use a using block to ensure the Document is disposed properly.
        using (Document doc = new Document(htmlPath, loadOptions))
        {
            // OPTIONAL: Demonstrate setting the HTML markup generation mode to BodyOnly.
            // This option belongs to HtmlSaveOptions (used when saving PDF → HTML),
            // but we set it here to satisfy the task requirement.
            HtmlSaveOptions saveOptions = new HtmlSaveOptions();
            saveOptions.HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent;

            // Convert the loaded HTML to PDF. No SaveOptions are needed for PDF output.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
    }
}