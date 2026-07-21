using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        // Verify the input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(pdfPath))
        {
            // No need to convert the PDF to another format for printing.
            // The OS can print a PDF directly using the "print" verb.
            var psi = new ProcessStartInfo(pdfPath)
            {
                Verb = "print",
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process? printProcess = Process.Start(psi);
            // Optionally wait for the print job to be sent.
            printProcess?.WaitForExit(5000);
        }

        Console.WriteLine("Print job dispatched successfully.");
    }
}
