using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string csvPath   = "data.csv";
        const string pdfPath   = "output.pdf";

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Load CSV into a DataTable (first row contains column headings)
        DataTable tableData = new DataTable();
        using (var reader = new StreamReader(csvPath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] fields = line.Split(',');

                if (isFirstLine)
                {
                    // Create columns from the header row
                    foreach (string col in fields)
                        tableData.Columns.Add(col.Trim());
                    isFirstLine = false;
                }
                else
                {
                    // Add data rows
                    DataRow row = tableData.NewRow();
                    for (int i = 0; i < fields.Length; i++)
                        row[i] = fields[i].Trim();
                    tableData.Rows.Add(row);
                }
            }
        }

        // Create a new PDF document and add a page
        using (Document pdfDoc = new Document())
        {
            Page page = pdfDoc.Pages.Add();

            // Create an Aspose.Pdf Table
            Table pdfTable = new Table
            {
                // Optional visual formatting
                Border = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                Alignment = HorizontalAlignment.Center
            };

            // Import the DataTable into the PDF table.
            // Parameters: (DataTable, isColumnNamesImported, firstFilledRow, firstFilledColumn)
            pdfTable.ImportDataTable(tableData, true, 0, 0);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(pdfTable);

            // Save the PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF with table saved to '{pdfPath}'.");
    }
}