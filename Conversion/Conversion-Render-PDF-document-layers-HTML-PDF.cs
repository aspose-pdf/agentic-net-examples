using System;
using System.IO;
using Aspose.Pdf; // Required for HtmlLoadOptions and Document types

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Verify the HTML source exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Determine the base directory for relative resources in the HTML file.
        // HtmlLoadOptions.BasePath is read‑only, so use the constructor that accepts a base path.
        string baseDir = Path.GetDirectoryName(Path.GetFullPath(htmlPath));
        HtmlLoadOptions loadOptions = new HtmlLoadOptions(baseDir);

        // Optional: render the whole HTML on a single PDF page.
        loadOptions.IsRenderToSinglePage = true;

        // Load the HTML and convert it to PDF. Wrap the Document in a using block for deterministic disposal.
        using (Document pdfDoc = new Document(htmlPath, loadOptions))
        {
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: '{pdfPath}'");
    }
}