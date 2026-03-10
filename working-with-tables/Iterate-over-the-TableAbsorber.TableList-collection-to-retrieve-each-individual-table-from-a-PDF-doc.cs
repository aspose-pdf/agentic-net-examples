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

        // Load the PDF document (disposed automatically)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            // Iterate over each table found
            for (int i = 0; i < absorber.TableList.Count; i++)
            {
                AbsorbedTable table = absorber.TableList[i];
                Console.WriteLine($"Table {i + 1} found on page {table.PageNum}");
                Console.WriteLine($"  Position: {table.Rectangle}");
                Console.WriteLine($"  Rows: {table.RowList.Count}");

                // Iterate rows and cells within the table
                for (int r = 0; r < table.RowList.Count; r++)
                {
                    var row = table.RowList[r];
                    Console.WriteLine($"    Row {r + 1} cells: {row.CellList.Count}");

                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];
                        // Concatenate all text fragments in the cell
                        string cellText = "";
                        foreach (var fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }
                        Console.WriteLine($"      Cell {c + 1}: \"{cellText}\"");
                    }
                }
            }
        }
    }
}