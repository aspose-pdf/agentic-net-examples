using System;
using System.IO;
using System.Diagnostics;
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

        try
        {
            // Load the PDF to ensure it is a valid document (optional, can be omitted if not needed)
            using (Document doc = new Document(inputPath))
            {
                // Document is successfully loaded – now print silently using the OS "print" verb.
                var psi = new ProcessStartInfo
                {
                    FileName = inputPath,
                    Verb = "print",
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = true
                };

                using (Process? proc = Process.Start(psi))
                {
                    // Give the print job a short time to be handed over to the printer driver.
                    // The process will typically exit quickly after the print command is issued.
                    proc?.WaitForExit(5000);
                }
            }

            Console.WriteLine("Print job sent to the default printer.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
