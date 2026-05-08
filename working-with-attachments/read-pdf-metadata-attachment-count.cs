using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string reportPath = "report.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use a using block for deterministic disposal (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Access standard metadata via DocumentInfo (doc.Info)
            DocumentInfo info = doc.Info;
            string title   = info.Title   ?? "N/A";
            string author  = info.Author  ?? "N/A";
            string subject = info.Subject ?? "N/A";
            string keywords = info.Keywords ?? "N/A";
            string creator = info.Creator ?? "N/A";
            string producer = info.Producer ?? "N/A";
            // CreationDate and ModDate are non‑nullable DateTime values in Aspose.Pdf, so we format them directly.
            string creationDate = info.CreationDate != DateTime.MinValue ? info.CreationDate.ToString("u") : "N/A";
            string modDate = info.ModDate != DateTime.MinValue ? info.ModDate.ToString("u") : "N/A";

            // Get attachment (embedded file) count. The Attachments property does not exist; use EmbeddedFiles.
            int attachmentCount = (doc.EmbeddedFiles != null) ? doc.EmbeddedFiles.Count : 0;

            // Build a simple text report
            string report = $"PDF Metadata Report{Environment.NewLine}" +
                            $"File: {Path.GetFileName(inputPath)}{Environment.NewLine}" +
                            $"Title: {title}{Environment.NewLine}" +
                            $"Author: {author}{Environment.NewLine}" +
                            $"Subject: {subject}{Environment.NewLine}" +
                            $"Keywords: {keywords}{Environment.NewLine}" +
                            $"Creator: {creator}{Environment.NewLine}" +
                            $"Producer: {producer}{Environment.NewLine}" +
                            $"Creation Date: {creationDate}{Environment.NewLine}" +
                            $"Modification Date: {modDate}{Environment.NewLine}" +
                            $"Attachment Count: {attachmentCount}{Environment.NewLine}";

            // Output to console
            Console.WriteLine(report);

            // Save the report to a text file
            File.WriteAllText(reportPath, report);
        }

        Console.WriteLine($"Report saved to '{reportPath}'.");
    }
}
