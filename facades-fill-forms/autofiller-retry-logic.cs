using System;
using System.Data;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;

class AutoFillerRetryExample
{
    // Configuration
    private const string TemplatePdfPath = "template.pdf";   // Path to the PDF template
    private const string OutputPdfPath   = "merged_output.pdf"; // Path for the generated PDF
    private const int    MaxRetries      = 3;               // Number of retry attempts
    private const int    DelayMilliseconds = 1000;          // Initial delay between retries

    static void Main()
    {
        // Prepare a sample DataTable (replace with real data source as needed)
        DataTable dataTable = CreateSampleDataTable();

        // Execute the AutoFiller operation with retry logic
        bool success = ExecuteWithRetry(() => ProcessAutoFiller(dataTable));

        if (success)
        {
            Console.WriteLine($"AutoFiller completed successfully. Output saved to '{OutputPdfPath}'.");
        }
        else
        {
            Console.Error.WriteLine("AutoFiller failed after multiple attempts.");
        }
    }

    // Wraps an action with retry handling for transient file‑access exceptions
    private static bool ExecuteWithRetry(Action operation)
    {
        int attempt = 0;
        int delay   = DelayMilliseconds;

        while (true)
        {
            try
            {
                operation();
                return true; // Success
            }
            catch (IOException ex) // Transient file‑access exception
            {
                attempt++;
                if (attempt > MaxRetries)
                {
                    Console.Error.WriteLine($"Operation failed after {MaxRetries} retries: {ex.Message}");
                    return false;
                }

                Console.WriteLine($"Transient error encountered (attempt {attempt}/{MaxRetries}): {ex.Message}");
                Console.WriteLine($"Waiting {delay} ms before retrying...");
                Thread.Sleep(delay);
                delay *= 2; // Exponential back‑off
            }
            catch (Exception ex) // Non‑transient – let the caller know we couldn't recover
            {
                Console.Error.WriteLine($"Non‑transient error: {ex.Message}");
                return false;
            }
        }
    }

    // Core AutoFiller processing: bind template, import data, and save result
    // Uses a using‑statement to guarantee disposal without null‑conditional warnings
    private static void ProcessAutoFiller(DataTable dataTable)
    {
        using (AutoFiller autoFiller = new AutoFiller())
        {
            // Bind the PDF template (can be a file path, stream, or Document)
            autoFiller.BindPdf(TemplatePdfPath);

            // Import the data table; column names must match field names in the PDF
            autoFiller.ImportDataTable(dataTable);

            // Save the merged PDF to the specified output file
            autoFiller.Save(OutputPdfPath);
        }
    }

    // Creates a simple DataTable with columns matching expected PDF field names
    private static DataTable CreateSampleDataTable()
    {
        DataTable table = new DataTable("MailMerge");

        // Example columns – adjust to match your PDF form fields
        table.Columns.Add("CompanyName", typeof(string));
        table.Columns.Add("ContactName", typeof(string));
        table.Columns.Add("Address",      typeof(string));
        table.Columns.Add("PostalCode",   typeof(string));
        table.Columns.Add("City",         typeof(string));
        table.Columns.Add("Country",      typeof(string));
        table.Columns.Add("Heading",      typeof(string));

        // Populate with a single sample row (replace with real data as needed)
        DataRow row = table.NewRow();
        row["CompanyName"] = "Acme Corp";
        row["ContactName"] = "John Doe";
        row["Address"]     = "123 Main St";
        row["PostalCode"]  = "12345";
        row["City"]        = "Metropolis";
        row["Country"]     = "USA";
        row["Heading"]     = "Dear John Doe,";
        table.Rows.Add(row);

        return table;
    }
}
