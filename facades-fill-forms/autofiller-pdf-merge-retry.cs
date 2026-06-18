using System;
using System.Data;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "merged_output.pdf";

        // Number of retry attempts for transient I/O errors
        const int maxRetries = 3;
        // Delay between retries (milliseconds)
        const int retryDelayMs = 2000;

        // Prepare the data to be merged into the PDF
        DataTable dataTable = CreateSampleDataTable();

        int attempt = 0;
        while (true)
        {
            try
            {
                // AutoFiller implements IDisposable; ensure proper cleanup
                using (AutoFiller autoFiller = new AutoFiller())
                {
                    // Bind the template PDF file
                    autoFiller.BindPdf(templatePath);

                    // Import the data table into the template fields
                    autoFiller.ImportDataTable(dataTable);

                    // Save the merged result to a file
                    autoFiller.Save(outputPath);
                }

                Console.WriteLine($"AutoFiller completed successfully. Output saved to '{outputPath}'.");
                break; // Success – exit retry loop
            }
            catch (IOException ex) when (IsTransient(ex) && attempt < maxRetries)
            {
                // Transient file access exception – retry after a short delay
                attempt++;
                Console.Error.WriteLine($"Transient I/O error (attempt {attempt}/{maxRetries}): {ex.Message}");
                Thread.Sleep(retryDelayMs);
                // Loop will retry
            }
            catch (Exception ex)
            {
                // Non‑transient or exhausted retries – report and abort
                Console.Error.WriteLine($"Operation failed: {ex.Message}");
                break;
            }
        }
    }

    // Simple heuristic to decide whether an IOException is transient.
    // Extend this method with more sophisticated checks if needed.
    static bool IsTransient(IOException ex) => true;

    // Creates a minimal DataTable matching the expected PDF form fields.
    static DataTable CreateSampleDataTable()
    {
        DataTable table = new DataTable("MailMerge");
        table.Columns.Add("Field1", typeof(string));
        table.Columns.Add("Field2", typeof(string));

        DataRow row = table.NewRow();
        row["Field1"] = "SampleValue1";
        row["Field2"] = "SampleValue2";
        table.Rows.Add(row);

        return table;
    }
}
