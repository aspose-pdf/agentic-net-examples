using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ReorderTableColumns
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "reordered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Desired column order (zero‑based indices). Adjust as needed.
        int[] newOrder = { 2, 0, 1 }; // example: move column 3 to first, then 1, then 2

        using (Document doc = new Document(inputPath))
        {
            // Process each page that may contain tables
            foreach (Page page in doc.Pages)
            {
                // Work on a copy of the paragraph list to avoid modification issues
                var paragraphs = page.Paragraphs.ToArray();

                for (int i = 0; i < paragraphs.Length; i++)
                {
                    if (paragraphs[i] is Table oldTable)
                    {
                        // Create a new table that will hold columns in the new order
                        Table newTable = new Table();

                        // Preserve original column widths if they are defined
                        if (!string.IsNullOrEmpty(oldTable.ColumnWidths))
                        {
                            newTable.ColumnWidths = oldTable.ColumnWidths;
                        }
                        else
                        {
                            // If no explicit widths, keep the same column count so Aspose can auto‑size
                            newTable.ColumnAdjustment = ColumnAdjustment.AutoFitToContent;
                        }

                        // Rebuild each row with cells reordered
                        foreach (Row oldRow in oldTable.Rows)
                        {
                            Row newRow = new Row();

                            foreach (int colIdx in newOrder)
                            {
                                // Guard against out‑of‑range indices
                                if (colIdx < 0 || colIdx >= oldRow.Cells.Count)
                                    continue;

                                Cell oldCell = oldRow.Cells[colIdx];
                                Cell newCell = new Cell();

                                // Copy all paragraphs (including TextFragment, FormattedText, etc.)
                                foreach (var paragraph in oldCell.Paragraphs)
                                {
                                    // Simple shallow copy – most paragraph types are clone‑able via constructor
                                    // For TextFragment we recreate it to keep the text content
                                    if (paragraph is TextFragment tf)
                                    {
                                        newCell.Paragraphs.Add(new TextFragment(tf.Text));
                                    }
                                    else
                                    {
                                        // For other paragraph types (e.g., FormattedText) we add the original instance
                                        // because they are immutable for our purpose.
                                        newCell.Paragraphs.Add(paragraph);
                                    }
                                }

                                newRow.Cells.Add(newCell);
                            }

                            newTable.Rows.Add(newRow);
                        }

                        // Replace the old table with the newly built one in the page's paragraph collection
                        page.Paragraphs[i] = newTable;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}
