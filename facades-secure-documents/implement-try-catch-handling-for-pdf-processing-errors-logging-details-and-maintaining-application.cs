using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string pdf1 = "input1.pdf";
        string pdf2 = "input2.pdf";
        string merged = "merged.pdf";
        string sanitized = "sanitized.pdf";

        // Concatenate PDFs with robust error handling
        ConcatenatePdfs(pdf1, pdf2, merged);

        // Sanitize a PDF with robust error handling
        SanitizePdf(pdf1, sanitized);
    }

    static void ConcatenatePdfs(string file1, string file2, string output)
    {
        if (!File.Exists(file1) || !File.Exists(file2))
        {
            Console.Error.WriteLine("One or more source files not found.");
            return;
        }

        PdfFileEditor editor = new PdfFileEditor();
        try
        {
            // TryConcatenate returns false on failure; detailed info is in LastException
            bool success = editor.TryConcatenate(file1, file2, output);
            if (!success)
            {
                Exception last = editor.LastException;
                Console.Error.WriteLine("Concatenation failed.");
                if (last != null)
                {
                    Console.Error.WriteLine($"Message: {last.Message}");
                    if (last.InnerException != null)
                        Console.Error.WriteLine($"Inner: {last.InnerException.Message}");
                }
                return;
            }

            Console.WriteLine($"Successfully concatenated to '{output}'.");
        }
        catch (PdfException pdfEx)
        {
            // Handles Aspose.Pdf specific errors
            Console.Error.WriteLine($"PdfException: {pdfEx.Message}");
            if (pdfEx.InnerException != null)
                Console.Error.WriteLine($"Inner: {pdfEx.InnerException.Message}");
        }
        catch (Exception ex)
        {
            // Handles any other unexpected errors
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    static void SanitizePdf(string input, string output)
    {
        if (!File.Exists(input))
        {
            Console.Error.WriteLine("Source file not found.");
            return;
        }

        // PdfFileSanitization does not implement IDisposable; no using block needed
        PdfFileSanitization sanitizer = new PdfFileSanitization();
        try
        {
            // Bind the source PDF
            sanitizer.BindPdf(input);

            // Optional: configure sanitization options
            sanitizer.UseRebuildXrefAndTrailer = true;
            sanitizer.UseTrimBottom = true;
            sanitizer.UseTrimTop = true;

            // Perform recovery/sanitization
            sanitizer.Recover();

            // Save the sanitized PDF
            sanitizer.Save(output);
            Console.WriteLine($"Sanitized PDF saved to '{output}'.");
        }
        catch (PdfException pdfEx)
        {
            Console.Error.WriteLine($"PdfException during sanitization: {pdfEx.Message}");
            if (pdfEx.InnerException != null)
                Console.Error.WriteLine($"Inner: {pdfEx.InnerException.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error during sanitization: {ex.Message}");
        }
    }
}