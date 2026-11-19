using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logPath = "pdf_operations.log";
        const string searchTerm = "Aspose";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Perform a text search using TextFragmentAbsorber
        int occurrences;
        using (Document doc = new Document(inputPath))
        {
            // TextFragmentAbsorber gives direct access to the matched fragments collection
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchTerm);
            doc.Pages.Accept(absorber);
            occurrences = absorber.TextFragments.Count;
        }

        // Simple custom logging (Aspose.Pdf does not expose a Logging class in this version)
        using (StreamWriter logWriter = new StreamWriter(logPath, false))
        {
            logWriter.WriteLine($"Search term: \"{searchTerm}\"");
            logWriter.WriteLine($"Found {occurrences} occurrence(s) of \"{searchTerm}\".");
            logWriter.WriteLine($"Timestamp: {DateTime.UtcNow:O}");
        }

        Console.WriteLine($"Found {occurrences} occurrence(s) of \"{searchTerm}\".");

        // Verify that the log file was created and display its content
        if (File.Exists(logPath))
        {
            Console.WriteLine("Log file content:");
            Console.WriteLine(File.ReadAllText(logPath));
        }
        else
        {
            Console.WriteLine("Log file was not created.");
        }
    }
}
