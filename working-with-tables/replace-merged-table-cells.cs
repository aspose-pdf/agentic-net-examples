using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Assume the table to modify is on the first page
            Aspose.Pdf.Page page = doc.Pages[1];

            // Find tables on the page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page);

            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found on the page.");
                doc.Save(outputPath); // still save unchanged document
                return;
            }

            // Work with the first detected table
            AbsorbedTable oldTable = absorber.TableList[0];

            // Determine total number of logical columns (sum of ColSpan in the first row)
            int totalColumns = 0;
            foreach (AbsorbedCell cell in oldTable.RowList[0].CellList)
                totalColumns += cell.ColSpan;

            // Create a new table that will replace the old one
            Table newTable = new Table();

            // Simple column width distribution (equal widths)
            string columnWidths = string.Empty;
            for (int i = 0; i < totalColumns; i++)
                columnWidths += "100 "; // 100 points per column (adjust as needed)
            newTable.ColumnWidths = columnWidths.Trim();

            // Iterate through each absorbed row and rebuild it
            foreach (var absorbedRow in oldTable.RowList)
            {
                // Add a new row to the replacement table
                Row newRow = newTable.Rows.Add();

                // Process each cell in the absorbed row
                foreach (AbsorbedCell absorbedCell in absorbedRow.CellList)
                {
                    // If the cell spans multiple columns, split it into separate cells
                    if (absorbedCell.ColSpan > 1)
                    {
                        // Extract text from the original merged cell (if any)
                        string mergedText = string.Empty;
                        if (absorbedCell.TextFragments.Count > 0)
                            mergedText = absorbedCell.TextFragments[0].Text;

                        // Create individual cells for each spanned column
                        for (int i = 0; i < absorbedCell.ColSpan; i++)
                        {
                            Cell splitCell = newRow.Cells.Add();

                            // Preserve the original text only in the first split cell
                            if (i == 0 && !string.IsNullOrEmpty(mergedText))
                                splitCell.Paragraphs.Add(new TextFragment(mergedText));
                        }
                    }
                    else
                    {
                        // Normal (non‑merged) cell – copy its text
                        Cell normalCell = newRow.Cells.Add();

                        if (absorbedCell.TextFragments.Count > 0)
                        {
                            string cellText = absorbedCell.TextFragments[0].Text;
                            normalCell.Paragraphs.Add(new TextFragment(cellText));
                        }
                    }
                }
            }

            // Replace the old table with the newly built one (replace‑table rule)
            absorber.Replace(page, oldTable, newTable);

            // Save the modified document (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}