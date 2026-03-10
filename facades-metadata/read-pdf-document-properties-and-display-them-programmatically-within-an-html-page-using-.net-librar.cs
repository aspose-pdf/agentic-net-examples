using System;
using System.IO;
using System.Linq;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Output HTML file path
        const string htmlPath = "output.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Use PdfFileInfo to read document properties
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            // Build HTML content
            var sb = new StringBuilder();

            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"en\">");
            sb.AppendLine("<head>");
            sb.AppendLine("    <meta charset=\"UTF-8\">");
            sb.AppendLine("    <title>PDF Properties</title>");
            sb.AppendLine("    <style>");
            sb.AppendLine("        table { border-collapse: collapse; width: 80%; margin: 20px auto; }");
            sb.AppendLine("        th, td { border: 1px solid #ccc; padding: 8px; text-align: left; }");
            sb.AppendLine("        th { background-color: #f2f2f2; }");
            sb.AppendLine("    </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("    <h2 style=\"text-align:center;\">PDF Document Properties</h2>");
            sb.AppendLine("    <table>");
            sb.AppendLine("        <tr><th>Property</th><th>Value</th></tr>");

            // Helper to add a row
            void AddRow(string name, string value)
            {
                sb.AppendLine($"        <tr><td>{System.Net.WebUtility.HtmlEncode(name)}</td><td>{System.Net.WebUtility.HtmlEncode(value)}</td></tr>");
            }

            AddRow("Title", pdfInfo.Title ?? string.Empty);
            AddRow("Author", pdfInfo.Author ?? string.Empty);
            AddRow("Subject", pdfInfo.Subject ?? string.Empty);
            AddRow("Keywords", pdfInfo.Keywords ?? string.Empty);
            AddRow("Creator", pdfInfo.Creator ?? string.Empty);
            AddRow("Producer", pdfInfo.Producer ?? string.Empty);
            AddRow("Creation Date", pdfInfo.CreationDate ?? string.Empty);
            AddRow("Modification Date", pdfInfo.ModDate ?? string.Empty);
            AddRow("Number of Pages", pdfInfo.NumberOfPages.ToString());
            AddRow("Is Encrypted", pdfInfo.IsEncrypted.ToString());
            AddRow("Is PDF File", pdfInfo.IsPdfFile.ToString());

            // Header dictionary (custom metadata)
            if (pdfInfo.Header != null && pdfInfo.Header.Any())
            {
                string headerString = string.Join("; ", pdfInfo.Header.Select(kv => $"{kv.Key}={kv.Value}"));
                AddRow("Header", headerString);
            }
            else
            {
                AddRow("Header", string.Empty);
            }

            sb.AppendLine("    </table>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            // Write HTML to file
            File.WriteAllText(htmlPath, sb.ToString(), Encoding.UTF8);
            Console.WriteLine($"HTML file generated at: {htmlPath}");
        }
    }
}