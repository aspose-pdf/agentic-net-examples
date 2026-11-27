using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "summary.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        using (StreamWriter csvWriter = new StreamWriter(outputCsvPath))
        {
            csvWriter.WriteLine("TableIndex,PageNumber,RowCount,ColumnCount");

            int tableIndex = 1;

            // Pages are 1‑based in Aspose.Pdf
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Extract tables from the current page
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(page);

                // Iterate over each detected (absorbed) table
                foreach (var absorbedTable in absorber.TableList)
                {
                    // Row count is the number of rows in the absorbed table
                    int rowCount = absorbedTable.RowList.Count;

                    // Column count – assume a regular table and use the first row's cell count
                    int columnCount = 0;
                    if (rowCount > 0)
                        columnCount = absorbedTable.RowList[0].CellList.Count;

                    // Write a CSV line with the table summary
                    csvWriter.WriteLine($"{tableIndex},{pageNum},{rowCount},{columnCount}");
                    tableIndex++;
                }
            }
        }

        Console.WriteLine($"Table summary CSV created at '{outputCsvPath}'.");
    }
}
