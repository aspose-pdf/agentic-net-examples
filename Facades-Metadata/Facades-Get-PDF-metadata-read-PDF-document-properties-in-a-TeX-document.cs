using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class PdfMetadataToTex
{
    // Escape LaTeX special characters to avoid compilation errors in the generated .tex file
    static string EscapeLatex(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        var sb = new StringBuilder(input);
        sb.Replace(@"\\", @"\\textbackslash{}");
        sb.Replace("{", "\\{");
        sb.Replace("}", "\\}");
        sb.Replace("_", "\\_");
        sb.Replace("&", "\\&");
        sb.Replace("%", "\\%");
        sb.Replace("$", "\\$");
        sb.Replace("#", "\\#");
        sb.Replace("^", "\\^{}");
        sb.Replace("~", "\\~{}");
        return sb.ToString();
    }

    // Helper that tries to parse a date string and returns it formatted, or null if parsing fails.
    static string TryFormatDate(string dateString, string format)
    {
        if (string.IsNullOrEmpty(dateString))
            return null;
        if (DateTime.TryParse(dateString, out var dt))
            return dt.ToString(format);
        return null;
    }

    static void Main(string[] args)
    {
        // Input PDF file path (change as needed)
        const string pdfPath = "input.pdf";
        // Output TeX file path
        const string texPath = "metadata.tex";

        // Verify that the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load PDF metadata using Aspose.Pdf.Facades.PdfFileInfo
            PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath);

            // Build LaTeX document content
            var texBuilder = new StringBuilder();
            texBuilder.AppendLine(@"\\documentclass{article}");
            texBuilder.AppendLine(@"\\usepackage[utf8]{inputenc}");
            texBuilder.AppendLine(@"\\begin{document}");

            // Title and author (if present)
            if (!string.IsNullOrEmpty(pdfInfo.Title))
                texBuilder.AppendLine($@"\\title{{{EscapeLatex(pdfInfo.Title)}}}");
            if (!string.IsNullOrEmpty(pdfInfo.Author))
                texBuilder.AppendLine($@"\\author{{{EscapeLatex(pdfInfo.Author)}}}");

            // Use creation date as the document date if available
            var formattedCreationDateForHeader = TryFormatDate(pdfInfo.CreationDate, "yyyy-MM-dd");
            if (!string.IsNullOrEmpty(formattedCreationDateForHeader))
                texBuilder.AppendLine($@"\\date{{{formattedCreationDateForHeader}}}");
            else
                texBuilder.AppendLine(@"\\date{}");

            texBuilder.AppendLine(@"\\maketitle");
            texBuilder.AppendLine(@"\\section*{PDF Metadata}");
            texBuilder.AppendLine(@"\\begin{itemize}");

            // Helper to add an item if the value is not null/empty
            void AddItem(string name, string value)
            {
                if (!string.IsNullOrEmpty(value))
                    texBuilder.AppendLine($@"\\item {name}: {EscapeLatex(value)}");
            }

            AddItem("Title", pdfInfo.Title);
            AddItem("Author", pdfInfo.Author);
            AddItem("Subject", pdfInfo.Subject);
            AddItem("Keywords", pdfInfo.Keywords);
            AddItem("Creator", pdfInfo.Creator);
            AddItem("Producer", pdfInfo.Producer);
            AddItem("Creation Date", TryFormatDate(pdfInfo.CreationDate, "yyyy-MM-dd HH:mm:ss"));
            AddItem("Modification Date", TryFormatDate(pdfInfo.ModDate, "yyyy-MM-dd HH:mm:ss"));
            AddItem("Number of Pages", pdfInfo.NumberOfPages.ToString());

            texBuilder.AppendLine(@"\\end{itemize}");
            texBuilder.AppendLine(@"\\end{document}");

            // Write the LaTeX content to the output file
            File.WriteAllText(texPath, texBuilder.ToString(), Encoding.UTF8);
            Console.WriteLine($"Metadata extracted and written to '{texPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
