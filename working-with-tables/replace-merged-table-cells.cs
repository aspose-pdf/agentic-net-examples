using System;
using System.IO;
using System.Collections.Generic;
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Aspose.Pdf.Page page = doc.Pages[pageIndex];

                // Create a TableAbsorber and enable the FlowEngine to obtain ColSpan information
                TableAbsorber absorber = new TableAbsorber
                {
                    UseFlowEngine = true
                };

                // Extract tables from the current page
                absorber.Visit(page);

                // Work on a copy of the TableList to avoid collection modification issues
                List<AbsorbedTable> tables = new List<AbsorbedTable>(absorber.TableList);

                foreach (AbsorbedTable oldTable in tables)
                {
                    // Determine if the table contains any merged (col‑spanned) cells
                    bool hasMergedCell = false;
                    foreach (AbsorbedRow row in oldTable.RowList)
                    {
                        foreach (AbsorbedCell cell in row.CellList)
                        {
                            if (cell.ColSpan > 1)
                            {
                                hasMergedCell = true;
                                break;
                            }
                        }
                        if (hasMergedCell) break;
                    }

                    if (!hasMergedCell)
                        continue; // No merged cells – nothing to replace

                    // Build a new Table that mirrors the old one but with split cells
                    Table newTable = new Table();

                    // Preserve the original table position (cast double → float)
                    Aspose.Pdf.Rectangle rect = oldTable.Rectangle;
                    newTable.Left = (float)rect.LLX;
                    newTable.Top  = (float)rect.URY; // Top is the upper‑right Y coordinate

                    // Recreate rows and cells
                    foreach (AbsorbedRow oldRow in oldTable.RowList)
                    {
                        Row newRow = new Row();

                        foreach (AbsorbedCell oldCell in oldRow.CellList)
                        {
                            // Extract the first text fragment (if any) as the cell content
                            string cellText = string.Empty;
                            if (oldCell.TextFragments.Count > 0)
                                cellText = oldCell.TextFragments[0].Text;

                            int span = oldCell.ColSpan;

                            // If the cell spans multiple columns, create separate cells
                            // each containing the same text. Otherwise create a single cell.
                            for (int i = 0; i < Math.Max(span, 1); i++)
                            {
                                Cell newCell = new Cell();
                                // Add the text to the cell's paragraph collection
                                newCell.Paragraphs.Add(new TextFragment(cellText));
                                // Ensure the cell does not span any columns
                                newCell.ColSpan = 1;
                                newRow.Cells.Add(newCell);
                            }
                        }

                        newTable.Rows.Add(newRow);
                    }

                    // Replace the absorbed table with the newly constructed table using the absorber instance
                    absorber.Replace(page, oldTable, newTable);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
