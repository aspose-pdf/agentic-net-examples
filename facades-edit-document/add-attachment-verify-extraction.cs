using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";          // Original PDF (will be created if missing)
        const string attachmentPath = "attachment.bin";   // File to attach (will be created if missing)
        const string outputPdfPath = "output_with_attachment.pdf";
        const string extractedDir = "extracted";         // Folder for extracted files

        // ------------------------------------------------------------
        // 1. Ensure required files exist (self‑contained example)
        // ------------------------------------------------------------
        // Create a minimal PDF if it does not exist
        if (!File.Exists(inputPdfPath))
        {
            var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPdfPath);
        }

        // Create a simple binary attachment if it does not exist
        if (!File.Exists(attachmentPath))
        {
            // Example content – 4 bytes; replace with any data you need
            byte[] sampleData = new byte[] { 0xDE, 0xAD, 0xBE, 0xEF };
            File.WriteAllBytes(attachmentPath, sampleData);
        }

        // Ensure the extraction folder exists
        Directory.CreateDirectory(extractedDir);

        // ------------------------------------------------------------
        // 2. Add the attachment to the PDF using PdfContentEditor
        // ------------------------------------------------------------
        var editor = new PdfContentEditor();
        editor.BindPdf(inputPdfPath);
        editor.AddDocumentAttachment(attachmentPath, "Sample attachment");
        editor.Save(outputPdfPath);
        editor.Close(); // Facade cleanup

        // ------------------------------------------------------------
        // 3. Extract attachments from the edited PDF using PdfExtractor
        // ------------------------------------------------------------
        var extractor = new PdfExtractor();
        extractor.BindPdf(outputPdfPath);
        extractor.ExtractAttachment();                     // Must be called before GetAttachNames
        IList<string> attachNames = extractor.GetAttachNames();    // List of attachment file names

        // Retrieve all attachment streams
        MemoryStream[] attachmentStreams = extractor.GetAttachment();

        // Load original attachment bytes for comparison
        byte[] originalBytes = File.ReadAllBytes(attachmentPath);

        bool matchFound = false;

        // Iterate over extracted attachments and compare with the original file
        for (int i = 0; i < attachmentStreams.Length; i++)
        {
            // Reset stream position before reading
            attachmentStreams[i].Position = 0;

            // Read extracted bytes
            using (var ms = new MemoryStream())
            {
                attachmentStreams[i].CopyTo(ms);
                byte[] extractedBytes = ms.ToArray();

                // Optional: save extracted file to disk for manual inspection
                string fileName = attachNames[i] ?? $"attachment_{i}"; // safeguard against null
                string extractedPath = Path.Combine(extractedDir, fileName);
                File.WriteAllBytes(extractedPath, extractedBytes);

                // Compare byte arrays
                if (originalBytes.Length == extractedBytes.Length)
                {
                    bool identical = true;
                    for (int j = 0; j < originalBytes.Length; j++)
                    {
                        if (originalBytes[j] != extractedBytes[j])
                        {
                            identical = false;
                            break;
                        }
                    }

                    if (identical)
                    {
                        Console.WriteLine($"Attachment '{fileName}' matches the original file.");
                        matchFound = true;
                    }
                    else
                    {
                        Console.WriteLine($"Attachment '{fileName}' does NOT match the original file (content differs).");
                    }
                }
                else
                {
                    Console.WriteLine($"Attachment '{fileName}' does NOT match the original file (size differs).");
                }
            }
        }

        if (!matchFound)
        {
            Console.WriteLine("No matching attachment was found.");
        }

        // Cleanup facades
        extractor.Close();
    }
}
