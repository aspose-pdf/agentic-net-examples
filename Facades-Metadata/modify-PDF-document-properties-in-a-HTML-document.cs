using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string htmlFile   = "input.html";   // source HTML
        const string tempPdf    = "temp.pdf";     // intermediate PDF
        const string outputPdf  = "output.pdf";   // final PDF with updated properties

        // Verify the HTML source exists
        if (!File.Exists(htmlFile))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlFile}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Convert the HTML document to PDF using the core Document API.
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(htmlFile, new HtmlLoadOptions()))
        {
            // Save the intermediate PDF file; this file will be used by the facade.
            pdfDoc.Save(tempPdf);
        }

        // -----------------------------------------------------------------
        // 2. Modify PDF document properties (metadata) using the PdfFileInfo facade.
        // -----------------------------------------------------------------
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            // Bind the intermediate PDF to the facade.
            pdfInfo.BindPdf(tempPdf);

            // Set desired metadata properties.
            pdfInfo.Title    = "Modified Title";
            pdfInfo.Author   = "John Doe";
            pdfInfo.Subject  = "Sample Subject";
            pdfInfo.Keywords = "Aspose, PDF, HTML conversion";

            // Save the updated PDF to the final output file.
            pdfInfo.SaveNewInfo(outputPdf);
        }

        // Clean up the temporary PDF file.
        try
        {
            File.Delete(tempPdf);
        }
        catch
        {
            // If deletion fails, ignore – the file is not critical.
        }

        Console.WriteLine($"PDF created and properties set: {outputPdf}");
    }
}