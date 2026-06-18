using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";               // source PDF
        const string attachmentPath = "attachment.bin";    // file to attach
        const string outputPdf = "output.pdf";             // result PDF

        // Verify required files exist
        if (!File.Exists(inputPdf) || !File.Exists(attachmentPath))
        {
            Console.Error.WriteLine("Input PDF or attachment file not found.");
            return;
        }

        // ---------- 1. Calculate MD5 checksum of the attachment ----------
        string checksum;
        using (FileStream attStream = File.OpenRead(attachmentPath))
        using (MD5 md5 = MD5.Create())
        {
            byte[] hash = md5.ComputeHash(attStream);
            // Convert to lower‑case hex string (16‑byte MD5)
            checksum = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        // ---------- 2. Add the attachment to the PDF ----------
        // PdfContentEditor is a Facades class that can modify PDFs.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf); // load the source PDF

            // Add the file as a document attachment (no visible annotation)
            editor.AddDocumentAttachment(attachmentPath, "Attached file for integrity check");

            // Save to a temporary file because we will modify metadata next
            string tempPdf = Path.GetTempFileName();
            editor.Save(tempPdf);

            // ---------- 3. Store the checksum in custom metadata ----------
            // PdfFileInfo provides access to PDF metadata (custom key/value pairs).
            using (PdfFileInfo info = new PdfFileInfo(tempPdf))
            {
                // Store the checksum under a custom key, e.g., "AttachmentChecksum"
                info.SetMetaInfo("AttachmentChecksum", checksum);

                // Save the updated PDF to the final output path
                info.SaveNewInfo(outputPdf);
            }

            // Clean up the temporary file
            File.Delete(tempPdf);
        }

        Console.WriteLine($"Attachment added and checksum stored in metadata. Output: {outputPdf}");
    }
}