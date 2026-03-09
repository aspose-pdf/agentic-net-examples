using System;
using System.IO;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for MarginInfo if needed

class Program
{
    static void Main()
    {
        const string csvPath = "data.csv";
        const string pdfPath = "output.pdf";

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Load CSV into a DataTable
        DataTable dataTable = new DataTable();
        using (var reader = new StreamReader(csvPath))
        {
            bool firstLine = true;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null) continue;

                // Simple CSV split – assumes no commas inside quoted fields
                string[] fields = line.Split(',');

                if (firstLine)
                {
                    // Create columns from header row
                    foreach (string col in fields)
                        dataTable.Columns.Add(col);
                    firstLine = false;
                }
                else
                {
                    // Add data rows
                    DataRow row = dataTable.NewRow();
                    for (int i = 0; i < fields.Length; i++)
                        row[i] = fields[i];
                    dataTable.Rows.Add(row);
                }
            }
        }

        // Create PDF document and add the table
        using (Document doc = new Document())
        {
            // Add a new page
            Page page = doc.Pages.Add();

            // Create a table with basic formatting
            Table table = new Table
            {
                // Border around the whole table
                Border = new BorderInfo(BorderSide.All, 0.5f),

                // Default cell border and padding
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),

                // Auto‑fit columns to content (updated enum name for newer Aspose.PDF versions)
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent
            };

            // Import the DataTable into the Aspose.Pdf.Table
            // Parameters: (DataTable, import column names, start row, start column)
            table.ImportDataTable(dataTable, true, 0, 0);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF created successfully at '{pdfPath}'.");
    }
}
