using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class RemoveTableColumn
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int columnIndexToRemove = 1; // zero‑based index of the column to delete

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Find tables on the first page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[1]);

            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found on the first page.");
                doc.Save(outputPath);
                return;
            }

            // Work with the first detected table
            var absorbedTable = absorber.TableList[0];

            // Build a new Table without the unwanted column
            Table newTable = new Table();

            foreach (var absorbedRow in absorbedTable.RowList)
            {
                Row newRow = new Row();

                for (int i = 0; i < absorbedRow.CellList.Count; i++)
                {
                    // Skip the column that must be removed
                    if (i == columnIndexToRemove)
                        continue;

                    var absorbedCell = absorbedRow.CellList[i];
                    Cell newCell = new Cell();

                    // Concatenate all text fragments of the original cell
                    string cellText = string.Empty;
                    foreach (var fragment in absorbedCell.TextFragments)
                        cellText += fragment.Text;

                    // Add the text to the new cell as a TextFragment
                    if (!string.IsNullOrEmpty(cellText))
                        newCell.Paragraphs.Add(new TextFragment(cellText));

                    newRow.Cells.Add(newCell);
                }

                newTable.Rows.Add(newRow);
            }

            // Replace the original table with the modified one
            absorber.Replace(doc.Pages[1], absorbedTable, newTable);

            // Save the updated document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Column {columnIndexToRemove} removed and saved to '{outputPath}'.");
    }
}