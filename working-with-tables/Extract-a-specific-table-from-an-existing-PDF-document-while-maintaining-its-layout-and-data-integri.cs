using System;
using System.Data;
using System.IO;
using System.Linq; // Added for LINQ extensions
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "extracted_table.pdf";
        const int pageNumber = 1;      // 1‑based page index containing the table
        const int tableIndex = 0;      // zero‑based index of the table on that page

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load source PDF
        using (Document srcDoc = new Document(inputPath))
        {
            // Locate tables on the specified page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(srcDoc.Pages[pageNumber]);

            if (absorber.TableList.Count <= tableIndex)
            {
                Console.Error.WriteLine("Specified table not found on the page.");
                return;
            }

            // Get the desired absorbed table
            var absorbedTable = absorber.TableList[tableIndex];

            // Convert the absorbed table into a System.Data.DataTable
            DataTable dt = new DataTable();

            // Determine column count from the first row
            int columnCount = absorbedTable.RowList[0].CellList.Count;
            for (int c = 0; c < columnCount; c++)
                dt.Columns.Add($"Column{c + 1}");

            // Populate rows
            foreach (var row in absorbedTable.RowList)
            {
                DataRow dr = dt.NewRow();
                for (int c = 0; c < columnCount; c++)
                {
                    var cell = row.CellList[c];
                    // Concatenate all text fragments inside the cell
                    string cellText = string.Empty;
                    foreach (var fragment in cell.TextFragments)
                        cellText += fragment.Text;
                    dr[c] = cellText;
                }
                dt.Rows.Add(dr);
            }

            // Create a new PDF that will contain only the extracted table
            using (Document outDoc = new Document())
            {
                // Add a blank page
                Page page = outDoc.Pages.Add();

                // Build a new Aspose.Pdf.Table and import the DataTable
                Table table = new Table
                {
                    // Simple equal column widths; adjust as needed for original layout
                    ColumnWidths = string.Join(" ", Enumerable.Repeat("100", columnCount)),
                    DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                    DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
                };
                table.ImportDataTable(dt, true, 0, 0);

                // Add the table to the page
                page.Paragraphs.Add(table);

                // Save the new PDF
                outDoc.Save(outputPath);
            }

            Console.WriteLine($"Table extracted and saved to '{outputPath}'.");
        }
    }
}
