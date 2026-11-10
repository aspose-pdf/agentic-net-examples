using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF containing attachments
        const string pdfPath = "input.pdf";

        // Folder where extracted attachments will be saved
        const string outputFolder = "ExtractedAttachments";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        Document pdfDoc;
        try
        {
            pdfDoc = new Document(pdfPath);
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"Failed to load PDF: {ex.Message}");
            return;
        }

        // Extract each embedded file (attachment) and save it to the output folder
        if (pdfDoc.EmbeddedFiles != null && pdfDoc.EmbeddedFiles.Count > 0)
        {
            foreach (FileSpecification fileSpec in pdfDoc.EmbeddedFiles)
            {
                string fileName = fileSpec.Name; // use Name property
                string destPath = Path.Combine(outputFolder, fileName);
                try
                {
                    using (FileStream outStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
                    {
                        // Ensure the stream is at the beginning
                        if (fileSpec.Contents.CanSeek)
                            fileSpec.Contents.Position = 0;
                        // Copy the attachment data to disk
                        fileSpec.Contents.CopyTo(outStream);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error saving attachment '{fileName}': {ex.Message}");
                }
            }
        }
        else
        {
            Console.WriteLine("No embedded attachments found in the PDF.");
        }

        // Compute SHA‑256 hash for each extracted file
        foreach (string filePath in Directory.GetFiles(outputFolder))
        {
            try
            {
                byte[] hashBytes;
                using (FileStream stream = File.OpenRead(filePath))
                using (SHA256 sha256 = SHA256.Create())
                {
                    hashBytes = sha256.ComputeHash(stream);
                }

                // Convert hash to hexadecimal string
                string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();

                Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                Console.WriteLine($"SHA‑256: {hashString}");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }
    }
}
