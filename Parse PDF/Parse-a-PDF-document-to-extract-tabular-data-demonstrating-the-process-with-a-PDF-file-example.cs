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

        // Open the PDF document; using ensures proper disposal.
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables in the document.
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document.
            absorber.Visit(doc);

            // Iterate over each found table.
            for (int t = 0; t < absorber.TableList.Count; t++)
            {
                var table = absorber.TableList[t];
                Console.WriteLine($"Table {t + 1}: {table.RowList.Count} rows");

                // Iterate over rows.
                for (int r = 0; r < table.RowList.Count; r++)
                {
                    var row = table.RowList[r];

                    // Iterate over cells in the current row.
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];

                        // Concatenate all text fragments inside the cell.
                        string cellText = "";
                        foreach (var fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }

                        // Output cell text separated by tabs.
                        Console.Write($"{cellText}\t");
                    }

                    Console.WriteLine(); // End of row.
                }

                Console.WriteLine(); // Blank line between tables.
            }

            // Example modification: change the text of the second fragment
            // in the first cell of the first table (if it exists).
            if (absorber.TableList.Count > 0 &&
                absorber.TableList[0].RowList.Count > 0 &&
                absorber.TableList[0].RowList[0].CellList.Count > 0 &&
                absorber.TableList[0].RowList[0].CellList[0].TextFragments.Count > 1)
            {
                TextFragment fragment = absorber.TableList[0].RowList[0].CellList[0].TextFragments[1];
                fragment.Text = "hi world";
            }

            // Save the (potentially modified) document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}