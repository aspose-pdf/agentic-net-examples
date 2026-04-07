using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "sample.pdf";
        const string reportPath   = "metadata_report.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Retrieve standard metadata via DocumentInfo
            DocumentInfo info = doc.Info;

            // Count embedded file attachments (may be null if none)
            int attachmentCount = doc.EmbeddedFiles?.Count ?? 0;

            // Build a simple text report
            string report = $"Title: {info.Title}{Environment.NewLine}" +
                            $"Author: {info.Author}{Environment.NewLine}" +
                            $"Subject: {info.Subject}{Environment.NewLine}" +
                            $"Keywords: {info.Keywords}{Environment.NewLine}" +
                            $"Creator: {info.Creator}{Environment.NewLine}" +
                            $"Producer: {info.Producer}{Environment.NewLine}" +
                            $"Creation Date: {info.CreationDate}{Environment.NewLine}" +
                            $"Modification Date: {info.ModDate}{Environment.NewLine}" +
                            $"Attachment Count: {attachmentCount}{Environment.NewLine}";

            // Output to console
            Console.WriteLine(report);

            // Persist the report to a text file
            File.WriteAllText(reportPath, report);
            Console.WriteLine($"Report saved to '{reportPath}'.");
        }
    }
}