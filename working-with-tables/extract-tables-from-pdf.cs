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
            // Iterate over each page and absorb tables individually.
            // This way we know the page number without relying on a non‑existent property.
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(page);

                foreach (var table in absorber.TableList)
                {
                    Console.WriteLine($"Table found on page {pageIndex}");
                    Console.WriteLine($"Location: {table.Rectangle}");
                    Console.WriteLine($"Rows: {table.RowList.Count}");

                    if (table.RowList.Count > 0)
                    {
                        var firstRow = table.RowList[0];
                        Console.WriteLine($"Cells in first row: {firstRow.CellList.Count}");
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
