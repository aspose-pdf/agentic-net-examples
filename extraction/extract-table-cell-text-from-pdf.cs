using System;
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // TableAbsorber extracts tables and their cells from pages
            TableAbsorber absorber = new TableAbsorber();

            // Pages are 1‑based in Aspose.Pdf
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Absorb tables on the current page
                absorber.Visit(doc.Pages[pageNum]);

                // Iterate over each extracted table
                foreach (AbsorbedTable table in absorber.TableList)
                {
                    Console.WriteLine($"Table on page {pageNum}: {table.RowList.Count} rows, {table.RowList[0].CellList.Count} columns");

                    // Iterate rows
                    for (int r = 0; r < table.RowList.Count; r++)
                    {
                        AbsorbedRow row = table.RowList[r];
                        // Iterate cells in the row
                        for (int c = 0; c < row.CellList.Count; c++)
                        {
                            AbsorbedCell cell = row.CellList[c];

                            // Combine the text of all fragments within the cell
                            string cellText = string.Empty;
                            foreach (TextFragment fragment in cell.TextFragments)
                            {
                                cellText += fragment.Text;
                            }

                            Console.WriteLine($"Cell[{r},{c}] = \"{cellText}\"");
                        }
                    }
                }

                // Clear the list to avoid processing the same tables on the next page
                absorber.TableList.Clear();
            }
        }
    }
}
