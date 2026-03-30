using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        using (Document doc = new Document(inputPath))
        {
            // Optional: DataTable to store identifiers (demonstration purpose)
            DataTable identifierTable = new DataTable();
            identifierTable.Columns.Add("RowId", typeof(string));

            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                Page page = doc.Pages[pageNumber];
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(page);

                // Process each extracted table on the current page
                for (int tableIdx = 0; tableIdx < absorber.TableList.Count; tableIdx++)
                {
                    // Process each row of the table
                    for (int rowIdx = 0; rowIdx < absorber.TableList[tableIdx].RowList.Count; rowIdx++)
                    {
                        // Assume the first cell contains the row identifier
                        TextFragment idFragment = absorber.TableList[tableIdx].RowList[rowIdx].CellList[0].TextFragments[0];
                        string rowId = idFragment.Text.Trim();

                        // Store identifier in the DataTable (optional)
                        DataRow dataRow = identifierTable.NewRow();
                        dataRow["RowId"] = rowId;
                        identifierTable.Rows.Add(dataRow);

                        // Output summary line
                        Console.WriteLine($"Page {pageNumber}: RowId = {rowId}");
                    }
                }
            }
        }
    }
}