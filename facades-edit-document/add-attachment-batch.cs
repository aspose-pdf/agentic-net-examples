using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string[] pdfFiles = { "input1.pdf", "input2.pdf", "input3.pdf" };
        string attachmentPath = "attachment.pdf";
        string attachmentDescription = "Sample attachment";

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(pdfPath);
                editor.AddDocumentAttachment(attachmentPath, attachmentDescription);
                editor.Save("output.pdf");
            }

            Console.WriteLine($"Attachment added to {pdfPath} and saved as output.pdf");
        }
    }
}