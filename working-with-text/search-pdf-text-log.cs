using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string logPath = "search_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Variables to hold search results for logging after the document is disposed
        int foundCount = 0;
        bool hasErrors = false;
        System.Collections.Generic.IList<TextExtractionError> extractionErrors = null;

        // Load the PDF document (using rule: load)
        using (Document doc = new Document(inputPath))
        {
            // Configure text search options to log extraction errors
            TextSearchOptions searchOptions = new TextSearchOptions(true) // enable regex mode (illustrative)
            {
                LogTextExtractionErrors = true
            };

            // Create a TextFragmentAbsorber with the desired phrase and the search options
            TextFragmentAbsorber absorber = new TextFragmentAbsorber("sample")
            {
                TextSearchOptions = searchOptions
            };

            // Perform the search on the whole document (using rule: create & load)
            absorber.Visit(doc);

            // Capture search results for later logging
            foundCount = absorber.TextFragments.Count;
            hasErrors = absorber.HasErrors;
            extractionErrors = absorber.Errors;

            // Example: modify found fragments (optional)
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = fragment.Text.ToUpperInvariant();
            }

            // Save the modified document (using rule: save)
            doc.Save(outputPath);
        }

        // Build the log content using the captured information
        string logContent = $"Search performed on '{inputPath}' at {DateTime.Now:u}{Environment.NewLine}" +
                            $"Search phrase: \"sample\"{Environment.NewLine}" +
                            $"Total fragments found: {foundCount}{Environment.NewLine}";

        if (hasErrors && extractionErrors != null)
        {
            logContent += "Extraction errors detected:" + Environment.NewLine;
            foreach (var err in extractionErrors)
            {
                // Use ToString() because TextExtractionError does not expose PageNumber or Message properties
                logContent += $"- {err.ToString()}{Environment.NewLine}";
            }
        }

        // Write the log to the specified file
        try
        {
            File.WriteAllText(logPath, logContent);
            Console.WriteLine($"Search log written to '{logPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write log file: {ex.Message}");
        }

        Console.WriteLine($"Processing completed. Output PDF saved to '{outputPath}'.");
    }
}
