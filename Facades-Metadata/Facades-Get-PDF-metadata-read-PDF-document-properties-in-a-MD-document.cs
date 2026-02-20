using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output Markdown path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.pdf> <output.md>");
            return;
        }

        string pdfPath = args[0];
        string mdPath = args[1];

        // Verify that the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load PDF metadata using the Facade class
            using (PdfFileInfo info = new PdfFileInfo(pdfPath))
            {
                // Build Markdown content
                StringBuilder md = new StringBuilder();
                md.AppendLine("# PDF Metadata");
                md.AppendLine();
                md.AppendLine($"**File:** `{Path.GetFileName(pdfPath)}`");
                md.AppendLine();
                md.AppendLine($"- **Title:** {Escape(info.Title)}");
                md.AppendLine($"- **Author:** {Escape(info.Author)}");
                md.AppendLine($"- **Subject:** {Escape(info.Subject)}");
                md.AppendLine($"- **Keywords:** {Escape(info.Keywords)}");
                md.AppendLine($"- **Creator:** {Escape(info.Creator)}");
                md.AppendLine($"- **Producer:** {Escape(info.Producer)}");
                md.AppendLine($"- **Creation Date:** {info.CreationDate}");
                md.AppendLine($"- **Modification Date:** {info.ModDate}");
                md.AppendLine($"- **Number of Pages:** {info.NumberOfPages}");
                md.AppendLine($"- **Is Encrypted:** {info.IsEncrypted}");
                md.AppendLine($"- **Has Open Password:** {info.HasOpenPassword}");
                md.AppendLine($"- **Has Edit Password:** {info.HasEditPassword}");

                // Write the Markdown file
                File.WriteAllText(mdPath, md.ToString());
                Console.WriteLine($"Metadata written to {mdPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }

    // Simple Markdown escaping for common characters
    static string Escape(string value)
    {
        if (string.IsNullOrEmpty(value))
            return "";
        return value.Replace("|", "\\|")
                    .Replace("\r", " ")
                    .Replace("\n", " ");
    }
}