using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string logPath    = "search_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // Enable logging by validating the document and writing the log file.
            // The Validate method creates a log that can be inspected later.
            // -----------------------------------------------------------------
            bool validationResult = doc.Validate(logPath, PdfFormat.PDF_A_1B);
            Console.WriteLine($"Document validation result: {validationResult}");

            // ---------------------------------------------------------------
            // Perform a text search using TextFragmentAbsorber.
            // Enable logging of text extraction errors (optional, demonstrates
            // the LogTextExtractionErrors property).
            // ---------------------------------------------------------------
            TextFragmentAbsorber absorber = new TextFragmentAbsorber("hello");
            absorber.TextSearchOptions = new TextSearchOptions(true) // use regex mode
            {
                LogTextExtractionErrors = true
            };

            // Search the entire document
            absorber.Visit(doc);

            // Output search results
            Console.WriteLine($"Found {absorber.TextFragments.Count} occurrence(s) of \"hello\".");
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                Console.WriteLine($" - Page {fragment.Page.Number}: \"{fragment.Text}\"");
            }

            // Save the (potentially modified) document
            doc.Save(outputPath);
        }

        // -----------------------------------------------------------------
        // Verify that the log file was created and contains content.
        // -----------------------------------------------------------------
        if (File.Exists(logPath))
        {
            string logContent = File.ReadAllText(logPath);
            Console.WriteLine("\n--- Log File Content ---");
            Console.WriteLine(string.IsNullOrWhiteSpace(logContent)
                ? "(Log file is empty)"
                : logContent);
        }
        else
        {
            Console.Error.WriteLine($"Log file was not created: {logPath}");
        }
    }
}