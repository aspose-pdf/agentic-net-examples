using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF file to be printed.
        const string pdfPath = "sample.pdf";

        // Verify that the file exists before attempting to print.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF using the core Aspose.Pdf Document class.
        // This ensures the file is a valid PDF and allows any
        // pre‑processing you might need before printing.
        using (Document doc = new Document(pdfPath))
        {
            // No core Aspose.Pdf API provides direct printing.
            // Use the operating system's default PDF viewer to
            // print the document via the "print" verb.
            var startInfo = new ProcessStartInfo
            {
                FileName = pdfPath,
                Verb = "print",               // Triggers the default print action.
                CreateNoWindow = true,        // Do not create a console window.
                UseShellExecute = true        // Required for the Verb to work.
            };

            try
            {
                Process printProcess = Process.Start(startInfo);
                // Optionally wait for the print job to be dispatched.
                // printProcess?.WaitForExit();
                Console.WriteLine("Print command sent successfully.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to print PDF: {ex.Message}");
            }
        }
    }
}