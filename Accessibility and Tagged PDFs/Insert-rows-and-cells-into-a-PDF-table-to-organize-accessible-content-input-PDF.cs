using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Locate the first table on the first page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[1]);

            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found on the first page.");
                doc.Save(outputPath); // save unchanged document
                return;
            }

            // Get the first detected table (old table)
            AbsorbedTable oldTable = absorber.TableList[0];

            // Create a DataTable with additional rows/cells
            DataTable dt = new DataTable();
            // Assume the original table has 3 columns; adjust as needed
            dt.Columns.Add("Column 1");
            dt.Columns.Add("Column 2");
            dt.Columns.Add("Column 3");

            // Copy existing rows from the old table into the DataTable
            foreach (var row in oldTable.RowList)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < row.CellList.Count && i < dt.Columns.Count; i++)
                {
                    // Extract text from the first text fragment in each cell (if any)
                    string cellText = string.Empty;
                    if (row.CellList[i].TextFragments.Count > 0)
                        cellText = row.CellList[i].TextFragments[0].Text;
                    dr[i] = cellText;
                }
                dt.Rows.Add(dr);
            }

            // Add new rows to the DataTable
            DataRow newRow1 = dt.NewRow();
            newRow1[0] = "New Cell 1A";
            newRow1[1] = "New Cell 1B";
            newRow1[2] = "New Cell 1C";
            dt.Rows.Add(newRow1);

            DataRow newRow2 = dt.NewRow();
            newRow2[0] = "New Cell 2A";
            newRow2[1] = "New Cell 2B";
            newRow2[2] = "New Cell 2C";
            dt.Rows.Add(newRow2);

            // Create a new Table and import the DataTable (including new rows)
            Table newTable = new Table();
            // Preserve column widths if needed (optional)
            // newTable.ColumnWidths = oldTable.Table.ColumnWidths;

            // Import data: column names are not imported as a separate row (false)
            newTable.ImportDataTable(dt, false, 0, 0);

            // Replace the old table with the new one (accessibility is retained)
            absorber.Replace(doc.Pages[1], oldTable, newTable);

            // Save the modified PDF (lifecycle rule: use Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table updated and saved to '{outputPath}'.");
    }
}