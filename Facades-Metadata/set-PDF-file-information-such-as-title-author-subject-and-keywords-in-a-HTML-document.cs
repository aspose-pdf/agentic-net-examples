using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string updatedPdf = "updated.pdf";        // PDF after metadata update
        const string outputHtml = "output.html";        // final HTML file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // ---------- Set PDF metadata ----------
        // PdfFileInfo works on an existing PDF file.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            pdfInfo.Title    = "Sample Document Title";
            pdfInfo.Author   = "John Doe";
            pdfInfo.Subject  = "Sample Subject";
            pdfInfo.Keywords = "keyword1, keyword2";

            // Save the PDF with the new metadata to a temporary file.
            pdfInfo.SaveNewInfo(updatedPdf);
        }

        // ---------- Convert the updated PDF to HTML ----------
        // Document must be disposed via using.
        using (Document doc = new Document(updatedPdf))
        {
            // Explicitly pass HtmlSaveOptions; otherwise a PDF would be written.
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions();
            doc.Save(outputHtml, htmlOpts);
        }

        Console.WriteLine($"HTML document created at '{outputHtml}'.");
    }
}