using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Simple internal logger – replaces Aspose.Pdf.Logging which may not be available in all versions
    private static class SimpleLogger
    {
        private static string _logFilePath;
        private static bool _enabled;

        public static void Enable(string logFilePath)
        {
            _logFilePath = logFilePath;
            _enabled = true;
            // Ensure a fresh log for each run
            try
            {
                File.WriteAllText(_logFilePath, $"Log started at {DateTime.Now}\n");
            }
            catch { /* ignore failures – logging will be best‑effort */ }
        }

        public static void Log(string message)
        {
            if (!_enabled) return;
            try
            {
                File.AppendAllText(_logFilePath, $"[{DateTime.Now:O}] {message}\n");
            }
            catch { /* ignore logging errors */ }
        }

        public static void Disable()
        {
            _enabled = false;
        }
    }

    static void Main()
    {
        // Paths
        const string inputPdf = "input.pdf";
        const string logPath = "pdf_operations.log";
        const string searchTerm = "Aspose";

        // Enable simple logging (replaces Aspose.Pdf.Logging which may be unavailable)
        SimpleLogger.Enable(logPath);
        SimpleLogger.Log("Logging enabled.");

        // Ensure the input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            SimpleLogger.Log($"Input file not found: {inputPdf}");
            SimpleLogger.Disable();
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            SimpleLogger.Log($"Loaded PDF: {inputPdf}");

            // Perform a text search using TextFragmentAbsorber
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchTerm);
            doc.Pages.Accept(absorber);

            int matchCount = absorber.TextFragments.Count;
            Console.WriteLine($"Found {matchCount} occurrence(s) of \"{searchTerm}\".");
            SimpleLogger.Log($"Search completed. Term: '{searchTerm}'. Matches: {matchCount}.");

            // Highlight found fragments – demonstrates that the document can be modified
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.TextState.BackgroundColor = Aspose.Pdf.Color.Yellow;
            }

            // Save the modified PDF (logging will capture the save operation as well)
            const string outputPdf = "output.pdf";
            doc.Save(outputPdf);
            Console.WriteLine($"Modified PDF saved as \"{outputPdf}\".");
            SimpleLogger.Log($"Saved modified PDF as '{outputPdf}'.");
        }

        // Verify that the log file contains entries related to the search operation
        if (File.Exists(logPath))
        {
            string logContent = File.ReadAllText(logPath);
            bool containsSearch = logContent.IndexOf("Search completed", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                  logContent.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0;

            Console.WriteLine(containsSearch
                ? "Search action was recorded in the log file."
                : "Search action was NOT found in the log file.");
        }
        else
        {
            Console.Error.WriteLine("Log file was not created.");
        }

        // Disable logging after the operation (good practice)
        SimpleLogger.Disable();
    }
}
