using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input HTML file and desired PDF output file.
        const string htmlPath = "input.html";
        const string pdfPath  = "output.pdf";

        // Folder where any SVG images referenced in the HTML will be stored.
        // Aspose.Pdf extracts SVG resources when loading HTML; ensuring the folder exists avoids errors.
        const string svgFolder = "SvgImages";
        Directory.CreateDirectory(svgFolder);

        // Verify that the source HTML file exists.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // HtmlLoadOptions can receive a base path; using the directory of the HTML file helps resolve relative resources.
        string basePath = Path.GetDirectoryName(Path.GetFullPath(htmlPath));
        HtmlLoadOptions loadOptions = new HtmlLoadOptions(basePath);

        // Load the HTML document into an Aspose.Pdf Document.
        using (Document doc = new Document(htmlPath, loadOptions))
        {
            // The Document now contains the content of the HTML, including any extracted SVG images.
            // Save the document as PDF.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
    }
}