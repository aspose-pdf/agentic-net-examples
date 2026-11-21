using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF (with updated metadata) and audit CSV file paths
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string auditCsvPath  = "metadata_audit.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1 – Capture original metadata using PdfFileInfo (Facades API)
        // -----------------------------------------------------------------
        PdfFileInfo originalInfo = new PdfFileInfo();
        originalInfo.BindPdf(inputPdfPath);

        string originalTitle   = originalInfo.Title   ?? string.Empty;
        string originalAuthor  = originalInfo.Author  ?? string.Empty;
        string originalSubject = originalInfo.Subject ?? string.Empty;
        string originalKeywords= originalInfo.Keywords?? string.Empty;

        // ---------------------------------------------------------------
        // Step 2 – Load the document, modify metadata, and save the result
        // ---------------------------------------------------------------
        using (Document doc = new Document(inputPdfPath))
        {
            // Update metadata (example values – replace with real data as needed)
            doc.Info.Title   = "New Document Title";
            doc.Info.Author  = "New Author Name";
            doc.Info.Subject = "Updated Subject";
            doc.Info.Keywords= "new,keywords,example";

            // Save the modified PDF (standard PDF save – no SaveOptions needed)
            doc.Save(outputPdfPath);
        }

        // ---------------------------------------------------------------
        // Step 3 – Capture new metadata after the update
        // ---------------------------------------------------------------
        PdfFileInfo newInfo = new PdfFileInfo();
        newInfo.BindPdf(outputPdfPath);

        string newTitle   = newInfo.Title   ?? string.Empty;
        string newAuthor  = newInfo.Author  ?? string.Empty;
        string newSubject = newInfo.Subject ?? string.Empty;
        string newKeywords= newInfo.Keywords?? string.Empty;

        // ---------------------------------------------------------------
        // Step 4 – Write audit information to CSV
        // ---------------------------------------------------------------
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Property,OriginalValue,NewValue");
        sb.AppendLine($"Title,\"{EscapeCsv(originalTitle)}\",\"{EscapeCsv(newTitle)}\"");
        sb.AppendLine($"Author,\"{EscapeCsv(originalAuthor)}\",\"{EscapeCsv(newAuthor)}\"");
        sb.AppendLine($"Subject,\"{EscapeCsv(originalSubject)}\",\"{EscapeCsv(newSubject)}\"");
        sb.AppendLine($"Keywords,\"{EscapeCsv(originalKeywords)}\",\"{EscapeCsv(newKeywords)}\"");

        try
        {
            File.WriteAllText(auditCsvPath, sb.ToString(), Encoding.UTF8);
            Console.WriteLine($"Metadata audit saved to '{auditCsvPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write CSV: {ex.Message}");
        }
    }

    // Helper to escape double quotes in CSV fields
    static string EscapeCsv(string field)
    {
        if (field.Contains("\""))
            field = field.Replace("\"", "\"\"");
        return field;
    }
}