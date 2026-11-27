using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TableAbsorber resides in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string csvOutputPath = "TableSummary.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare CSV writer.
            using (StreamWriter csvWriter = new StreamWriter(csvOutputPath, false))
            {
                // Write CSV header.
                csvWriter.WriteLine("TableIndex,PageNumber,RowCount,ColumnCount");

                int globalTableIndex = 1; // Counter for tables across the whole document.

                // Iterate pages using 1‑based indexing (Aspose.Pdf requirement).
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    // Extract tables from the current page using the core TableAbsorber API.
                    TableAbsorber absorber = new TableAbsorber();
                    absorber.Visit(pdfDoc.Pages[pageNum]);

                    // Loop through each detected (absorbed) table.
                    foreach (var absorbedTable in absorber.TableList)
                    {
                        int rowCount = absorbedTable.RowList.Count;
                        int columnCount = 0;
                        if (rowCount > 0)
                        {
                            // Assuming a regular table, column count is the cell count of the first row.
                            columnCount = absorbedTable.RowList[0].CellList.Count;
                        }

                        // Write a CSV line for this table.
                        csvWriter.WriteLine($"{globalTableIndex},{pageNum},{rowCount},{columnCount}");
                        globalTableIndex++;
                    }
                }
            }
        }

        Console.WriteLine($"Table summary CSV created at '{csvOutputPath}'.");
    }
}
