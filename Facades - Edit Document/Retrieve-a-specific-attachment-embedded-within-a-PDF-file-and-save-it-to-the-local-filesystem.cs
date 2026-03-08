using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF containing attachments
        const string pdfPath = "input.pdf";
        // Name of the attachment to extract (case‑insensitive)
        const string attachmentName = "myfile.txt";
        // Destination file for the extracted attachment
        const string outputPath = "extracted_myfile.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Use PdfExtractor (facade) to work with attachments
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(pdfPath);

            // Extract all attachments from the PDF into memory
            extractor.ExtractAttachment();

            // Retrieve the list of attachment names (generic IList<string>)
            IList<string> names = extractor.GetAttachNames();

            // Locate the index of the desired attachment
            int targetIndex = -1;
            for (int i = 0; i < names.Count; i++)
            {
                if (string.Equals(names[i], attachmentName, StringComparison.OrdinalIgnoreCase))
                {
                    targetIndex = i;
                    break;
                }
            }

            if (targetIndex == -1)
            {
                Console.Error.WriteLine($"Attachment '{attachmentName}' not found in the PDF.");
                return;
            }

            // Get all attachment streams (order matches GetAttachNames)
            MemoryStream[] attachmentStreams = extractor.GetAttachment();

            // Save the selected attachment stream to the file system
            using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                MemoryStream ms = attachmentStreams[targetIndex];
                ms.Position = 0;               // Ensure stream is at the beginning
                ms.CopyTo(fs);                 // Write the attachment bytes to file
            }

            Console.WriteLine($"Attachment '{attachmentName}' extracted to '{outputPath}'.");
        }
    }
}
