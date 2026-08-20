using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "sample.pdf";      // input PDF path
        const string reportTxt = "metadata_report.txt"; // output report path

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Access standard metadata via DocumentInfo
            DocumentInfo info = doc.Info;

            // Get the number of embedded file attachments (may be zero)
            int attachmentCount = doc.EmbeddedFiles?.Count ?? 0;

            // Build a simple text report
            string report = $"Title: {info.Title}\n" +
                            $"Author: {info.Author}\n" +
                            $"Subject: {info.Subject}\n" +
                            $"Keywords: {info.Keywords}\n" +
                            $"Creator: {info.Creator}\n" +
                            $"Producer: {info.Producer}\n" +
                            $"Creation Date: {info.CreationDate}\n" +
                            $"Modification Date: {info.ModDate}\n" +
                            $"Attachment Count: {attachmentCount}\n";

            // Output to console
            Console.WriteLine(report);

            // Write the report to a text file
            File.WriteAllText(reportTxt, report);

            // Demonstrate a save operation (no modifications made)
            doc.Save("output_copy.pdf");
        }

        Console.WriteLine($"Metadata report saved to '{reportTxt}'.");
    }
}