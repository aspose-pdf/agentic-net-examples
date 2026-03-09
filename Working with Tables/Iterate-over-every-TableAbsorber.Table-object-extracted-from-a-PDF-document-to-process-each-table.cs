using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document
            absorber.Visit(doc);

            // Iterate over each extracted table
            foreach (AbsorbedTable table in absorber.TableList)
            {
                // Example processing: output page number and row count
                Console.WriteLine($"Table on page {table.PageNum} with {table.RowList.Count} rows.");

                // Iterate rows and cells to read cell text
                for (int r = 0; r < table.RowList.Count; r++)
                {
                    var row = table.RowList[r];
                    Console.WriteLine($"  Row {r + 1} has {row.CellList.Count} cells.");

                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];
                        string cellText = string.Empty;
                        foreach (TextFragment tf in cell.TextFragments)
                        {
                            cellText += tf.Text;
                        }
                        Console.WriteLine($"    Cell {c + 1}: \"{cellText}\"");
                    }
                }

                // Example modification: change the first text fragment of the first cell
                if (table.RowList.Count > 0 && table.RowList[0].CellList.Count > 0)
                {
                    var firstCell = table.RowList[0].CellList[0];
                    if (firstCell.TextFragments.Count > 0)
                    {
                        firstCell.TextFragments[0].Text = "Modified";
                    }
                }
            }

            // Save the (potentially modified) document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}