using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output Markdown file path
        const string outputMd = "output.md";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create MarkdownSaveOptions to control the conversion.
            // The default options preserve paragraph indentation and other formatting.
            MarkdownSaveOptions mdOptions = new MarkdownSaveOptions();

            // Save the PDF as a formatted Markdown file.
            // This conversion keeps the original paragraph indentation.
            pdfDoc.Save(outputMd, mdOptions);
        }

        Console.WriteLine($"Markdown file created: {outputMd}");
    }
}