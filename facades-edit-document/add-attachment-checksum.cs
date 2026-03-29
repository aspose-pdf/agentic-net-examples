using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachmentFile = "attachment_file.pdf";

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

        using (Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(inputPdf))
        {
            // Embed the file as a document attachment (no visual annotation)
            Aspose.Pdf.FileSpecification fileSpec = new Aspose.Pdf.FileSpecification(attachmentFile, "Sample attachment");
            pdfDocument.EmbeddedFiles.Add(fileSpec);

            // Retrieve the MD5 checksum from the embedded file parameters
            Aspose.Pdf.FileParams fileParams = fileSpec.Params;
            if (fileParams != null)
            {
                string checksum = fileParams.CheckSum;
                // Store the checksum in custom metadata using the DocumentInfo indexer
                pdfDocument.Info["AttachmentChecksum"] = checksum;
                Console.WriteLine($"Checksum stored in metadata: {checksum}");
            }
            else
            {
                Console.WriteLine("FileParams is null; checksum not available.");
            }

            // Save the updated PDF
            pdfDocument.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with attachment and checksum: {outputPdf}");
    }
}