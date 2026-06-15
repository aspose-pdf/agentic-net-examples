using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "booklet_output.pdf";
        const string auditLogPath  = "booklet_audit.log";

        // Ensure the audit log file is created fresh
        using (StreamWriter log = new StreamWriter(auditLogPath, false))
        {
            try
            {
                log.WriteLine($"{DateTime.Now:u} - Batch booklet creation started.");
                log.WriteLine($"Input PDF: {inputPdfPath}");
                log.WriteLine($"Output PDF: {outputPdfPath}");

                // Verify input file exists
                if (!File.Exists(inputPdfPath))
                {
                    log.WriteLine($"{DateTime.Now:u} - ERROR: Input file not found.");
                    Console.Error.WriteLine("Input file not found.");
                    return;
                }

                // Create the PdfFileEditor facade
                PdfFileEditor editor = new PdfFileEditor();
                log.WriteLine($"{DateTime.Now:u} - PdfFileEditor instance created.");

                // Perform booklet creation (default page size)
                bool success = editor.MakeBooklet(inputPdfPath, outputPdfPath);
                log.WriteLine($"{DateTime.Now:u} - MakeBooklet returned: {success}");

                // Append conversion log from the editor, if any
                if (!string.IsNullOrEmpty(editor.ConversionLog))
                {
                    log.WriteLine($"{DateTime.Now:u} - Conversion Log:");
                    log.WriteLine(editor.ConversionLog);
                }

                if (success && File.Exists(outputPdfPath))
                {
                    log.WriteLine($"{DateTime.Now:u} - Booklet PDF successfully created.");
                    Console.WriteLine("Booklet creation completed successfully.");
                }
                else
                {
                    log.WriteLine($"{DateTime.Now:u} - ERROR: Booklet creation failed.");
                    Console.Error.WriteLine("Booklet creation failed.");
                }
            }
            catch (Exception ex)
            {
                log.WriteLine($"{DateTime.Now:u} - EXCEPTION: {ex.GetType().Name} - {ex.Message}");
                log.WriteLine(ex.StackTrace);
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
            }
            finally
            {
                log.WriteLine($"{DateTime.Now:u} - Batch process finished.");
            }
        }
    }
}