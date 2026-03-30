using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentFile = "attachment.txt";
        const string outputPdf = "output.pdf";

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

        // Add the attachment to the PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);
        editor.AddDocumentAttachment(attachmentFile, "Sample attachment");
        editor.Save(outputPdf);
        editor.Close();

        // List all attachment names in the resulting PDF
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(outputPdf);
        extractor.ExtractAttachment();
        IList<string> attachmentNames = extractor.GetAttachNames();
        Console.WriteLine("Attachments in the PDF:");
        foreach (string name in attachmentNames)
        {
            Console.WriteLine(name);
        }
    }
}