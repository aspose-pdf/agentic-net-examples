using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileMend does NOT implement IDisposable, so we use try‑finally.
        PdfFileMend mend = new PdfFileMend();

        try
        {
            // Load the source PDF.
            mend.BindPdf(inputPath);

            // Perform any desired modifications here.
            // Example: (no modification) just save the bound document.

            // Save the (potentially) modified PDF.
            mend.Save(outputPath);
        }
        finally
        {
            // Ensure that all resources are released and changes are flushed.
            mend.Close();
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}