using System;
using System.IO;
using System.Drawing; // Needed for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfEditorWithLogging
{
    // Path to the log file
    private const string LogFilePath = "edit_log.txt";

    // Writes a log entry with timestamp, PDF file name and operation description
    private static void LogOperation(string pdfFileName, string operationDescription)
    {
        string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | File: {pdfFileName} | Operation: {operationDescription}{Environment.NewLine}";
        File.AppendAllText(LogFilePath, entry);
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the log file starts fresh for this run
        File.WriteAllText(LogFilePath, $"Log started at {DateTime.Now}{Environment.NewLine}");

        // Use PdfContentEditor to perform edit operations
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPdf);
            LogOperation(inputPdf, "BindPdf");

            // Example operation 1: Replace a text string
            string oldText = "Hello";
            string newText = "Hi";
            editor.ReplaceText(oldText, newText);
            LogOperation(inputPdf, $"ReplaceText \"{oldText}\" with \"{newText}\"");

            // Example operation 2: Add a web link annotation
            // Define the rectangle where the link will appear (left, top, width, height)
            // System.Drawing.Rectangle expects (x, y, width, height)
            System.Drawing.Rectangle linkRect = new System.Drawing.Rectangle(100, 500, 200, 50); // 300-100 = 200, 550-500 = 50
            string url = "https://www.example.com";
            editor.CreateWebLink(linkRect, url, 0);
            LogOperation(inputPdf, $"CreateWebLink to \"{url}\" at {linkRect}");

            // Save the edited PDF to a new file
            editor.Save(outputPdf);
            LogOperation(outputPdf, "Save");
        }

        Console.WriteLine($"Editing completed. Log written to {LogFilePath}");
    }
}
