using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF file to be inspected
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfFileInfo provides file‑level information such as encryption status
        PdfFileInfo fileInfo = new PdfFileInfo();

        // Bind the PDF file to the PdfFileInfo instance
        fileInfo.BindPdf(pdfPath);

        // Determine whether the PDF is encrypted
        bool isEncrypted = fileInfo.IsEncrypted;

        // Build a plain‑text summary
        string summary = $"PDF File: {Path.GetFileName(pdfPath)}{Environment.NewLine}" +
                         $"Encrypted: {isEncrypted}{Environment.NewLine}";

        // Output the summary to the console
        Console.WriteLine(summary);

        // Optionally, write the summary to a text file
        const string outputTxt = "pdf_encryption_summary.txt";
        try
        {
            File.WriteAllText(outputTxt, summary);
            Console.WriteLine($"Summary written to '{outputTxt}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write summary file: {ex.Message}");
        }
    }
}