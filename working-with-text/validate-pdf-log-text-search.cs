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
        const string logPath = "validation_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Enable document logging by validating the document.
            // The Validate method creates a log file with validation comments.
            // PdfFormat can be any supported format; using PDF/A-1B here.
            doc.Validate(logPath, PdfFormat.PDF_A_1B);

            // Set up a text search for the phrase "hello"
            TextFragmentAbsorber absorber = new TextFragmentAbsorber("hello");

            // Enable logging of text extraction errors (optional, may help diagnostics)
            absorber.TextSearchOptions = new TextSearchOptions(true)
            {
                LogTextExtractionErrors = true
            };

            // Perform the search on the whole document
            absorber.Visit(doc);

            // Example action: replace found text with "hi"
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = "hi";
            }

            // Save the modified document (lifecycle rule: use Save method)
            doc.Save(outputPath);
        }

        // Verify that the log file was created and contains entries
        if (File.Exists(logPath))
        {
            string[] logLines = File.ReadAllLines(logPath);
            Console.WriteLine($"Log file created: {logPath}");
            Console.WriteLine($"Log contains {logLines.Length} line(s).");
        }
        else
        {
            Console.Error.WriteLine("Log file was not created.");
        }
    }
}