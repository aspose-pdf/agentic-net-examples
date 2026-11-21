using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string markdownPath = "input.md";
        const string pdfPath = "output.pdf";

        if (!File.Exists(markdownPath))
        {
            Console.Error.WriteLine($"File not found: {markdownPath}");
            return;
        }

        // Load options for Markdown files. Default settings preserve code blocks.
        MdLoadOptions loadOptions = new MdLoadOptions();

        // Load the Markdown document and convert it to PDF.
        using (Document doc = new Document(markdownPath, loadOptions))
        {
            // Save the result as a PDF file.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Markdown successfully converted to PDF: {pdfPath}");
    }
}