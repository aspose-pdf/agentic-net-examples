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
        const string outputPath = "output.pdf";
        const int maxRetries = 3;
        int attempt = 0;
        bool success = false;

        // Example data table – replace with real data as needed
        DataTable data = new DataTable("Data");
        data.Columns.Add("Field1", typeof(string));
        data.Columns.Add("Field2", typeof(string));
        data.Rows.Add("Value1", "Value2");

        while (attempt < maxRetries && !success)
        {
            attempt++;
            try
            {
                // AutoFiller implements IDisposable, so use using for deterministic cleanup
                using (AutoFiller autoFiller = new AutoFiller())
                {
                    // Bind the template PDF (may throw transient file‑access exceptions)
                    autoFiller.BindPdf(templatePath);

                    // Import the data table into the template
                    autoFiller.ImportDataTable(data);

                    // Save the merged result to a file
                    autoFiller.Save(outputPath);
                }

                success = true;
                Console.WriteLine($"AutoFiller succeeded on attempt {attempt}.");
            }
            catch (IOException ex) when (IsTransient(ex))
            {
                // Transient I/O error – log and retry after a short delay
                Console.Error.WriteLine($"Transient I/O error on attempt {attempt}: {ex.Message}");
                if (attempt < maxRetries)
                {
                    Thread.Sleep(1000);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                // Access error – also retry after a delay
                Console.Error.WriteLine($"Access error on attempt {attempt}: {ex.Message}");
                if (attempt < maxRetries)
                {
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                // Non‑recoverable error – abort retries
                Console.Error.WriteLine($"Fatal error: {ex.Message}");
                break;
            }
        }

        if (!success)
        {
            Console.Error.WriteLine("AutoFiller failed after maximum retries.");
        }
    }

    // Simple heuristic: treat all IOExceptions as transient for this example
    static bool IsTransient(IOException ex) => true;
}