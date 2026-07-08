using System;
using System.Data;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";      // PDF template with form fields
        const string outputPath   = "merged_output.pdf"; // Resulting merged PDF
        const int maxRetries      = 3;                  // Number of retry attempts
        const int delayMilliseconds = 1000;             // Delay between retries

        // Prepare a simple DataTable matching the form field names in the template
        DataTable dataTable = new DataTable("AutoFillData");
        dataTable.Columns.Add("FieldName", typeof(string));
        dataTable.Columns.Add("FieldValue", typeof(string));
        // Example row – replace with actual field names and values
        dataTable.Rows.Add("Name", "John Doe");
        dataTable.Rows.Add("Date", DateTime.Today.ToShortDateString());

        // Retry loop for the ImportDataTable operation
        int attempt = 0;
        bool success = false;

        while (attempt < maxRetries && !success)
        {
            attempt++;
            try
            {
                // Create and configure AutoFiller
                using (AutoFiller autoFiller = new AutoFiller())
                {
                    // Bind the PDF template (can be a file path, stream, or Document)
                    autoFiller.BindPdf(templatePath);

                    // Import the DataTable into the form fields
                    autoFiller.ImportDataTable(dataTable);

                    // Save the merged result to a single output file
                    autoFiller.Save(outputPath);
                }

                // If we reach this point, the operation succeeded
                success = true;
                Console.WriteLine($"AutoFiller completed successfully on attempt {attempt}.");
            }
            catch (IOException ex) when (IsTransient(ex))
            {
                Console.Error.WriteLine($"Transient I/O error on attempt {attempt}: {ex.Message}");
                if (attempt < maxRetries)
                {
                    Thread.Sleep(delayMilliseconds);
                }
            }
            catch (UnauthorizedAccessException ex) when (IsTransient(ex))
            {
                Console.Error.WriteLine($"Transient access error on attempt {attempt}: {ex.Message}");
                if (attempt < maxRetries)
                {
                    Thread.Sleep(delayMilliseconds);
                }
            }
            catch (Exception ex)
            {
                // Non‑transient or unexpected exception – abort retries
                Console.Error.WriteLine($"Fatal error: {ex.Message}");
                break;
            }
        }

        if (!success)
        {
            Console.Error.WriteLine("AutoFiller failed after maximum retry attempts.");
        }
    }

    // Determines whether an exception is considered transient for retry purposes
    private static bool IsTransient(Exception ex)
    {
        // Simple heuristic: treat I/O errors with HResult indicating sharing violations or
        // temporary unavailability as transient. Extend as needed.
        if (ex is IOException ioEx)
        {
            // ERROR_SHARING_VIOLATION (32) or ERROR_LOCK_VIOLATION (33)
            const int sharingViolation = 0x20;
            const int lockViolation   = 0x21;
            int hresult = ioEx.HResult & 0xFFFF;
            return hresult == sharingViolation || hresult == lockViolation;
        }

        if (ex is UnauthorizedAccessException)
        {
            // Access denied may be transient if the file is being released by another process
            return true;
        }

        return false;
    }
}