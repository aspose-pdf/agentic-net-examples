using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Retrieve tables on the first page using TableAbsorber
            // ------------------------------------------------------------
            Aspose.Pdf.Text.TableAbsorber absorber = new Aspose.Pdf.Text.TableAbsorber();
            absorber.Visit(doc.Pages[1]); // Pages are 1‑based

            // Output information about found tables
            Console.WriteLine($"Tables found on page 1: {absorber.TableList.Count}");
            for (int t = 0; t < absorber.TableList.Count; t++)
            {
                var table = absorber.TableList[t];
                Console.WriteLine($" Table {t + 1}: {table.RowList.Count} rows, {table.RowList[0].CellList.Count} columns");
                for (int r = 0; r < table.RowList.Count; r++)
                {
                    var row = table.RowList[r];
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];
                        // Each cell may contain multiple text fragments; concatenate them
                        string cellText = "";
                        foreach (Aspose.Pdf.Text.TextFragment frag in cell.TextFragments)
                            cellText += frag.Text;
                        Console.WriteLine($"  Row {r + 1}, Col {c + 1}: \"{cellText}\"");
                    }
                }
            }

            // ------------------------------------------------------------
            // 2. Modify the first cell of the first table (if any)
            // ------------------------------------------------------------
            if (absorber.TableList.Count > 0)
            {
                var firstTable = absorber.TableList[0];
                if (firstTable.RowList.Count > 0 && firstTable.RowList[0].CellList.Count > 0)
                {
                    var firstCell = firstTable.RowList[0].CellList[0];
                    if (firstCell.TextFragments.Count > 0)
                    {
                        // Change the text of the first fragment in the cell
                        firstCell.TextFragments[0].Text = "Modified Text";
                        Console.WriteLine("First cell text modified.");
                    }
                }
            }

            // ------------------------------------------------------------
            // 3. Create a new table programmatically
            // ------------------------------------------------------------
            Aspose.Pdf.Table newTable = new Aspose.Pdf.Table();

            // Define two columns with equal width
            newTable.ColumnWidths = "200 200";

            // Set table border and default cell border using BorderInfo
            newTable.Border = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, 1f, Aspose.Pdf.Color.LightGray);
            newTable.DefaultCellBorder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray);

            // Add a single row (renamed variable to avoid name clash)
            Aspose.Pdf.Row newRow = newTable.Rows.Add();

            // Add two cells with sample text
            Aspose.Pdf.Cell cell1 = newRow.Cells.Add("Hello");
            Aspose.Pdf.Cell cell2 = newRow.Cells.Add("World");

            // Optional: set background color for the row
            newRow.BackgroundColor = Aspose.Pdf.Color.FromRgb(0.9, 0.9, 0.9);

            // ------------------------------------------------------------
            // 4. Replace the first existing table with the new table
            // ------------------------------------------------------------
            if (absorber.TableList.Count > 0)
            {
                var oldTable = absorber.TableList[0];
                // TableAbsorber.Replace replaces the absorbed table on the specified page
                absorber.Replace(doc.Pages[1], oldTable, newTable);
                Console.WriteLine("First table replaced with the new table.");
            }
            else
            {
                // If no table existed, simply add the new table to the page
                doc.Pages[1].Paragraphs.Add(newTable);
                Console.WriteLine("No existing table found; new table added to page 1.");
            }

            // ------------------------------------------------------------
            // 5. Save the modified document (lifecycle rule: use Save inside using)
            // ------------------------------------------------------------
            doc.Save(outputPath);
            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
    }
}
