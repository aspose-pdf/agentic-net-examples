using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logFile = "modifications.log";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize log file
        File.WriteAllText(logFile, string.Empty);

        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPdf);

            // Helper to write log entries
            void Log(string message)
            {
                File.AppendAllText(logFile, $"{DateTime.UtcNow:O} - {message}{Environment.NewLine}");
            }

            // Log and modify Zoom
            float originalZoom = editor.Zoom;
            editor.Zoom = 0.5f;
            Log($"Zoom changed from {originalZoom} to {editor.Zoom}");

            // Log and modify Rotation
            int originalRotation = editor.Rotation;
            editor.Rotation = 90;
            Log($"Rotation changed from {originalRotation} to {editor.Rotation}");

            // Log and modify PageSize
            PageSize originalSize = editor.PageSize;
            editor.PageSize = PageSize.A4; // Set to A4 size
            Log($"PageSize changed from {originalSize.Width}x{originalSize.Height} to {editor.PageSize.Width}x{editor.PageSize.Height}");

            // Save the modified PDF
            editor.Save(outputPdf);
            Log($"Saved modified PDF to '{outputPdf}'");
        }

        Console.WriteLine($"Modifications have been logged to '{logFile}'.");
    }
}