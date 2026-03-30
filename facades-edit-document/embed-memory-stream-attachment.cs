using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }

        // Create a memory stream containing the attachment data
        byte[] attachmentData = System.Text.Encoding.UTF8.GetBytes("This is a sample attachment content.");
        using (MemoryStream attachmentStream = new MemoryStream(attachmentData))
        {
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(inputPdf);
                editor.AddDocumentAttachment(attachmentStream, "sample.txt", "Sample attachment from memory stream");
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine("Attachment added and saved to '" + outputPdf + "'.");
    }
}