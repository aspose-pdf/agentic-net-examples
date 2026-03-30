using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;

public class Program
{
    private const string LogFilePath = "process.log";

    public static void Main()
    {
        // Prepare a sample DataTable
        DataTable dataTable = new DataTable("Sample");
        dataTable.Columns.Add("ID", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Value", typeof(double));

        DataRow row1 = dataTable.NewRow();
        row1["ID"] = 1;
        row1["Name"] = "Alpha";
        row1["Value"] = 123.45;
        dataTable.Rows.Add(row1);

        DataRow row2 = dataTable.NewRow();
        row2["ID"] = 2;
        row2["Name"] = "Beta";
        row2["Value"] = 678.90;
        dataTable.Rows.Add(row2);

        // Create PDF document
        using (Document pdfDocument = new Document())
        {
            // Iterate through each DataRow
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow currentRow = dataTable.Rows[i];

                // Add a new page for this row
                Page page = pdfDocument.Pages.Add();

                // Create a table and optionally add a header row on the first page
                Table table = new Table();
                if (i == 0)
                {
                    Row headerRow = table.Rows.Add();
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        Cell headerCell = headerRow.Cells.Add(column.ColumnName);
                    }
                }

                // Add the data row
                Row dataRow = table.Rows.Add();
                foreach (DataColumn column in dataTable.Columns)
                {
                    object cellValue = currentRow[column];
                    Cell dataCell = dataRow.Cells.Add(cellValue != null ? cellValue.ToString() : string.Empty);
                }

                // Add the table to the page
                page.Paragraphs.Add(table);

                // Log the processed row and the page number (1‑based)
                int pageNumber = pdfDocument.Pages.Count;
                string logMessage = $"Processed DataTable row {i + 1}, created page {pageNumber}";
                Console.WriteLine(logMessage);
                Log(logMessage);
            }

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            string outputPath = "output.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                pdfDocument.Save(outputPath);
                Log($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    pdfDocument.Save(outputPath);
                    Log($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Log("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    private static void Log(string message)
    {
        try
        {
            File.AppendAllText(LogFilePath, $"{DateTime.UtcNow:O} {message}{Environment.NewLine}");
        }
        catch
        {
            // Swallow logging errors – they should not crash the main flow.
        }
    }

    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
