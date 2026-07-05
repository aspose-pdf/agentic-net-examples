using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath      = "input.pdf";          // Original PDF
        const string attachmentPath    = "attachment_file.pdf"; // File to attach
        const string outputPdfPath     = "output_with_attachment.pdf";

        // Verify source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Add the attachment to the PDF using PdfContentEditor
        // ------------------------------------------------------------
        Aspose.Pdf.Facades.PdfContentEditor editor = new Aspose.Pdf.Facades.PdfContentEditor();
        editor.BindPdf(inputPdfPath);
        // AddDocumentAttachment adds a file attachment without a visible annotation
        editor.AddDocumentAttachment(attachmentPath, "Sample attachment");
        editor.Save(outputPdfPath);
        editor.Close(); // Facade must be closed explicitly

        // ------------------------------------------------------------
        // 2. Extract the attachment from the edited PDF using PdfExtractor
        // ------------------------------------------------------------
        Aspose.Pdf.Facades.PdfExtractor extractor = new Aspose.Pdf.Facades.PdfExtractor();
        extractor.BindPdf(outputPdfPath);
        // Must call ExtractAttachment before querying names or streams
        extractor.ExtractAttachment();

        IList<string> attachmentNames = extractor.GetAttachNames();
        MemoryStream[] attachmentStreams = extractor.GetAttachment();

        // Find the extracted stream that matches our attachment name
        MemoryStream matchingStream = null;
        string matchingName = null;
        for (int i = 0; i < attachmentNames.Count; i++)
        {
            string name = attachmentNames[i];
            if (string.Equals(name, Path.GetFileName(attachmentPath), StringComparison.OrdinalIgnoreCase))
            {
                matchingName = name;
                matchingStream = attachmentStreams[i];
                break;
            }
        }

        if (matchingStream == null)
        {
            Console.Error.WriteLine("Attachment not found in the extracted PDF.");
            extractor.Close();
            return;
        }

        // ------------------------------------------------------------
        // 3. Compare the extracted attachment with the original file
        // ------------------------------------------------------------
        byte[] originalBytes = File.ReadAllBytes(attachmentPath);

        // Ensure the stream is positioned at the beginning
        matchingStream.Position = 0;
        byte[] extractedBytes = new byte[matchingStream.Length];
        int read = matchingStream.Read(extractedBytes, 0, extractedBytes.Length);
        if (read != extractedBytes.Length)
        {
            Console.Error.WriteLine("Failed to read the full attachment stream.");
            extractor.Close();
            return;
        }

        bool isIdentical = originalBytes.Length == extractedBytes.Length;
        if (isIdentical)
        {
            for (int i = 0; i < originalBytes.Length; i++)
            {
                if (originalBytes[i] != extractedBytes[i])
                {
                    isIdentical = false;
                    break;
                }
            }
        }

        Console.WriteLine(isIdentical
            ? $"Success: Extracted attachment \"{matchingName}\" matches the original file."
            : $"Failure: Extracted attachment \"{matchingName}\" differs from the original file.");

        // Clean up
        extractor.Close();
    }
}