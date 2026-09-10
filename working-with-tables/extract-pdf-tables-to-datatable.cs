using System;
using System.Data;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class TableExtractor
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // -------------------------------------------------
        // Create a sample PDF with a simple table if it does not exist
        // -------------------------------------------------
        if (!System.IO.File.Exists(inputPath))
        {
            using (Document seed = new Document())
            {
                Page page = seed.Pages.Add();

                // Build a simple 2x3 table
                Table table = new Table
                {
                    ColumnWidths = "100 100 100"
                };

                // Header row
                Row header = table.Rows.Add();
                header.Cells.Add("Header 1");
                header.Cells.Add("Header 2");
                header.Cells.Add("Header 3");

                // Data rows
                Row row1 = table.Rows.Add();
                row1.Cells.Add("R1C1");
                row1.Cells.Add("R1C2");
                row1.Cells.Add("R1C3");

                Row row2 = table.Rows.Add();
                row2.Cells.Add("R2C1");
                row2.Cells.Add("R2C2");
                row2.Cells.Add("R2C3");

                page.Paragraphs.Add(table);
                seed.Save(inputPath);
            }
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc);

            for (int t = 0; t < absorber.TableList.Count; t++)
            {
                var absorbedTable = absorber.TableList[t];

                int maxColumns = absorbedTable.RowList.Max(r => r.CellList.Count);
                DataTable dataTable = new DataTable($"Table_{t + 1}");

                for (int c = 0; c < maxColumns; c++)
                {
                    dataTable.Columns.Add($"Column{c + 1}", typeof(string));
                }

                foreach (var row in absorbedTable.RowList)
                {
                    DataRow dataRow = dataTable.NewRow();

                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];
                        string cellText = string.Concat(cell.TextFragments.Select(tf => tf.Text));
                        dataRow[c] = cellText;
                    }

                    dataTable.Rows.Add(dataRow);
                }

                Console.WriteLine($"--- Extracted {dataTable.TableName} ---");
                foreach (DataRow dr in dataTable.Rows)
                {
                    string line = string.Join(" | ", dr.ItemArray.Select(v => v?.ToString() ?? string.Empty));
                    Console.WriteLine(line);
                }
                Console.WriteLine();
            }
        }
    }
}
