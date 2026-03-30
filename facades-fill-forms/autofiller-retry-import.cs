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
        const string outputPath = "merged_output.pdf";

        // Verify that the template file exists before attempting any operation.
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        // Prepare a simple DataTable with field names matching the PDF form fields.
        DataTable dataTable = new DataTable("Data");
        dataTable.Columns.Add("Field1", typeof(string));
        dataTable.Columns.Add("Field2", typeof(string));
        DataRow row = dataTable.NewRow();
        row["Field1"] = "Value1";
        row["Field2"] = "Value2";
        dataTable.Rows.Add(row);

        const int maxAttempts = 3;
        int attempt = 0;
        bool success = false;

        while (attempt < maxAttempts && !success)
        {
            AutoFiller autoFiller = null;
            try
            {
                // Initialise AutoFiller *after* we know the file exists.
                autoFiller = new AutoFiller();

                // Bind the template PDF. This operation may throw transient I/O exceptions.
                autoFiller.BindPdf(templatePath);

                // Import the data into the template.
                autoFiller.ImportDataTable(dataTable);

                // Save the merged result to a single PDF file.
                autoFiller.Save(outputPath);

                success = true; // If we reach here, everything succeeded.
            }
            catch (IOException ioEx)
            {
                attempt++;
                Console.Error.WriteLine($"Attempt {attempt} failed with I/O error: {ioEx.Message}");
                if (attempt >= maxAttempts)
                {
                    Console.Error.WriteLine("Maximum retry attempts reached. Operation aborted.");
                    throw;
                }
                Thread.Sleep(1000);
            }
            catch (UnauthorizedAccessException uaEx)
            {
                attempt++;
                Console.Error.WriteLine($"Attempt {attempt} failed with access error: {uaEx.Message}");
                if (attempt >= maxAttempts)
                {
                    Console.Error.WriteLine("Maximum retry attempts reached. Operation aborted.");
                    throw;
                }
                Thread.Sleep(1000);
            }
            finally
            {
                // Ensure the AutoFiller instance is disposed only if it was successfully created.
                // The null‑conditional operator prevents a NullReferenceException when Dispose is called on a null reference.
                autoFiller?.Dispose();
            }
        }

        if (success)
        {
            Console.WriteLine($"AutoFiller completed and saved to '{outputPath}'.");
        }
    }
}
