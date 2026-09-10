using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added namespace for TextFragment

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachmentFile = "attachment_file.pdf";
        const string attachmentDescription = "Sample attachment";

        // ------------------------------------------------------------
        // 1. Create a minimal source PDF (input.pdf) – the sandbox has no files.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            using (Document src = new Document())
            {
                src.Pages.Add(); // add a blank page
                src.Save(inputPdf);
            }
        }

        // ------------------------------------------------------------
        // 2. Create a minimal attachment PDF (attachment_file.pdf).
        // ------------------------------------------------------------
        if (!File.Exists(attachmentFile))
        {
            using (Document att = new Document())
            {
                Page page = att.Pages.Add();
                // Add a simple paragraph so the file is not empty.
                page.Paragraphs.Add(new TextFragment("This is a sample attachment PDF."));
                att.Save(attachmentFile);
            }
        }

        // ------------------------------------------------------------
        // 3. Calculate MD5 checksum of the attachment file.
        // ------------------------------------------------------------
        string checksum;
        using (FileStream fs = File.OpenRead(attachmentFile))
        using (MD5 md5 = MD5.Create())
        {
            byte[] hash = md5.ComputeHash(fs);
            checksum = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        // ------------------------------------------------------------
        // 4. Add the attachment to the PDF using PdfContentEditor (Facades API).
        // ------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);
            editor.AddDocumentAttachment(attachmentFile, attachmentDescription);
            editor.Save(outputPdf);
        }

        // ------------------------------------------------------------
        // 5. Store the checksum in custom metadata using PdfFileInfo.
        // ------------------------------------------------------------
        using (PdfFileInfo info = new PdfFileInfo(outputPdf))
        {
            info.SetMetaInfo("AttachmentChecksum", checksum);
            // Save the updated metadata back to the same file.
            info.SaveNewInfo(outputPdf);
        }

        Console.WriteLine($"Attachment added and checksum stored: {checksum}");
    }
}