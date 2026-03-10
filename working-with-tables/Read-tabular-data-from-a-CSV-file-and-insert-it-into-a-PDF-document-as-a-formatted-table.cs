using System;
using System.IO;
using System.Data;
using Aspose.Pdf;               // Core PDF classes
using Aspose.Pdf.Text;          // For text-related types if needed

class Program
{
    static void Main()
    {
        const string csvPath = "data.csv";
        const string pdfPath = "output.pdf";

        // Verify CSV file exists
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Load CSV data into a DataTable
        DataTable dataTable = LoadCsvToDataTable(csvPath);

        // Create PDF document inside a using block (ensures disposal)
        using (Document doc = new Document())
        {
            // Add a new page (first page is index 1)
            Page page = doc.Pages.Add();

            // Create a Table instance and set basic formatting
            Table table = new Table
            {
                // Draw borders around the table and cells
                Border = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                // Add some padding inside each cell
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Import the DataTable into the Aspose.Pdf.Table
            // Parameters: (DataTable, import column names as first row, start row, start column)
            table.ImportDataTable(dataTable, true, 0, 0);

            // Add the populated table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document (PDF format is default for Save(string))
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF successfully created at '{pdfPath}'.");
    }

    // Helper method to read a CSV file into a System.Data.DataTable
    static DataTable LoadCsvToDataTable(string filePath)
    {
        DataTable dt = new DataTable();

        using (StreamReader sr = new StreamReader(filePath))
        {
            bool isFirstLine = true;

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (line == null) continue;

                // Simple CSV split on commas (does not handle quoted commas)
                string[] fields = line.Split(',');

                if (isFirstLine)
                {
                    // First line defines column headers
                    foreach (string header in fields)
                    {
                        dt.Columns.Add(header.Trim());
                    }
                    isFirstLine = false;
                }
                else
                {
                    // Subsequent lines are data rows
                    DataRow row = dt.NewRow();
                    for (int i = 0; i < fields.Length && i < dt.Columns.Count; i++)
                    {
                        row[i] = fields[i].Trim();
                    }
                    dt.Rows.Add(row);
                }
            }
        }

        return dt;
    }
}