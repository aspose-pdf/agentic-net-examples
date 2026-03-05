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
            Console.Error.WriteLine($"File not found: {htmlPath}");
            return;
        }

        // Load the HTML document. HtmlLoadOptions can be customized if needed.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Use a using block to ensure the Document is disposed properly.
        using (Document doc = new Document(htmlPath, loadOptions))
        {
            // The HtmlMarkupGenerationMode property belongs to HtmlSaveOptions,
            // which is used when saving PDF → HTML. It does not affect HTML → PDF
            // conversion, but we set it here as requested.
            HtmlSaveOptions htmlSaveOpts = new HtmlSaveOptions
            {
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent
            };

            // Save the loaded document as PDF. The HtmlSaveOptions instance is not
            // needed for this operation, but it demonstrates the required setting.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
    }
}