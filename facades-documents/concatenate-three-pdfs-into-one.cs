using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file paths – adjust as needed
        const string pdf1 = "file1.pdf";
        const string pdf2 = "file2.pdf";
        const string pdf3 = "file3.pdf";

        // Final output PDF
        const string outputPdf = "merged.pdf";

        // Validate input files exist
        if (!File.Exists(pdf1) || !File.Exists(pdf2) || !File.Exists(pdf3))
        {
            Console.Error.WriteLine("One or more input PDF files were not found.");
            return;
        }

        // Temporary file to hold intermediate concatenation result
        string tempFile = Path.GetTempFileName();

        try
        {
            // First concatenation: pdf1 + pdf2 -> tempFile
            PdfFileEditor editor = new PdfFileEditor();
            bool firstSuccess = editor.Concatenate(pdf1, pdf2, tempFile);
            if (!firstSuccess)
            {
                Console.Error.WriteLine("Failed to concatenate the first two PDFs.");
                return;
            }

            // Second concatenation: tempFile + pdf3 -> outputPdf
            bool secondSuccess = editor.Concatenate(tempFile, pdf3, outputPdf);
            if (!secondSuccess)
            {
                Console.Error.WriteLine("Failed to concatenate the third PDF.");
                return;
            }

            Console.WriteLine($"Successfully concatenated PDFs into '{outputPdf}'.");
        }
        finally
        {
            // Clean up the temporary file
            if (File.Exists(tempFile))
            {
                try { File.Delete(tempFile); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}