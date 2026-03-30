using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputPdfPath = "input.pdf";
        string attachmentFilePath = "attachment.txt";
        string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdfPath);
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine("Attachment file not found: " + attachmentFilePath);
            return;
        }

        // Add a document attachment (no visual annotation)
        PdfContentEditor contentEditor = new PdfContentEditor();
        contentEditor.BindPdf(inputPdfPath);
        contentEditor.AddDocumentAttachment(attachmentFilePath, "Sample attachment");
        contentEditor.Save(outputPdfPath);

        // Retrieve attachment information to verify
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(outputPdfPath);
        List<FileSpecification> attachmentInfo = extractor.GetAttachmentInfo();

        if (attachmentInfo != null && attachmentInfo.Count > 0)
        {
            FileSpecification firstSpec = attachmentInfo[0];
            string attachmentName = firstSpec.Name;
            Console.WriteLine("First attachment name: " + attachmentName);
        }
        else
        {
            Console.WriteLine("No attachments found.");
        }
    }
}