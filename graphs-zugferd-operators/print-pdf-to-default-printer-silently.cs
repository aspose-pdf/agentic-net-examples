using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to ensure it is a valid document (optional verification)
        using (Document doc = new Document(inputPath))
        {
            // No modifications required for printing
        }

        // Send the PDF to the default printer silently using the OS "print" verb
        var startInfo = new ProcessStartInfo
        {
            FileName = inputPath,
            Verb = "print",
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden,
            UseShellExecute = true
        };

        try
        {
            // Process.Start can return null, so handle the nullable result explicitly
            Process? proc = Process.Start(startInfo);
            if (proc == null)
            {
                Console.Error.WriteLine("Failed to start the print process.");
                return;
            }

            using (proc)
            {
                // Optionally wait for the process to exit or timeout after a reasonable period
                proc.WaitForExit(10000);
            }

            Console.WriteLine("Print job sent to the default printer.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Printing failed: {ex.Message}");
        }
    }
}
