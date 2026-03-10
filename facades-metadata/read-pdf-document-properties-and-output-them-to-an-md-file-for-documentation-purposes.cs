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
        const string inputPdf = "input.pdf";
        // Output Markdown file path
        const string outputMd = "document_properties.md";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfFileInfo facade to read PDF metadata
        using (PdfFileInfo info = new PdfFileInfo(inputPdf))
        {
            // Build markdown content
            var md = new StringBuilder();
            md.AppendLine("# PDF Document Properties");
            md.AppendLine();
            md.AppendLine($"**File:** `{Path.GetFileName(inputPdf)}`");
            md.AppendLine();
            md.AppendLine("| Property | Value |");
            md.AppendLine("|---|---|");
            md.AppendLine($"| Title | {Escape(info.Title)} |");
            md.AppendLine($"| Author | {Escape(info.Author)} |");
            md.AppendLine($"| Subject | {Escape(info.Subject)} |");
            md.AppendLine($"| Keywords | {Escape(info.Keywords)} |");
            md.AppendLine($"| Creator | {Escape(info.Creator)} |");
            md.AppendLine($"| Producer | {Escape(info.Producer)} |");
            md.AppendLine($"| Creation Date | {Escape(info.CreationDate ?? string.Empty)} |");
            md.AppendLine($"| Modification Date | {Escape(info.ModDate ?? string.Empty)} |");
            md.AppendLine($"| Number of Pages | {info.NumberOfPages} |");
            md.AppendLine($"| PDF Version | {Escape(info.GetPdfVersion())} |");
            md.AppendLine($"| Is Encrypted | {info.IsEncrypted} |");
            md.AppendLine($"| Is PDF File | {info.IsPdfFile} |");
            md.AppendLine($"| Has Open Password | {info.HasOpenPassword} |");
            md.AppendLine($"| Has Edit Password | {info.HasEditPassword} |");

            // Convert Header dictionary to a readable string
            string headerString = info.Header != null
                ? string.Join("; ", info.Header.Select(kv => $"{kv.Key}={kv.Value}"))
                : string.Empty;
            md.AppendLine($"| Header (custom) | {Escape(headerString)} |");

            // Write markdown to file
            File.WriteAllText(outputMd, md.ToString());
            Console.WriteLine($"Properties written to '{outputMd}'.");
        }
    }

    // Escape pipe characters to keep markdown table well‑formed
    static string Escape(string value)
    {
        return string.IsNullOrEmpty(value) ? "" : value.Replace("|", "\\|");
    }
}
