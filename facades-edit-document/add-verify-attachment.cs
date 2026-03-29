using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main()
    {
        string inputPdfPath = "input.pdf";
        string attachmentFilePath = "attachment.pdf";
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

        // Add attachment to PDF
        PdfContentEditor contentEditor = new PdfContentEditor();
        contentEditor.BindPdf(inputPdfPath);
        contentEditor.AddDocumentAttachment(attachmentFilePath, "Sample attachment");
        contentEditor.Save(outputPdfPath);

        // Extract attachment from the new PDF
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(outputPdfPath);
        extractor.ExtractAttachment();
        IList<string> attachmentNames = extractor.GetAttachNames();
        MemoryStream[] attachmentStreams = extractor.GetAttachment();

        if (attachmentNames.Count == 0 || attachmentStreams.Length == 0)
        {
            Console.Error.WriteLine("No attachments found in the PDF.");
            return;
        }

        // Assume first attachment
        MemoryStream extractedStream = attachmentStreams[0];
        extractedStream.Position = 0;
        byte[] extractedBytes = extractedStream.ToArray();

        // Read original attachment bytes
        byte[] originalBytes = File.ReadAllBytes(attachmentFilePath);

        bool isEqual = (originalBytes.Length == extractedBytes.Length);
        if (isEqual)
        {
            for (int i = 0; i < originalBytes.Length; i++)
            {
                if (originalBytes[i] != extractedBytes[i])
                {
                    isEqual = false;
                    break;
                }
            }
        }

        if (isEqual)
        {
            Console.WriteLine("Attachment verification succeeded: extracted content matches the original file.");
        }
        else
        {
            Console.WriteLine("Attachment verification failed: extracted content differs from the original file.");
        }

        // Clean up
        extractedStream.Close();
    }
}
