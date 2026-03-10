using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdfPath      = "source.pdf";          // PDF to which we will attach a file
        const string attachmentFilePath = "attachment.pdf";      // File to embed (must exist)
        const string attachedPdfPath    = "with_attachment.pdf"; // Resulting PDF containing the attachment
        const string extractedFolder    = "ExtractedAttachments"; // Folder to store extracted files

        // Verify input files
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // -------------------------------------------------
        // 1. Attach a file to the PDF (no visual annotation)
        // -------------------------------------------------
        Aspose.Pdf.Facades.PdfContentEditor editor = new Aspose.Pdf.Facades.PdfContentEditor();
        editor.BindPdf(sourcePdfPath);
        // description can be any string; here we use the file name
        editor.AddDocumentAttachment(attachmentFilePath, $"Embedded file: {Path.GetFileName(attachmentFilePath)}");
        editor.Save(attachedPdfPath);
        Console.WriteLine($"Attachment added. Saved as: {attachedPdfPath}");

        // -------------------------------------------------
        // 2. Extract all embedded attachments from the PDF
        // -------------------------------------------------
        // Ensure the output directory exists
        Directory.CreateDirectory(extractedFolder);

        Aspose.Pdf.Facades.PdfExtractor extractor = new Aspose.Pdf.Facades.PdfExtractor();
        extractor.BindPdf(attachedPdfPath);
        extractor.ExtractAttachment();                     // Prepare extraction
        IList<string> attachmentNames = extractor.GetAttachNames(); // Names of embedded files
        MemoryStream[] attachmentStreams = extractor.GetAttachment(); // Content streams

        for (int i = 0; i < attachmentStreams.Length; i++)
        {
            string name = attachmentNames[i];
            string outputPath = Path.Combine(extractedFolder, name);

            // Write the stream to a file
            using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                attachmentStreams[i].Position = 0;
                attachmentStreams[i].CopyTo(fs);
            }

            Console.WriteLine($"Extracted attachment saved to: {outputPath}");
        }

        Console.WriteLine("All attachments have been extracted.");
    }
}