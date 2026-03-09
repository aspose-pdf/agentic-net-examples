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

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Create a TableAbsorber instance to detect tables
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables from the entire document
                absorber.Visit(doc);

                // Enumerate the detected tables
                for (int i = 0; i < absorber.TableList.Count; i++)
                {
                    var table = absorber.TableList[i];
                    Console.WriteLine($"Table {i + 1}:");
                    Console.WriteLine($"  Page number: {table.PageNum}");
                    Console.WriteLine($"  Position: {table.Rectangle}");
                    Console.WriteLine($"  Row count: {table.RowList.Count}");

                    // Example: show number of cells in the first row, if any rows exist
                    if (table.RowList.Count > 0)
                    {
                        var firstRow = table.RowList[0];
                        Console.WriteLine($"  Cells in first row: {firstRow.CellList.Count}");
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