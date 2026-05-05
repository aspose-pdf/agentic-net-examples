using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "booklet.pdf";
        const string logPath = "audit_log.txt";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the audit log file (overwrites if it already exists)
        using (StreamWriter log = new StreamWriter(logPath, false))
        {
            log.WriteLine($"[{DateTime.Now}] Batch booklet creation started.");

            try
            {
                // Create the PdfFileEditor facade
                PdfFileEditor editor = new PdfFileEditor();

                log.WriteLine($"[{DateTime.Now}] Calling MakeBooklet('{inputPdf}', '{outputPdf}').");

                // Perform the booklet operation
                bool success = editor.MakeBooklet(inputPdf, outputPdf);

                log.WriteLine($"[{DateTime.Now}] MakeBooklet returned: {success}.");

                // Record any conversion log produced by the editor
                string conversionLog = editor.ConversionLog;
                if (!string.IsNullOrEmpty(conversionLog))
                {
                    log.WriteLine($"[{DateTime.Now}] ConversionLog:");
                    log.WriteLine(conversionLog);
                }

                // Verify that the output file was created
                if (success && File.Exists(outputPdf))
                {
                    log.WriteLine($"[{DateTime.Now}] Booklet created successfully at '{outputPdf}'.");
                }
                else
                {
                    log.WriteLine($"[{DateTime.Now}] Booklet creation failed.");
                }
            }
            catch (Exception ex)
            {
                // Log any unexpected errors
                log.WriteLine($"[{DateTime.Now}] Exception: {ex.Message}");
                log.WriteLine(ex.StackTrace);
            }
            finally
            {
                log.WriteLine($"[{DateTime.Now}] Batch process completed.");
            }
        }

        Console.WriteLine($"Audit log written to '{logPath}'.");
    }
}