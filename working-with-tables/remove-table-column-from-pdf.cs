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
        const int pageNumber = 1;               // 1‑based page index
        const int columnIndexToRemove = 1;      // zero‑based column index to delete

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Locate tables on the specified page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[pageNumber]);

            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found on the page.");
                doc.Save(outputPath);
                return;
            }

            // Work with the first detected table
            AbsorbedTable oldTable = absorber.TableList[0];

            // Create a new table that will replace the old one
            Table newTable = new Table();

            // Optional: set column widths (example with equal widths)
            // newTable.ColumnWidths = "100 100 100";

            // Rebuild rows without the unwanted column
            foreach (AbsorbedRow absorbedRow in oldTable.RowList)
            {
                Row newRow = newTable.Rows.Add();

                for (int i = 0; i < absorbedRow.CellList.Count; i++)
                {
                    if (i == columnIndexToRemove)
                        continue; // skip the column to be removed

                    // Concatenate all text fragments inside the cell
                    string cellText = string.Empty;
                    foreach (TextFragment tf in absorbedRow.CellList[i].TextFragments)
                        cellText += tf.Text;

                    newRow.Cells.Add(cellText);
                }
            }

            // Replace the original table with the newly built table
            absorber.Replace(doc.Pages[pageNumber], oldTable, newTable);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Column removed and saved to '{outputPath}'.");
    }
}