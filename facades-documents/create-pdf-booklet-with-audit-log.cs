using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_booklet.pdf";
        const string logPath = "booklet_creation_log.txt";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open a StreamWriter for the audit log (overwrites any existing file)
        using (StreamWriter log = new StreamWriter(logPath, false))
        {
            try
            {
                log.WriteLine($"{DateTime.Now}: Starting booklet creation process.");

                // Create the PdfFileEditor facade (no IDisposable, so no using block)
                PdfFileEditor editor = new PdfFileEditor();

                // Perform the booklet operation using the simple string overload
                bool success = editor.MakeBooklet(inputPdf, outputPdf);

                // Log the result of MakeBooklet
                if (success)
                {
                    log.WriteLine($"{DateTime.Now}: MakeBooklet succeeded.");
                }
                else
                {
                    log.WriteLine($"{DateTime.Now}: MakeBooklet reported failure.");
                }

                // Retrieve and log the internal conversion log from the editor
                string conversionLog = editor.ConversionLog;
                if (!string.IsNullOrEmpty(conversionLog))
                {
                    log.WriteLine($"{DateTime.Now}: ConversionLog:");
                    log.WriteLine(conversionLog);
                }

                log.WriteLine($"{DateTime.Now}: Booklet creation process completed.");
            }
            catch (Exception ex)
            {
                // Log any unexpected exception
                log.WriteLine($"{DateTime.Now}: Exception occurred - {ex.Message}");
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }

        Console.WriteLine($"Booklet created: {outputPdf}");
        Console.WriteLine($"Audit log written to: {logPath}");
    }
}