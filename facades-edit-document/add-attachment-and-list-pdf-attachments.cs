using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentFile = "attachment.txt";
        const string outputPdf = "output_with_attachment.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        // Add attachment to the PDF using PdfContentEditor (facade API)
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);
        editor.AddDocumentAttachment(attachmentFile, "Sample attachment");
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");

        // List all attachment names using PdfExtractor
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(outputPdf);
        extractor.ExtractAttachment(); // required before GetAttachNames
        var attachmentNames = extractor.GetAttachNames();

        Console.WriteLine("Attachments in the PDF:");
        foreach (string name in attachmentNames)
        {
            Console.WriteLine($"- {name}");
        }
    }
}