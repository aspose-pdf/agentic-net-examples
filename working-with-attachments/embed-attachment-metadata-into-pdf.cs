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
        const string attachmentPath = "attachment.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Add the attachment without creating an annotation
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(doc);
                editor.AddDocumentAttachment(attachmentPath, "Sample attachment");
            }

            // Embed attachment metadata into the document information dictionary
            // Custom keys can be any string; here we store file name and description
            doc.Info.Add("AttachmentFileName", Path.GetFileName(attachmentPath));
            doc.Info.Add("AttachmentDescription", "Sample attachment");

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with attachment and metadata to '{outputPdf}'.");
    }
}