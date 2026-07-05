using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Simple logger that writes messages to a text file.
    private static readonly string LogFilePath = "process.log";
    private static void Log(string message)
    {
        // Append the message with a timestamp.
        File.AppendAllText(LogFilePath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}");
    }

    static void Main()
    {
        // Sample DataTable with some data
        DataTable dataTable = new DataTable("Sample");
        dataTable.Columns.Add("ID", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Value", typeof(double));

        for (int i = 1; i <= 10; i++)
        {
            dataTable.Rows.Add(i, $"Item {i}", i * 10.5);
        }

        const string outputPdf = "output.pdf";

        // Aspose evaluation mode allows only 4 pages to be created.
        // Guard against the limitation by processing at most 4 rows.
        const int maxPagesAllowedByEvaluation = 4;

        // Ensure the log file starts empty for each run.
        if (File.Exists(LogFilePath)) File.Delete(LogFilePath);

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Process each DataTable row individually (up to the evaluation limit)
            for (int i = 0; i < dataTable.Rows.Count && i < maxPagesAllowedByEvaluation; i++)
            {
                // Add a new page for the current row
                doc.Pages.Add();
                // Pages collection is 1‑based, so the newly added page is at index doc.Pages.Count
                Page page = doc.Pages[doc.Pages.Count];

                // Create a table and add a single row with the DataTable values
                Table table = new Table
                {
                    // Simple column widths; adjust as needed
                    ColumnWidths = "100 200 100"
                };

                // Add header row only on the first page
                if (i == 0)
                {
                    Row header = table.Rows.Add();
                    header.Cells.Add("ID");
                    header.Cells.Add("Name");
                    header.Cells.Add("Value");
                }

                // Add the data row
                Row dataRow = table.Rows.Add();
                dataRow.Cells.Add(dataTable.Rows[i]["ID"].ToString());
                dataRow.Cells.Add(dataTable.Rows[i]["Name"].ToString());
                dataRow.Cells.Add(dataTable.Rows[i]["Value"].ToString());

                // Add the table to the page
                page.Paragraphs.Add(table);

                // Log the processed row and the page number it was placed on (both to console and file)
                string logMessage = $"Processed DataTable row {i + 1} → added to page {doc.Pages.Count}";
                Console.WriteLine(logMessage);
                Log(logMessage);
            }

            // Save the PDF (PDF format, no special SaveOptions needed)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
        Log($"PDF saved to '{outputPdf}'.");
    }
}
