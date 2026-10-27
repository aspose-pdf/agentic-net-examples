using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputFolder = "Attachments";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        Document pdfDoc = new Document(pdfPath);

        // Extract all embedded attachments
        if (pdfDoc.EmbeddedFiles != null && pdfDoc.EmbeddedFiles.Count > 0)
        {
            foreach (FileSpecification spec in pdfDoc.EmbeddedFiles)
            {
                // Determine a safe file name for the attachment
                string attachmentName = string.IsNullOrEmpty(spec.Name)
                    ? Path.GetFileNameWithoutExtension(pdfPath) + "_attachment"
                    : spec.Name;

                // Replace any invalid file‑name characters
                foreach (char c in Path.GetInvalidFileNameChars())
                    attachmentName = attachmentName.Replace(c, '_');

                string destPath = Path.Combine(outputFolder, attachmentName);

                // Ensure the stream is at the beginning before copying
                spec.Contents.Position = 0;
                using (FileStream fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
                {
                    spec.Contents.CopyTo(fileStream);
                }
            }
        }
        else
        {
            Console.WriteLine("No embedded attachments found in the PDF.");
        }

        // Compute SHA‑256 hash for each extracted attachment
        foreach (string filePath in Directory.GetFiles(outputFolder))
        {
            using (FileStream stream = File.OpenRead(filePath))
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(stream);
                string hashHex = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                Console.WriteLine($"{Path.GetFileName(filePath)}: {hashHex}");
            }
        }
    }
}
