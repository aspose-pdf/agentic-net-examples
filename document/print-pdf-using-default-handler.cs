using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf; // Core API for optional validation

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        // Verify that the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Optional: load the document to ensure it is a valid PDF
        try
        {
            using (Document doc = new Document(pdfPath))
            {
                // Document loaded successfully; no further action needed here
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load PDF: {ex.Message}");
            return;
        }

        // Print the PDF using the operating system's default PDF handler
        var startInfo = new ProcessStartInfo
        {
            FileName = pdfPath,
            Verb = "print",                 // Use the "print" verb associated with the file type
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden,
            UseShellExecute = true          // Required for the Verb to work
        };

        try
        {
            using (Process printProcess = Process.Start(startInfo))
            {
                // Give the print command some time to be processed
                printProcess.WaitForExit(10000); // wait up to 10 seconds
            }

            Console.WriteLine("Print command sent successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Printing failed: {ex.Message}");
        }
    }
}