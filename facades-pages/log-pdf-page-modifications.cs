using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string logPath    = "modifications.log";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the log file is empty before starting
        File.WriteAllText(logPath, string.Empty);

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Enable internal notification logging (optional, for paragraph events)
            doc.EnableNotificationLogging = true;

            // Create a PdfPageEditor bound to the document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Edit all pages
                editor.ProcessPages = null; // null means all pages

                // Example modification: rotate each page 90 degrees clockwise
                editor.Rotation = 90;
                editor.ApplyChanges();

                // Log the rotation change
                LogModification(logPath, $"Applied rotation of 90 degrees to all pages using PdfPageEditor.");

                // Example modification: set background color for each page
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    page.Background = Aspose.Pdf.Color.LightGray;
                    LogModification(logPath, $"Set background color to LightGray on page {i}.");
                }
            }

            // Save the modified document
            doc.Save(outputPath);
            LogModification(logPath, $"Document saved to '{outputPath}'.");
        }

        Console.WriteLine($"Processing complete. Modifications logged to '{logPath}'.");
    }

    // Helper method to append a line to the log file with timestamp
    static void LogModification(string logFilePath, string message)
    {
        string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
        File.AppendAllLines(logFilePath, new[] { entry });
    }
}