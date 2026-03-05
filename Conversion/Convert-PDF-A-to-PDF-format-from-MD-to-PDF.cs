using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string markdownPath = "input.md";
        const string pdfOutputPath = "output.pdf";

        if (!File.Exists(markdownPath))
        {
            Console.Error.WriteLine($"Markdown file not found: {markdownPath}");
            return;
        }

        // Load the Markdown file using MdLoadOptions
        MdLoadOptions mdOptions = new MdLoadOptions();

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(markdownPath, mdOptions))
        {
            // If the generated PDF happens to be PDF/A, remove the compliance flag
            doc.RemovePdfaCompliance();

            // Save as a regular PDF (no SaveOptions needed for PDF output)
            doc.Save(pdfOutputPath);
        }

        Console.WriteLine($"Markdown successfully converted to PDF: {pdfOutputPath}");
    }
}