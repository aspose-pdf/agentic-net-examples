using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    // Path for the log file
    private const string LogFilePath = "process.log";

    static void Main()
    {
        // Prepare sample DataTable
        DataTable dataTable = new DataTable("Sample");
        dataTable.Columns.Add("ID", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Quantity", typeof(int));

        for (int i = 1; i <= 20; i++)
        {
            DataRow row = dataTable.NewRow();
            row["ID"] = i;
            row["Name"] = "Item " + i;
            row["Quantity"] = i * 10;
            dataTable.Rows.Add(row);
        }

        // Open a log file (overwrites any existing file)
        using (StreamWriter logWriter = new StreamWriter(LogFilePath, false))
        {
            // Create PDF document
            using (Document document = new Document())
            {
                // Add first page
                Page page = document.Pages.Add();

                // Create a table with three columns
                Table table = new Table
                {
                    // Define column widths (in points or percentages)
                    ColumnWidths = "100 200 100"
                };

                // Add header row
                Row header = table.Rows.Add();
                Cell headerCell1 = header.Cells.Add("ID");
                Cell headerCell2 = header.Cells.Add("Name");
                Cell headerCell3 = header.Cells.Add("Quantity");
                headerCell1.BackgroundColor = Aspose.Pdf.Color.LightGray;
                headerCell2.BackgroundColor = Aspose.Pdf.Color.LightGray;
                headerCell3.BackgroundColor = Aspose.Pdf.Color.LightGray;

                // Add data rows and log processing information
                for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
                {
                    DataRow dataRow = dataTable.Rows[rowIndex];
                    Row pdfRow = table.Rows.Add();
                    pdfRow.Cells.Add(dataRow["ID"].ToString());
                    pdfRow.Cells.Add(dataRow["Name"].ToString());
                    pdfRow.Cells.Add(dataRow["Quantity"].ToString());

                    // Log to console
                    Console.WriteLine(
                        "Processed DataTable row {0}, current PDF page count: {1}",
                        rowIndex + 1,
                        document.Pages.Count);

                    // Log to file (includes page number)
                    logWriter.WriteLine(
                        "Row {0}: ID={1}, Name={2}, Quantity={3}, Page={4}",
                        rowIndex + 1,
                        dataRow["ID"],
                        dataRow["Name"],
                        dataRow["Quantity"],
                        document.Pages.Count);
                }

                // Attach the table to the page
                page.Paragraphs.Add(table);

                // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
                string outputPath = "output.pdf";
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    document.Save(outputPath);
                }
                else
                {
                    try
                    {
                        document.Save(outputPath);
                    }
                    catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                    {
                        Console.WriteLine(
                            "Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                    }
                }
            }
        }

        Console.WriteLine("PDF creation finished. Log written to '{0}'.", LogFilePath);
    }

    // Helper method to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
