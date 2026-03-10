using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Retrieve metadata using the PdfFileInfo facade
        PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf);
        string title    = pdfInfo.Title;
        string author   = pdfInfo.Author;
        string subject  = pdfInfo.Subject;
        string keywords = pdfInfo.Keywords;

        // Load the PDF document and apply the retrieved metadata
        using (Document doc = new Document(inputPdf))
        {
            doc.Info.Title    = title;
            doc.Info.Author   = author;
            doc.Info.Subject  = subject;
            doc.Info.Keywords = keywords;

            // Aspose.Pdf does not support saving directly to PostScript (PS) format.
            // The document is saved as PDF; to obtain a PS file you would need to
            // print the PDF using a PostScript printer driver outside of Aspose.Pdf.
            doc.Save(outputPdf);
        }

        Console.WriteLine("Metadata transferred and PDF saved.");
    }
}