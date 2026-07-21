using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;

class PdfSanitizer
{
    // Sanitizes a PDF document and logs the start/end times of the operation.
    // inputPath  : path to the source PDF file.
    // outputPath : path where the sanitized PDF will be saved.
    // logPath    : path to a file where the audit log will be written.
    public static void SanitizePdf(string inputPath, string outputPath, string logPath)
    {
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the directory for the output and log files exists.
        Directory.CreateDirectory(Path.GetDirectoryName(outputPath) ?? string.Empty);
        Directory.CreateDirectory(Path.GetDirectoryName(logPath) ?? string.Empty);

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Optional: enable signature field sanitization (enabled by default).
            doc.EnableSignatureSanitization = true;

            // Record the start time.
            DateTime startTime = DateTime.UtcNow;
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                // Perform a validation which also acts as a sanitization step.
                // The validation log is written to the specified logPath.
                // Using PDF/A-1B format as an example; adjust as needed.
                bool validationResult = doc.Validate(logPath, PdfFormat.PDF_A_1B);

                // You can also call other sanitization‑related methods here if required.
                // For example: doc.RemoveMetadata(); // removes metadata from the document.
                // doc.RemovePdfUaCompliance(); // removes PDF/UA compliance if needed.

                // Save the (potentially sanitized) document.
                doc.Save(outputPath);
            }
            catch (Exception ex) // Catch any exception that occurs during sanitization.
            {
                // Log sanitization‑specific errors.
                Console.Error.WriteLine($"Sanitization failed: {ex.Message}");
                File.AppendAllText(logPath, $"SanitizationException: {ex}{Environment.NewLine}");
                return;
            }
            finally
            {
                // Stop the timer and record the end time.
                sw.Stop();
                DateTime endTime = DateTime.UtcNow;

                // Append audit information to the log file.
                string auditEntry = $"Sanitization operation:{Environment.NewLine}" +
                                    $"InputFile: {inputPath}{Environment.NewLine}" +
                                    $"OutputFile: {outputPath}{Environment.NewLine}" +
                                    $"StartTimeUtc: {startTime:O}{Environment.NewLine}" +
                                    $"EndTimeUtc:   {endTime:O}{Environment.NewLine}" +
                                    $"Duration: {sw.Elapsed}{Environment.NewLine}" +
                                    $"---------------------------{Environment.NewLine}";
                File.AppendAllText(logPath, auditEntry);
            }
        }
    }

    // Example usage.
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "sanitized_output.pdf";
        const string auditLog  = "sanitization_audit.log";

        SanitizePdf(inputPdf, outputPdf, auditLog);
        Console.WriteLine("Sanitization completed. Check the audit log for details.");
    }
}
