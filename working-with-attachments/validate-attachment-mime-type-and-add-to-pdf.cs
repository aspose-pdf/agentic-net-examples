using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string attachmentPath = "sample.txt";

        // Verify that source PDF and attachment exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Determine MIME type based on file extension
        string mimeFromExtension = GetMimeTypeFromExtension(Path.GetExtension(attachmentPath));

        // Simple validation – reject unknown or mismatched types
        if (mimeFromExtension == "application/octet-stream")
        {
            Console.Error.WriteLine("Unsupported attachment file type.");
            return;
        }

        // Add the attachment to the PDF using PdfContentEditor
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdfPath);
            // The description can be any text you want to associate with the attachment
            editor.AddDocumentAttachment(attachmentPath, "Sample attachment");
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
    }

    // Basic mapping from file extension to MIME type
    static string GetMimeTypeFromExtension(string extension)
    {
        if (string.IsNullOrWhiteSpace(extension))
            return "application/octet-stream";

        switch (extension.ToLowerInvariant())
        {
            case ".txt":  return "text/plain";
            case ".pdf":  return "application/pdf";
            case ".doc":  return "application/msword";
            case ".docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            case ".xls":  return "application/vnd.ms-excel";
            case ".xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            case ".png":  return "image/png";
            case ".jpg":
            case ".jpeg": return "image/jpeg";
            case ".gif":  return "image/gif";
            case ".zip":  return "application/zip";
            default:      return "application/octet-stream";
        }
    }
}