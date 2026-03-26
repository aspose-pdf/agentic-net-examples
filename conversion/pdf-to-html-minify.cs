using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string outputHtml = "output.html";      // generated HTML

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"PDF file not found: {inputPdf}");
            return;
        }

        // Convert PDF to HTML
        using (Document pdfDoc = new Document(inputPdf))
        {
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions();
            pdfDoc.Save(outputHtml, htmlOpts);
        }

        // Minify the generated HTML
        string htmlContent = File.ReadAllText(outputHtml);
        // Collapse multiple whitespace characters into a single space
        string minified = Regex.Replace(htmlContent, @"\s+", " ");
        // Remove whitespace between tags
        minified = Regex.Replace(minified, @">\s+<", "><");
        // Trim leading/trailing spaces
        minified = minified.Trim();
        File.WriteAllText(outputHtml, minified);

        Console.WriteLine($"PDF converted and minified HTML saved to '{outputHtml}'.");
    }
}