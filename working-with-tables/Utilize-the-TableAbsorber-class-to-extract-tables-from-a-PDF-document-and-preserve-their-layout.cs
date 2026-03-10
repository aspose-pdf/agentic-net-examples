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

        // Open the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            // Iterate over all found tables
            for (int t = 0; t < absorber.TableList.Count; t++)
            {
                AbsorbedTable table = absorber.TableList[t];
                Console.WriteLine($"Table {t + 1} on page {table.PageNum}:");

                // Iterate over rows
                for (int r = 0; r < table.RowList.Count; r++)
                {
                    var row = table.RowList[r];
                    // Iterate over cells in the row
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];
                        // Concatenate all text fragments inside the cell
                        string cellText = string.Empty;
                        foreach (TextFragment fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }

                        Console.Write($"\"{cellText}\"");
                        if (c < row.CellList.Count - 1)
                            Console.Write("\t"); // tab separator between cells
                    }
                    Console.WriteLine(); // new line after each row
                }

                Console.WriteLine(new string('-', 40));
            }

            // No modifications are made; simply save the original PDF (optional)
            // doc.Save("output.pdf"); // Uncomment if you need to save a copy
        }
    }
}