using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDirectory = "attachments";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractAttachment();

            IList<string> attachmentNames = extractor.GetAttachNames();
            MemoryStream[] attachmentStreams = extractor.GetAttachment();

            for (int i = 0; i < attachmentStreams.Length; i++)
            {
                string attachmentName = attachmentNames[i];
                string savedPath = Path.Combine(outputDirectory, attachmentName);

                // Save the attachment to disk
                using (FileStream fileStream = new FileStream(savedPath, FileMode.Create, FileAccess.Write))
                {
                    attachmentStreams[i].Position = 0;
                    attachmentStreams[i].CopyTo(fileStream);
                }

                // Compute SHA‑256 hash of the saved file
                using (FileStream readStream = new FileStream(savedPath, FileMode.Open, FileAccess.Read))
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(readStream);
                    string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                    Console.WriteLine($"Attachment: {attachmentName}, SHA‑256: {hashString}");
                }
            }
        }
    }
}
