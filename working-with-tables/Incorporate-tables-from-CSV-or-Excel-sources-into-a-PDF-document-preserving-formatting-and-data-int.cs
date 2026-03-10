using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades; // Facades namespace included as requested

class Program
{
    static void Main()
    {
        const string csvPath = "data.csv";          // Path to the CSV source
        const string outputPdf = "output.pdf";      // Path for the generated PDF

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Load CSV data into a DataTable
        DataTable dataTable = LoadCsvIntoDataTable(csvPath);

        // Create PDF document and add a page
        using (Document pdfDoc = new Document())
        {
            Page page = pdfDoc.Pages.Add();

            // Create a table and configure its appearance
            Table table = new Table
            {
                // Simple equal column widths based on column count
                ColumnWidths = GenerateColumnWidths(dataTable.Columns.Count),

                // Outer border for the whole table
                Border = new BorderInfo(BorderSide.All, 1),

                // Default border for each cell
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),

                // Default text formatting for cells
                DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    ForegroundColor = Aspose.Pdf.Color.Black
                }
            };

            // Import the DataTable into the PDF table (include column headers)
            table.ImportDataTable(dataTable, true, 0, 0);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created successfully: {outputPdf}");
    }

    // Helper: reads a CSV file (comma‑separated, first line contains headers) into a DataTable
    private static DataTable LoadCsvIntoDataTable(string csvFilePath)
    {
        DataTable dt = new DataTable();

        using (var reader = new StreamReader(csvFilePath))
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
                    // Create columns using the header row
                    foreach (string header in fields)
                        dt.Columns.Add(header.Trim());
                    isFirstLine = false;
                }
                else
                {
                    // Add data rows
                    DataRow row = dt.NewRow();
                    for (int i = 0; i < fields.Length; i++)
                        row[i] = fields[i].Trim();
                    dt.Rows.Add(row);
                }
            }
        }

        return dt;
    }

    // Helper: generates a space‑separated column width string (e.g., "100 100 100")
    private static string GenerateColumnWidths(int columnCount)
    {
        if (columnCount <= 0)
            return string.Empty;

        // Use a fixed width of 100 points per column; adjust as needed
        string[] widths = new string[columnCount];
        for (int i = 0; i < columnCount; i++)
            widths[i] = "100";

        return string.Join(" ", widths);
    }
}