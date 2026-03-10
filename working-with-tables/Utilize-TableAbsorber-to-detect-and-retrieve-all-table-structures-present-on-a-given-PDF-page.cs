using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and page number (1‑based)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.pdf> <pageNumber>");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        if (!int.TryParse(args[1], out int pageNumber) || pageNumber < 1)
        {
            Console.Error.WriteLine("Invalid page number. Must be a positive integer.");
            return;
        }

        try
        {
            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document doc = new Document(inputPath))
            {
                // Ensure the requested page exists (pages are 1‑based)
                if (pageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page number {pageNumber} exceeds document page count {doc.Pages.Count}.");
                    return;
                }

                // Create a TableAbsorber to find tables
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables from the specified page
                absorber.Visit(doc.Pages[pageNumber]);

                // Access the detected tables via TableList
                var tables = absorber.TableList;
                Console.WriteLine($"Found {tables.Count} table(s) on page {pageNumber}.");

                // Iterate over each table and output its structure
                for (int t = 0; t < tables.Count; t++)
                {
                    var table = tables[t];
                    Console.WriteLine($"Table {t + 1}: Page={table.PageNum}, Rows={table.RowList.Count}");

                    for (int r = 0; r < table.RowList.Count; r++)
                    {
                        var row = table.RowList[r];
                        Console.WriteLine($"  Row {r + 1}: Cells={row.CellList.Count}");

                        for (int c = 0; c < row.CellList.Count; c++)
                        {
                            var cell = row.CellList[c];
                            // Concatenate all text fragments inside the cell
                            string cellText = "";
                            foreach (var fragment in cell.TextFragments)
                            {
                                cellText += fragment.Text;
                            }
                            Console.WriteLine($"    Cell {c + 1}: \"{cellText}\"");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}