using System;
using System.IO;
using System.Drawing; // Required for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// Stub for missing type that caused CS0246. Adjust the namespace if the real SDK is referenced.
namespace Aspose.Storage.Cloud.Sdk.Model
{
    // Minimal placeholder – the real SDK provides properties for object upload.
    public class PutObjectRequest { }
}

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "edited_output.pdf";
        const string logPath   = "edit_log.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open a log file for appending. Using a StreamWriter inside a using block guarantees the file is closed.
        using (StreamWriter logWriter = new StreamWriter(logPath, append: true))
        {
            // Initialize the content editor and bind the source PDF
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(inputPdf);
                LogOperation(logWriter, inputPdf, "Bound PDF for editing");

                // Example 1: Replace a text string
                string oldText = "Hello";
                string newText = "Hi";
                editor.ReplaceText(oldText, newText);
                LogOperation(logWriter, inputPdf, $"Replaced text \"{oldText}\" with \"{newText}\"");

                // Example 2: Add a web link annotation
                // Use System.Drawing.Rectangle as required by CreateWebLink overload
                System.Drawing.Rectangle linkRect = new System.Drawing.Rectangle(100, 500, 200, 20);
                string url = "https://www.example.com";
                // The third parameter is the page number (1‑based). Use 1 for the first page.
                editor.CreateWebLink(linkRect, url, 1);
                LogOperation(logWriter, inputPdf, $"Created web link to \"{url}\" on page 1");

                // Example 3: Add a document attachment (no visual annotation)
                string attachmentPath = "attachment.txt";
                if (File.Exists(attachmentPath))
                {
                    editor.AddDocumentAttachment(attachmentPath, "Sample attachment");
                    LogOperation(logWriter, inputPdf, $"Added document attachment \"{attachmentPath}\"");
                }
                else
                {
                    LogOperation(logWriter, inputPdf, $"Attachment file not found: {attachmentPath}");
                }

                // Save the edited PDF
                editor.Save(outputPdf);
                LogOperation(logWriter, outputPdf, "Saved edited PDF");
            }
        }

        Console.WriteLine($"Editing completed. Log written to \"{logPath}\".");
    }

    // Helper method to write a timestamped log entry. It records the operation, the PDF file name and the exact time.
    static void LogOperation(StreamWriter writer, string pdfFilePath, string message)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string fileName  = Path.GetFileName(pdfFilePath);
        writer.WriteLine($"{timestamp} - {fileName} - {message}");
        writer.Flush();
    }
}
