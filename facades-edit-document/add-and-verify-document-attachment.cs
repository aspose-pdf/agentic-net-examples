using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";
        const string attachmentFile = "attachment.txt";
        const string outputPdf = "output_with_attachment.pdf";

        // -------------------------------------------------
        // 1. Prepare required files (inline creation)
        // -------------------------------------------------
        // Create a minimal source PDF if it does not already exist.
        if (!File.Exists(sourcePdf))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(sourcePdf);
            }
        }

        // Create a simple text file to be attached if it does not already exist.
        if (!File.Exists(attachmentFile))
        {
            File.WriteAllText(attachmentFile, "This is a sample attachment.");
        }

        // -------------------------------------------------
        // 2. Add a document attachment using PdfContentEditor
        // -------------------------------------------------
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(sourcePdf);
        // AddDocumentAttachment(string fileAttachmentPath, string description)
        editor.AddDocumentAttachment(attachmentFile, "Sample attachment description");
        editor.Save(outputPdf); // Save the modified PDF

        // -------------------------------------------------
        // 3. Verify the attachment by retrieving its file specification name
        // -------------------------------------------------
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(outputPdf);
        // Must call ExtractAttachment before querying names
        extractor.ExtractAttachment();
        IList<string> attachmentNames = extractor.GetAttachNames();

        // Output the retrieved attachment names
        Console.WriteLine("Attachments found in the PDF:");
        foreach (string name in attachmentNames)
        {
            Console.WriteLine($"- {name}");
        }

        // Simple verification: check that our expected file name is present
        if (attachmentNames.Contains(Path.GetFileName(attachmentFile)))
        {
            Console.WriteLine("Attachment verification succeeded.");
        }
        else
        {
            Console.WriteLine("Attachment verification failed.");
        }
    }
}