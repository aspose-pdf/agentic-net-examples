using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "ExtractedAttachments";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Create a minimal PDF with an attachment if it does not already exist
        if (!File.Exists(pdfPath))
        {
            using (var doc = new Document())
            {
                doc.Pages.Add();

                // Create a simple text attachment in memory
                string attachmentContent = "This is a sample attachment.";
                byte[] attachmentBytes = Encoding.UTF8.GetBytes(attachmentContent);
                using (var ms = new MemoryStream(attachmentBytes))
                {
                    // Add the attachment to the PDF using EmbeddedFiles collection
                    var fileSpec = new FileSpecification("sample.txt", "Sample attachment");
                    fileSpec.Contents = ms; // assign the stream containing the file data
                    doc.EmbeddedFiles.Add(fileSpec);
                }

                doc.Save(pdfPath);
            }
        }

        // Extract attachments and rename each with a timestamp prefix
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            extractor.ExtractAttachment();

            IList<string> attachmentNames = extractor.GetAttachNames();
            MemoryStream[] attachmentStreams = extractor.GetAttachment();

            for (int i = 0; i < attachmentStreams.Length; i++)
            {
                string originalName = attachmentNames[i];
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_";
                string newFileName = timestamp + originalName;
                string outputPath = Path.Combine(outputDir, newFileName);

                // Ensure the stream is positioned at the beginning
                attachmentStreams[i].Position = 0;
                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    attachmentStreams[i].CopyTo(fs);
                }

                Console.WriteLine($"Saved attachment: {outputPath}");
            }
        }
    }
}
