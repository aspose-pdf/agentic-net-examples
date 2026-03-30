using System;
using System.Data;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputTemplatePath = "template.pdf";
        const string outputPath = "filled.pdf";
        const int maxRetryAttempts = 3;
        const int retryDelayMilliseconds = 1000;

        if (!File.Exists(inputTemplatePath))
        {
            Console.Error.WriteLine($"Template file not found: {inputTemplatePath}");
            return;
        }

        // Prepare sample data table matching form field names
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("FirstName", typeof(string));
        dataTable.Columns.Add("LastName", typeof(string));
        DataRow row = dataTable.NewRow();
        row["FirstName"] = "John";
        row["LastName"] = "Doe";
        dataTable.Rows.Add(row);

        using (AutoFiller autoFiller = new AutoFiller())
        {
            // Bind the PDF template file
            autoFiller.BindPdf(inputTemplatePath);

            int attempt = 0;
            bool imported = false;
            while (attempt < maxRetryAttempts && !imported)
            {
                try
                {
                    autoFiller.ImportDataTable(dataTable);
                    imported = true;
                }
                catch (IOException ioEx)
                {
                    attempt++;
                    if (attempt >= maxRetryAttempts)
                    {
                        Console.Error.WriteLine($"Import failed after {attempt} attempts: {ioEx.Message}");
                        throw;
                    }
                    Console.WriteLine($"Transient I/O error encountered (attempt {attempt}), retrying after delay...");
                    Thread.Sleep(retryDelayMilliseconds);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Unexpected error during import: {ex.Message}");
                    throw;
                }
            }

            // Save the filled PDF
            autoFiller.Save(outputPath);
            Console.WriteLine($"PDF successfully filled and saved to '{outputPath}'.");
        }
    }
}