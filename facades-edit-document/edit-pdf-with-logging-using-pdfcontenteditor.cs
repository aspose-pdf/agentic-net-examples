using System;
using System.IO;
using System.Drawing; // Needed for System.Drawing.Rectangle used by PdfContentEditor.CreateWebLink
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Path to the log file where edit operations will be recorded
    private const string LogFilePath = "edit_operations.log";

    // Helper method to write a log entry with timestamp and PDF file name
    private static void LogOperation(string pdfFileName, string operationDescription)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        string logEntry = $"{timestamp} | File: {pdfFileName} | Operation: {operationDescription}{Environment.NewLine}";
        File.AppendAllText(LogFilePath, logEntry);
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

        // Ensure previous log is cleared for a fresh run
        if (File.Exists(LogFilePath))
            File.Delete(LogFilePath);

        // Create the PdfContentEditor facade
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the source PDF file for editing
        editor.BindPdf(inputPdf);
        LogOperation(inputPdf, "Bound PDF for editing");

        // Example edit operation 1: Replace a specific text string
        string oldText = "Hello";
        string newText = "Hi";
        editor.ReplaceText(oldText, newText);
        LogOperation(inputPdf, $"Replaced text \"{oldText}\" with \"{newText}\"");

        // Example edit operation 2: Create a web link annotation on page 1
        // Fully qualify Aspose.Pdf.Rectangle to avoid ambiguity with System.Drawing.Rectangle
        Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
        // PdfContentEditor.CreateWebLink expects a System.Drawing.Rectangle, so we convert
        System.Drawing.Rectangle sysRect = new System.Drawing.Rectangle(
            (int)linkRect.LLX,
            (int)linkRect.LLY,
            (int)(linkRect.URX - linkRect.LLX),
            (int)(linkRect.URY - linkRect.LLY));
        string url = "https://www.example.com";
        editor.CreateWebLink(sysRect, url, 1);
        LogOperation(inputPdf, $"Created web link to \"{url}\" on page 1");

        // Example edit operation 3: Delete all images from page 2
        // DeleteImage requires an image index array; passing an empty array deletes all images on the page.
        editor.DeleteImage(2, new int[] { });
        LogOperation(inputPdf, "Deleted all images from page 2");

        // Save the edited PDF to the output path
        editor.Save(outputPdf);
        LogOperation(outputPdf, "Saved edited PDF");

        // Close the editor (optional, as it does not implement IDisposable)
        editor.Close();

        Console.WriteLine($"Editing completed. Log written to {LogFilePath}");
    }
}
