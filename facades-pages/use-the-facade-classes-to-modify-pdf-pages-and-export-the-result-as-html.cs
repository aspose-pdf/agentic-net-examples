using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the facade and modify its pages
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Initialize the facade with the source PDF
            editor.BindPdf(inputPdf);

            // Example modifications:
            // Rotate all pages by 90 degrees (int literal)
            editor.Rotation = 90; // Rotation property expects an int
            // Zoom the content to 120% (int literal)
            editor.Zoom = 120; // Zoom property expects an int (percentage)

            // Apply the changes to the underlying document
            editor.ApplyChanges();

            // Retrieve the modified Document from the facade
            Document doc = editor.Document;

            // Export the modified PDF to HTML using explicit HtmlSaveOptions
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions();
            doc.Save(outputHtml, htmlOptions);
        }

        Console.WriteLine($"HTML saved to '{outputHtml}'.");
    }
}
