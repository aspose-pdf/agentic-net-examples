using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // <-- required for TableAbsorber

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Sample DataTable with an identifier column
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("ID", typeof(string));
        for (int r = 0; r < 10; r++)
        {
            DataRow row = dataTable.NewRow();
            row["ID"] = $"Row{r + 1}";
            dataTable.Rows.Add(row);
        }

        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(page);

                // Process each table found on the page
                for (int tableIndex = 0; tableIndex < absorber.TableList.Count; tableIndex++)
                {
                    // Process each row of the table
                    for (int rowIndex = 0; rowIndex < absorber.TableList[tableIndex].RowList.Count; rowIndex++)
                    {
                        string identifier = "N/A";
                        if (rowIndex < dataTable.Rows.Count)
                        {
                            identifier = dataTable.Rows[rowIndex]["ID"].ToString();
                        }
                        Console.WriteLine($"Page {pageIndex}, Table {tableIndex + 1}, Row {rowIndex + 1} -> DataTable ID: {identifier}");
                    }
                }
            }
        }
    }
}
