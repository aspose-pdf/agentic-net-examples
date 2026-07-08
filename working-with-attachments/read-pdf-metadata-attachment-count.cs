using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath = "sample.pdf";          // PDF to analyze
        const string reportPath   = "metadata_report.txt"; // Output report file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ----- Retrieve standard metadata (DocumentInfo) -----
            DocumentInfo info = pdfDoc.Info;

            string title   = info.Title   ?? "(none)";
            string author  = info.Author  ?? "(none)";
            string subject = info.Subject ?? "(none)";
            string creator = info.Creator ?? "(none)";
            string producer = info.Producer ?? "(none)";
            string keywords = info.Keywords ?? "(none)";

            // CreationDate and ModDate are non‑nullable DateTime values.
            // Treat default(DateTime) as "no value".
            string creationDate = info.CreationDate != default ? info.CreationDate.ToString("u") : "(none)";
            string modDate      = info.ModDate      != default ? info.ModDate.ToString("u")      : "(none)";

            // ----- Count embedded file attachments -----
            // Aspose.Pdf stores attachments in the EmbeddedFiles collection.
            // If the collection is null (no attachments), treat count as zero.
            int attachmentCount = pdfDoc.EmbeddedFiles?.Count ?? 0;

            // ----- Build the report text -----
            string report = $"PDF Metadata Report for '{Path.GetFileName(inputPdfPath)}'{Environment.NewLine}" +
                            $"------------------------------------------------------------{Environment.NewLine}" +
                            $"Title   : {title}{Environment.NewLine}" +
                            $"Author  : {author}{Environment.NewLine}" +
                            $"Subject : {subject}{Environment.NewLine}" +
                            $"Creator : {creator}{Environment.NewLine}" +
                            $"Producer: {producer}{Environment.NewLine}" +
                            $"Keywords: {keywords}{Environment.NewLine}" +
                            $"Created : {creationDate}{Environment.NewLine}" +
                            $"Modified: {modDate}{Environment.NewLine}" +
                            $"Attachments count: {attachmentCount}{Environment.NewLine}";

            // ----- Output to console -----
            Console.WriteLine(report);

            // ----- Save report to a text file -----
            try
            {
                File.WriteAllText(reportPath, report);
                Console.WriteLine($"Report saved to '{reportPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to write report file: {ex.Message}");
            }
        }
    }
}
