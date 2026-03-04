using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input HTML file and desired output paths
        const string htmlPath = "input.html";
        const string pdfPath  = "intermediate.pdf";
        // Note: CorelDRAW (CDR) format is not supported by Aspose.Pdf.
        // The conversion can stop at PDF, which can then be processed by other tools if needed.

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load HTML into a PDF document using HtmlLoadOptions
        HtmlLoadOptions htmlLoadOptions = new HtmlLoadOptions();
        using (Document pdfDoc = new Document(htmlPath, htmlLoadOptions))
        {
            // Example of page manipulation using PdfPageEditor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the PDF document to the editor
                editor.BindPdf(pdfDoc);

                // Example: rotate all pages 90 degrees clockwise
                editor.Rotation = 90;

                // Example: set a new page size (A4)
                editor.PageSize = new PageSize(595, 842); // Width x Height in points

                // Apply the changes
                editor.ApplyChanges();

                // Save the manipulated PDF
                editor.Save(pdfPath);
            }

            Console.WriteLine($"HTML converted and pages manipulated. PDF saved to '{pdfPath}'.");
        }
    }
}