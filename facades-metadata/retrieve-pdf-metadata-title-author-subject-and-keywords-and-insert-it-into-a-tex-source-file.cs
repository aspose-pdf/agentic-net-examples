using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output TeX source file path
        const string texPath = "metadata.tex";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Retrieve metadata using PdfFileInfo facade
        string title, author, subject, keywords;

        // PdfFileInfo implements IDisposable, so use a using block for deterministic disposal
        using (PdfFileInfo info = new PdfFileInfo(pdfPath))
        {
            title    = info.Title    ?? string.Empty;
            author   = info.Author   ?? string.Empty;
            subject  = info.Subject  ?? string.Empty;
            keywords = info.Keywords ?? string.Empty;
        }

        // Create a simple TeX file that defines the metadata as macros
        try
        {
            using (StreamWriter writer = new StreamWriter(texPath, false))
            {
                writer.WriteLine("% Auto-generated metadata macros");
                writer.WriteLine($"\\newcommand{{\\pdftitle}}{{{EscapeForTeX(title)}}}");
                writer.WriteLine($"\\newcommand{{\\pdfauthor}}{{{EscapeForTeX(author)}}}");
                writer.WriteLine($"\\newcommand{{\\pdfsubject}}{{{EscapeForTeX(subject)}}}");
                writer.WriteLine($"\\newcommand{{\\pdfkeywords}}{{{EscapeForTeX(keywords)}}}");
            }

            Console.WriteLine($"Metadata written to '{texPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error writing TeX file: {ex.Message}");
        }
    }

    // Simple TeX escaping for special characters
    static string EscapeForTeX(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        return input
            .Replace("\\", "\\textbackslash{}")
            .Replace("{", "\\{")
            .Replace("}", "\\}")
            .Replace("$", "\\$")
            .Replace("&", "\\&")
            .Replace("#", "\\#")
            .Replace("_", "\\_")
            .Replace("%", "\\%")
            .Replace("^", "\\^{}")
            .Replace("~", "\\~{}");
    }
}