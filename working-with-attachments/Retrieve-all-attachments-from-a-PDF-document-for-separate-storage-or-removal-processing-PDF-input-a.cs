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
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_without_attachments.pdf";
        const string attachmentsFolder = "ExtractedAttachments";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the folder for extracted attachments exists
        Directory.CreateDirectory(attachmentsFolder);

        // -------------------------------------------------
        // 1. Extract all attachments from the PDF
        // -------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdfPath);

            // Prepare the extractor to work with attachments
            extractor.ExtractAttachment();

            // Get attachment names (may be null if there are no attachments)
            IList<string> attachmentNames = extractor.GetAttachNames();
            // Get attachment contents as streams (may be null if there are no attachments)
            MemoryStream[] attachmentStreams = extractor.GetAttachment();

            // Guard against PDFs without attachments
            if (attachmentNames != null && attachmentStreams != null && attachmentNames.Count > 0 && attachmentStreams.Length > 0)
            {
                // Ensure both collections have the same count to avoid IndexOutOfRangeException
                int count = Math.Min(attachmentNames.Count, attachmentStreams.Length);
                for (int i = 0; i < count; i++)
                {
                    string name = attachmentNames[i] ?? $"attachment_{i}"; // fallback name
                    string outputPath = Path.Combine(attachmentsFolder, name);

                    // Reset stream position before reading
                    if (attachmentStreams[i] != null)
                    {
                        attachmentStreams[i].Position = 0;
                        using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            attachmentStreams[i].CopyTo(fs);
                        }
                        Console.WriteLine($"Extracted attachment: {outputPath}");
                    }
                }
            }
            else
            {
                Console.WriteLine("No attachments found in the PDF.");
            }
        }

        // -------------------------------------------------
        // 2. Remove all attachments from the PDF and save
        // -------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the same source PDF
            editor.BindPdf(inputPdfPath);

            // Delete every attachment (no‑op if none exist)
            editor.DeleteAttachments();

            // Save the cleaned PDF
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF without attachments saved to: {outputPdfPath}");
    }
}
