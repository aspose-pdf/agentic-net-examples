using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string markdownPath = "input.md";
        const string pdfPath      = "output.pdf";

        if (!File.Exists(markdownPath))
        {
            Console.Error.WriteLine($"File not found: {markdownPath}");
            return;
        }

        // Load the Markdown file using MdLoadOptions and convert it to a PDF document.
        using (Document doc = new Document(markdownPath, new MdLoadOptions()))
        {
            // Save the resulting document as PDF. No SaveOptions are needed for PDF output.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Markdown converted to PDF: '{pdfPath}'");
    }
}