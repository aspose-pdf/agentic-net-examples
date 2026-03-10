using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "extracted_tables.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to analyze (1‑based indexing)
            int pageNumber = 1;
            if (pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} does not exist.");
                return;
            }

            // Create a TableAbsorber instance
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the selected page
            absorber.Visit(doc.Pages[pageNumber]);

            // Write extracted table information to a text file
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                writer.WriteLine($"Found {absorber.TableList.Count} table(s) on page {pageNumber}.");

                for (int t = 0; t < absorber.TableList.Count; t++)
                {
                    var table = absorber.TableList[t];
                    writer.WriteLine($"Table {t + 1}:");
                    writer.WriteLine($"  Position: {table.Rectangle}");
                    writer.WriteLine($"  Rows: {table.RowList.Count}");

                    for (int r = 0; r < table.RowList.Count; r++)
                    {
                        var row = table.RowList[r];
                        writer.WriteLine($"    Row {r + 1}: Cells={row.CellList.Count}");

                        for (int c = 0; c < row.CellList.Count; c++)
                        {
                            var cell = row.CellList[c];
                            writer.Write($"      Cell {c + 1}: ");

                            // Concatenate all text fragments inside the cell
                            for (int f = 0; f < cell.TextFragments.Count; f++)
                            {
                                var fragment = cell.TextFragments[f];
                                writer.Write(fragment.Text);
                                if (f < cell.TextFragments.Count - 1) writer.Write(" ");
                            }
                            writer.WriteLine();
                        }
                    }
                }
            }

            Console.WriteLine($"Extraction completed. Results saved to '{outputPath}'.");
        }
    }
}