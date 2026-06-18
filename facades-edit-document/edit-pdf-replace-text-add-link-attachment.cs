using System;
using System.IO;
using System.Drawing; // Needed for Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Simple logger that appends a line with timestamp and PDF file name.
    static void LogOperation(string pdfPath, string operation, string logFile)
    {
        string entry = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} | File: {Path.GetFileName(pdfPath)} | Operation: {operation}";
        File.AppendAllText(logFile, entry + Environment.NewLine);
    }

    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output.pdf";
        const string logFile    = "edit_log.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure previous log is cleared for this run (optional)
        if (File.Exists(logFile))
            File.Delete(logFile);

        // Initialize the content editor and bind the source PDF.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);
        LogOperation(inputPdf, "BindPdf", logFile);

        // 1. Replace all occurrences of a word.
        editor.ReplaceText("OldWord", "NewWord");
        LogOperation(inputPdf, "ReplaceText('OldWord' -> 'NewWord')", logFile);

        // 2. Add a web link annotation.
        // Use System.Drawing.Rectangle as required by CreateWebLink overload.
        System.Drawing.Rectangle linkRect = new System.Drawing.Rectangle(100, 500, 200, 50); // x, y, width, height
        // The third parameter is the page number (1‑based). Use 1 for the first page.
        editor.CreateWebLink(linkRect, "https://www.example.com", 1);
        LogOperation(inputPdf, "CreateWebLink to https://www.example.com", logFile);

        // 3. Attach an external file to the PDF (no visual annotation).
        const string attachmentPath = "attachment.txt";
        if (File.Exists(attachmentPath))
        {
            editor.AddDocumentAttachment(attachmentPath, "Sample attachment");
            LogOperation(inputPdf, $"AddDocumentAttachment('{attachmentPath}')", logFile);
        }
        else
        {
            Console.WriteLine($"Attachment not found: {attachmentPath} (skipped)");
        }

        // Save the edited PDF.
        editor.Save(outputPdf);
        LogOperation(outputPdf, "Save edited PDF", logFile);

        // Clean up the facade.
        editor.Close();

        Console.WriteLine($"Editing completed. Log written to '{logFile}'.");
    }
}
