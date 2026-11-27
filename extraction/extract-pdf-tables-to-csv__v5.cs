using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "tables.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF document inside a using block (deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create a CSV writer
            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                // CSV header: include a column for the source page number
                writer.WriteLine("PageNumber,TableIndex,RowIndex,ColumnIndex,CellText");

                // Pages are 1‑based (Aspose.Pdf rule)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];

                    // Extract tables from the current page using TableAbsorber
                    TableAbsorber absorber = new TableAbsorber();
                    absorber.Visit(page);

                    // Iterate over each absorbed table found on the page
                    for (int t = 0; t < absorber.TableList.Count; t++)
                    {
                        var absorbedTable = absorber.TableList[t];

                        // Iterate rows
                        for (int r = 0; r < absorbedTable.RowList.Count; r++)
                        {
                            var absorbedRow = absorbedTable.RowList[r];

                            // Iterate cells
                            for (int c = 0; c < absorbedRow.CellList.Count; c++)
                            {
                                var absorbedCell = absorbedRow.CellList[c];
                                // Concatenate all text fragments inside the cell
                                string cellText = string.Concat(absorbedCell.TextFragments.Select(tf => tf.Text ?? string.Empty));
                                // Escape double quotes and wrap text in quotes to handle commas
                                cellText = cellText.Replace("\"", "\"\"");
                                writer.WriteLine($"{pageNum},{t + 1},{r + 1},{c + 1},\"{cellText}\"");
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Table data exported to '{outputCsv}'.");
    }
}
