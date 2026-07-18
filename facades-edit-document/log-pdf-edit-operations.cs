using System;
using System.IO;
using System.Drawing; // needed for Rectangle used by CreateWebLink
using Aspose.Pdf.Facades;

class PdfEditorWithLog
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string logPath = "edit_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize logger
        using (Logger logger = new Logger(logPath))
        {
            logger.Log($"Opening PDF: {inputPath}");

            // Edit PDF using PdfContentEditor
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the source PDF
                editor.BindPdf(inputPath);
                logger.Log($"Bound PDF file: {inputPath}");

                // Example operation: replace text
                editor.ReplaceText("OldText", "NewText");
                logger.Log("Replaced text 'OldText' with 'NewText'");

                // Example operation: create a web link annotation
                // System.Drawing.Rectangle is required by CreateWebLink (x, y, width, height)
                System.Drawing.Rectangle linkRect = new System.Drawing.Rectangle(100, 500, 200, 50);
                editor.CreateWebLink(linkRect, "https://www.example.com", 0);
                logger.Log($"Created web link to https://www.example.com at rectangle {linkRect}");

                // Save the edited PDF
                editor.Save(outputPath);
                logger.Log($"Saved edited PDF as: {outputPath}");
            }

            Console.WriteLine($"Editing completed. Log written to {logPath}");
        }
    }
}

// Simple logger that appends timestamped entries to a text file
class Logger : IDisposable
{
    private readonly StreamWriter _writer;

    public Logger(string logFilePath)
    {
        // Append to existing log or create a new one
        _writer = new StreamWriter(logFilePath, append: true);
        _writer.AutoFlush = true;
    }

    public void Log(string message)
    {
        string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
        _writer.WriteLine(entry);
    }

    public void Dispose()
    {
        _writer?.Dispose();
    }
}
