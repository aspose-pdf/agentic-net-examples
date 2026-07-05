using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputMd  = "output.md";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create default Markdown save options
            MarkdownSaveOptions mdOptions = new MarkdownSaveOptions();

            // Save the document as Markdown using the options
            pdfDoc.Save(outputMd, mdOptions);
        }

        Console.WriteLine($"PDF successfully converted to Markdown: '{outputMd}'");
    }
}