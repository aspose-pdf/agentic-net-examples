using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfEditLoggingExample
{
    // Simple logger that appends operation details to a text file.
    internal static class Logger
    {
        private static readonly string LogFilePath = "edit_operations.log";

        // Logs the operation with timestamp and PDF file name.
        public static void Log(string pdfFilePath, string operationDescription)
        {
            string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\t{Path.GetFileName(pdfFilePath)}\t{operationDescription}";
            File.AppendAllText(LogFilePath, entry + Environment.NewLine);
        }
    }

    class Program
    {
        static void Main()
        {
            const string inputPdfPath = "input.pdf";
            const string outputPdfPath = "edited_output.pdf";

            // Ensure the input file exists.
            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
                return;
            }

            // Use PdfContentEditor (Facade) to perform edit operations.
            // The facade is created, bound to the source PDF, edited, and saved.
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Load the PDF for editing.
                editor.BindPdf(inputPdfPath);
                Logger.Log(inputPdfPath, "BindPdf");

                // Example edit operation 1: Replace text.
                editor.ReplaceText("OldText", "NewText");
                Logger.Log(inputPdfPath, "ReplaceText: 'OldText' -> 'NewText'");

                // Example edit operation 2: Add a document attachment (no visual annotation).
                const string attachmentPath = "sample_attachment.txt";
                if (File.Exists(attachmentPath))
                {
                    editor.AddDocumentAttachment(attachmentPath, "Sample attachment");
                    Logger.Log(inputPdfPath, $"AddDocumentAttachment: {Path.GetFileName(attachmentPath)}");
                }

                // Example edit operation 3: Create a web link annotation.
                // Use System.Drawing.Rectangle (fully qualified) because CreateWebLink expects that type.
                System.Drawing.Rectangle linkRect = new System.Drawing.Rectangle(100, 500, 200, 550);
                editor.CreateWebLink(linkRect, "https://www.example.com", 0);
                Logger.Log(inputPdfPath, "CreateWebLink to https://www.example.com");

                // Save the edited PDF to a new file.
                editor.Save(outputPdfPath);
                Logger.Log(outputPdfPath, "Save edited PDF");
            }

            Console.WriteLine("PDF editing completed. Operations logged to edit_operations.log.");
        }
    }
}
