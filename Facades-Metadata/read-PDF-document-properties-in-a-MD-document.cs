using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputMd = "document_properties.md";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfFileInfo facade to read PDF metadata
        using (PdfFileInfo info = new PdfFileInfo(inputPdf))
        {
            // Build Markdown content
            var md = new System.Text.StringBuilder();
            md.AppendLine("# PDF Document Properties");
            md.AppendLine();
            md.AppendLine($"**Title:** {info.Title}");
            md.AppendLine($"**Author:** {info.Author}");
            md.AppendLine($"**Subject:** {info.Subject}");
            md.AppendLine($"**Keywords:** {info.Keywords}");
            md.AppendLine($"**Creator:** {info.Creator}");
            md.AppendLine($"**Producer:** {info.Producer}");
            md.AppendLine($"**Creation Date:** {info.CreationDate}");
            md.AppendLine($"**Modification Date:** {info.ModDate}");
            md.AppendLine($"**Number of Pages:** {info.NumberOfPages}");
            md.AppendLine($"**Is Encrypted:** {info.IsEncrypted}");
            md.AppendLine($"**Has Open Password:** {info.HasOpenPassword}");
            md.AppendLine($"**Has Edit Password:** {info.HasEditPassword}");
            md.AppendLine($"**PDF Version:** {info.GetPdfVersion()}");
            md.AppendLine($"**Is PDF File:** {info.IsPdfFile}");
            md.AppendLine($"**Has Collection (Portfolio):** {info.HasCollection}");
            md.AppendLine();

            // Write Markdown to file
            File.WriteAllText(outputMd, md.ToString());
            Console.WriteLine($"PDF properties written to '{outputMd}'.");
        }
    }
}