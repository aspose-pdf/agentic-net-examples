using System;
using System.Data;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;

class Program
{
    // Determines whether an IOException is considered transient.
    // For simplicity, all IOExceptions are treated as transient in this example.
    static bool IsTransient(IOException ex)
    {
        return true;
    }

    static void Main()
    {
        const string templatePdfPath = "template.pdf";   // Path to the PDF template
        const string outputPdfPath   = "filled_output.pdf"; // Desired output file
        const int maxRetryAttempts   = 3;               // Maximum number of retries
        const int retryDelayMs       = 1000;            // Delay between retries (ms)

        // Prepare a sample DataTable with field names matching the PDF form fields.
        DataTable dataTable = new DataTable("FormData");
        dataTable.Columns.Add("FirstName", typeof(string));
        dataTable.Columns.Add("LastName",  typeof(string));
        dataTable.Columns.Add("Address",   typeof(string));

        // Add a single row of data (replace with real data as needed).
        DataRow row = dataTable.NewRow();
        row["FirstName"] = "John";
        row["LastName"]  = "Doe";
        row["Address"]   = "123 Main St.";
        dataTable.Rows.Add(row);

        int attempt = 0;
        while (true)
        {
            attempt++;
            try
            {
                // Create and configure the AutoFiller instance.
                using (AutoFiller autoFiller = new AutoFiller())
                {
                    // Bind the template PDF file.
                    autoFiller.BindPdf(templatePdfPath);

                    // Import the data from the DataTable into the form fields.
                    autoFiller.ImportDataTable(dataTable);

                    // Save the filled PDF to the specified output path.
                    autoFiller.Save(outputPdfPath);
                }

                Console.WriteLine($"AutoFiller completed successfully on attempt {attempt}.");
                break; // Success – exit the retry loop.
            }
            catch (IOException ioEx) when (IsTransient(ioEx) && attempt < maxRetryAttempts)
            {
                // Transient file access error – wait and retry.
                Console.Error.WriteLine($"Transient I/O error on attempt {attempt}: {ioEx.Message}");
                Thread.Sleep(retryDelayMs);
            }
            catch (Exception ex)
            {
                // Non-transient or max retries exceeded – report and abort.
                Console.Error.WriteLine($"AutoFiller failed on attempt {attempt}: {ex.Message}");
                break;
            }
        }
    }
}