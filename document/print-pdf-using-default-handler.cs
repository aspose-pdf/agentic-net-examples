using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the PDF file exists before attempting to print
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document to ensure it is a valid PDF (optional)
        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Document loaded successfully – now send it to the default printer
                PrintPdf(inputPath);
            }
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"Failed to load PDF: {ex.Message}");
            return;
        }

        Console.WriteLine("Print job sent successfully.");
    }

    private static void PrintPdf(string pdfPath)
    {
        // Use the OS shell "print" verb to send the PDF to the default printer.
        // This approach does not require Aspose.Pdf.Facades and works with any
        // PDF viewer that registers the "print" verb (e.g., Adobe Reader, Edge).
        var psi = new ProcessStartInfo
        {
            FileName = pdfPath,
            Verb = "print",
            CreateNoWindow = true,
            UseShellExecute = true
        };

        using (var process = Process.Start(psi))
        {
            // Give the viewer some time to finish the print job.
            // The exact timing depends on the installed PDF viewer.
            if (process != null)
            {
                process.WaitForExit(10000); // wait up to 10 seconds
            }
        }
    }
}
