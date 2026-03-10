using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string attachmentFile = "attachment.txt";
        const string outputPdf = "sample_with_attachment.pdf";
        const string extractedDir = "ExtractedAttachments";
        const string cleanedPdf = "sample_cleaned.pdf";

        // Verify required files exist
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

        // -------------------------------------------------
        // 1. Add a file attachment to the PDF
        // -------------------------------------------------
        PdfContentEditor addEditor = new PdfContentEditor();
        addEditor.BindPdf(inputPdf);
        // Add the external file as an attachment with a description
        addEditor.AddDocumentAttachment(attachmentFile, "Sample attachment");
        addEditor.Save(outputPdf);
        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");

        // -------------------------------------------------
        // 2. Extract all attachments from the PDF
        // -------------------------------------------------
        Directory.CreateDirectory(extractedDir);
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(outputPdf);
        extractor.ExtractAttachment(); // Prepare extraction of all attachments

        // Retrieve attachment names (optional, for naming the output files)
        var names = extractor.GetAttachNames(); // IList of attachment file names

        // Retrieve attachment streams
        MemoryStream[] attachmentStreams = extractor.GetAttachment();

        for (int i = 0; i < attachmentStreams.Length; i++)
        {
            string name = (string)names[i];
            string outPath = Path.Combine(extractedDir, name);
            using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
            {
                attachmentStreams[i].Position = 0;
                attachmentStreams[i].CopyTo(fs);
            }
            Console.WriteLine($"Extracted attachment saved to '{outPath}'.");
        }

        // -------------------------------------------------
        // 3. Delete all attachments from the PDF
        // -------------------------------------------------
        PdfContentEditor deleteEditor = new PdfContentEditor();
        deleteEditor.BindPdf(outputPdf);
        deleteEditor.DeleteAttachments();
        deleteEditor.Save(cleanedPdf);
        Console.WriteLine($"All attachments removed. Clean PDF saved to '{cleanedPdf}'.");
    }
}