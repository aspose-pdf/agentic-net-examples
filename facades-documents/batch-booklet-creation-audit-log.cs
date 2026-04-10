using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "booklet_output.pdf";
        const string auditLog  = "booklet_audit.txt";

        // Prepare a StreamWriter for the audit log
        using (StreamWriter logger = new StreamWriter(auditLog, false))
        {
            logger.WriteLine($"[{DateTime.Now}] Batch booklet creation started.");
            logger.WriteLine($"Input PDF : {inputPdf}");
            logger.WriteLine($"Output PDF: {outputPdf}");

            if (!File.Exists(inputPdf))
            {
                logger.WriteLine($"[{DateTime.Now}] ERROR: Input file not found.");
                Console.Error.WriteLine("Input file not found.");
                return;
            }

            try
            {
                // Create the PdfFileEditor facade
                PdfFileEditor editor = new PdfFileEditor();

                logger.WriteLine($"[{DateTime.Now}] PdfFileEditor instantiated.");

                // Perform the booklet operation
                bool success = editor.MakeBooklet(inputPdf, outputPdf);

                logger.WriteLine($"[{DateTime.Now}] MakeBooklet returned: {success}");

                // Retrieve the conversion log from the editor
                string conversionLog = editor.ConversionLog ?? string.Empty;

                logger.WriteLine($"[{DateTime.Now}] Conversion Log:");
                logger.WriteLine(conversionLog);

                if (success && File.Exists(outputPdf))
                {
                    logger.WriteLine($"[{DateTime.Now}] Booklet PDF created successfully.");
                    Console.WriteLine("Booklet creation completed.");
                }
                else
                {
                    logger.WriteLine($"[{DateTime.Now}] ERROR: Booklet creation failed.");
                    Console.Error.WriteLine("Booklet creation failed.");
                }
            }
            catch (Exception ex)
            {
                logger.WriteLine($"[{DateTime.Now}] EXCEPTION: {ex.Message}");
                logger.WriteLine(ex.StackTrace);
                Console.Error.WriteLine($"Exception: {ex.Message}");
            }
            finally
            {
                logger.WriteLine($"[{DateTime.Now}] Batch process finished.");
            }
        }
    }
}