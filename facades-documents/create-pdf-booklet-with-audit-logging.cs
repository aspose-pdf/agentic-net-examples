using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "booklet.pdf";
        const string logPath = "booklet_audit.log";

        // Verify that the input PDF exists
        if (!File.Exists(inputPath))
        {
            File.WriteAllText(logPath, $"[{DateTime.Now}] ERROR: Input file '{inputPath}' not found.");
            return;
        }

        // Open the audit log for appending
        using (StreamWriter log = new StreamWriter(logPath, append: true))
        {
            log.WriteLine($"[{DateTime.Now}] Starting booklet creation process.");
            log.WriteLine($"[{DateTime.Now}] Input file: {inputPath}");
            log.WriteLine($"[{DateTime.Now}] Output file: {outputPath}");

            // Create the PdfFileEditor facade
            PdfFileEditor editor = new PdfFileEditor();
            log.WriteLine($"[{DateTime.Now}] PdfFileEditor instance created.");

            bool success = false;
            try
            {
                // Perform the booklet operation using default page size
                success = editor.MakeBooklet(inputPath, outputPath);
                log.WriteLine($"[{DateTime.Now}] MakeBooklet invoked. Success = {success}");
            }
            catch (Exception ex)
            {
                log.WriteLine($"[{DateTime.Now}] Exception during MakeBooklet: {ex.Message}");
            }

            // Record conversion log if available
            if (success)
            {
                string conversionLog = editor.ConversionLog;
                if (!string.IsNullOrEmpty(conversionLog))
                {
                    log.WriteLine($"[{DateTime.Now}] ConversionLog:");
                    log.WriteLine(conversionLog);
                }
                else
                {
                    log.WriteLine($"[{DateTime.Now}] No conversion log returned.");
                }
            }
            else
            {
                log.WriteLine($"[{DateTime.Now}] Booklet creation failed.");
            }

            log.WriteLine($"[{DateTime.Now}] Booklet creation process completed.");
        }
    }
}