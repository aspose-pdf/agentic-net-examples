using System;
using System.Data;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath = "merged_output.pdf";
        const int maxRetries = 3;
        const int delayMilliseconds = 1000;

        // Ensure a template PDF exists so the demo does not crash with FileNotFoundException.
        // In a real scenario the file would already be present.
        if (!File.Exists(templatePath))
        {
            var emptyDoc = new Document();
            emptyDoc.Save(templatePath);
        }

        // Prepare a sample DataTable (replace with real data as needed)
        DataTable dataTable = new DataTable("Sample");
        dataTable.Columns.Add("Field1", typeof(string));
        dataTable.Columns.Add("Field2", typeof(string));
        DataRow row = dataTable.NewRow();
        row["Field1"] = "Value1";
        row["Field2"] = "Value2";
        dataTable.Rows.Add(row);

        int attempt = 0;
        bool success = false;
        while (attempt < maxRetries && !success)
        {
            try
            {
                AutoFiller autoFiller = new AutoFiller();
                autoFiller.BindPdf(templatePath);
                autoFiller.ImportDataTable(dataTable);
                autoFiller.Save(outputPath);
                success = true;
            }
            // More specific exception first
            catch (FileNotFoundException fnfEx) // treat missing template as a retryable condition (e.g., file may be created by another process)
            {
                attempt++;
                if (attempt >= maxRetries)
                {
                    Console.Error.WriteLine($"Failed after {maxRetries} attempts: {fnfEx.Message}");
                    throw;
                }
                Console.WriteLine($"Template file not found. Retrying {attempt}/{maxRetries}...");
                Thread.Sleep(delayMilliseconds);
            }
            catch (IOException ioEx) // includes transient I/O errors like sharing violations
            {
                attempt++;
                if (attempt >= maxRetries)
                {
                    Console.Error.WriteLine($"Failed after {maxRetries} attempts: {ioEx.Message}");
                    throw;
                }
                Console.WriteLine($"Transient I/O error encountered. Retrying {attempt}/{maxRetries}...");
                Thread.Sleep(delayMilliseconds);
            }
            catch (UnauthorizedAccessException uaEx)
            {
                attempt++;
                if (attempt >= maxRetries)
                {
                    Console.Error.WriteLine($"Failed after {maxRetries} attempts: {uaEx.Message}");
                    throw;
                }
                Console.WriteLine($"Access error encountered. Retrying {attempt}/{maxRetries}...");
                Thread.Sleep(delayMilliseconds);
            }
        }

        if (success)
        {
            Console.WriteLine($"AutoFiller completed and saved to '{outputPath}'.");
        }
    }
}
