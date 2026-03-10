using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF containing attachments
        const string pdfPath = "input.pdf";
        // Name of the attachment to extract (case‑insensitive match)
        const string attachmentName = "myfile.txt";
        // Destination file for the extracted attachment
        string outputPath = "extracted_" + attachmentName;

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Initialize the PdfExtractor facade
        PdfExtractor extractor = new PdfExtractor();

        // Bind the PDF file to the extractor
        extractor.BindPdf(pdfPath);

        // Extract attachment information from the PDF
        extractor.ExtractAttachment();

        // Retrieve the list of attachment names
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
        using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            MemoryStream attachmentStream = attachmentStreams[targetIndex];
            attachmentStream.Position = 0; // Ensure stream is at the beginning
            attachmentStream.CopyTo(fileStream);
        }

        Console.WriteLine($"Attachment '{attachmentName}' extracted to '{outputPath}'.");
    }
}