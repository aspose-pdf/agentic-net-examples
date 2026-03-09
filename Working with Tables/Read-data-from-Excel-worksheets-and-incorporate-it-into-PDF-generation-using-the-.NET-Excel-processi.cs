using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using System.Collections.Generic;
using System.Linq;

class ExcelToPdf
{
    static void Main()
    {
        // Paths to the source data file (CSV for this example), an optional PDF template, and the output PDF.
        const string dataPath = "Data.csv";          // CSV representation of the Excel sheet.
        const string templatePdf = "Template.pdf";   // can be an existing PDF or omitted.
        const string outputPdf = "Result.pdf";

        // Verify that the data file exists.
        if (!File.Exists(dataPath))
        {
            Console.Error.WriteLine($"Data file not found: {dataPath}");
            return;
        }

        // --------------------------------------------------------------------
        // 1. Load data from the CSV file into a DataTable.
        // --------------------------------------------------------------------
        DataTable excelTable = LoadCsvToDataTable(dataPath);

        // --------------------------------------------------------------------
        // 2. Create a PDF document (either from a template or a new blank document).
        // --------------------------------------------------------------------
        using (Document pdfDoc = File.Exists(templatePdf)
                                   ? new Document(templatePdf)   // load existing template
                                   : new Document())            // start with a blank PDF
        {
            // Ensure there is at least one page to host the table.
            Page page = pdfDoc.Pages.Count > 0 ? pdfDoc.Pages[1] : pdfDoc.Pages.Add();

            // ----------------------------------------------------------------
            // 3. Create an Aspose.Pdf.Table and import the DataTable content.
            // ----------------------------------------------------------------
            Table pdfTable = new Table();

            // Optional: set column widths based on the number of columns.
            int columnCount = excelTable.Columns.Count;
            if (columnCount > 0)
            {
                // Distribute columns equally across a total width of 500 points.
                var widths = new List<string>();
                for (int i = 0; i < columnCount; i++)
                {
                    widths.Add((500.0 / columnCount).ToString(System.Globalization.CultureInfo.InvariantCulture));
                }
                // Table.ColumnWidths expects a space‑separated string, not a collection.
                pdfTable.ColumnWidths = string.Join(" ", widths);
            }

            // Import the DataTable.
            // Parameters:
            //   - excelTable: source data.
            //   - true: import column names as the first row.
            //   - 0: start importing at the first row of the PDF table (zero‑based).
            //   - 0: start at the first column of the PDF table (zero‑based).
            pdfTable.ImportDataTable(excelTable, true, 0, 0);

            // Optional: style the header row (first row) for better readability.
            if (pdfTable.Rows.Count > 0)
            {
                // Aspose.Pdf uses 1‑based indexing for rows.
                Row headerRow = pdfTable.Rows[1];
                foreach (Cell cell in headerRow.Cells)
                {
                    // Set a light gray background.
                    cell.BackgroundColor = Aspose.Pdf.Color.LightGray;

                    // Make header text bold.
                    if (cell.Paragraphs[1] is TextFragment tf)
                    {
                        tf.TextState.FontStyle = FontStyles.Bold;
                        tf.TextState.FontSize = 12;
                    }
                }
            }

            // Add the table to the page.
            page.Paragraphs.Add(pdfTable);

            // ----------------------------------------------------------------
            // 4. Save the resulting PDF.
            // ----------------------------------------------------------------
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated successfully: {outputPdf}");
    }

    /// <summary>
    /// Loads a CSV file into a DataTable. The first line is treated as column headers.
    /// This method replaces the previous OleDb based Excel reader to avoid platform‑specific dependencies.
    /// </summary>
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var table = new DataTable();
        using (var reader = new StreamReader(csvFilePath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null) continue;
                // Simple CSV split – does not handle quoted commas.
                var values = line.Split(',');
                if (isFirstLine)
                {
                    foreach (var header in values)
                    {
                        table.Columns.Add(header.Trim());
                    }
                    isFirstLine = false;
                }
                else
                {
                    var row = table.NewRow();
                    for (int i = 0; i < values.Length && i < table.Columns.Count; i++)
                    {
                        row[i] = values[i].Trim();
                    }
                    table.Rows.Add(row);
                }
            }
        }
        return table;
    }
}
