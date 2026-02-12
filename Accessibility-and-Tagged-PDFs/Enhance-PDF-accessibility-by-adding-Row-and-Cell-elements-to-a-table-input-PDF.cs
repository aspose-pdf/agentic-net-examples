using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // NOTE: The 'Tagged' property is not available in recent Aspose.Pdf versions.
            // Tagging is automatically handled via the TaggedContent API, so we omit the line.

            // Process each page (example uses the first page)
            Page page = pdfDocument.Pages[1];

            // Locate tables on the page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page);

            // If no tables are found, just save the original document
            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables detected on the page.");
                pdfDocument.Save(outputPath);
                return;
            }

            // Iterate over all detected tables
            foreach (AbsorbedTable absorbedTable in absorber.TableList)
            {
                // Create a new visual Table that will replace the absorbed one
                Table newTable = new Table
                {
                    // Position the new table at the same location as the original (cast to float)
                    Left = (float)absorbedTable.Rectangle.LLX,
                    Top = (float)absorbedTable.Rectangle.URY
                };

                // Determine column count from the first row of the absorbed table
                int columnCount = absorbedTable.RowList[0].CellList.Count;

                // Compute equal column widths based on the original table rectangle
                double totalWidth = absorbedTable.Rectangle.URX - absorbedTable.Rectangle.LLX;
                double colWidth = totalWidth / columnCount;
                string[] widths = new string[columnCount];
                for (int i = 0; i < columnCount; i++)
                    widths[i] = colWidth.ToString();

                // Set column widths (space‑separated string)
                newTable.ColumnWidths = string.Join(" ", widths);

                // Build rows and cells, copying text fragments from the original cells
                foreach (AbsorbedRow absorbedRow in absorbedTable.RowList)
                {
                    Row row = newTable.Rows.Add();
                    foreach (AbsorbedCell absorbedCell in absorbedRow.CellList)
                    {
                        Cell cell = row.Cells.Add();

                        // Transfer each text fragment from the original cell
                        foreach (TextFragment tf in absorbedCell.TextFragments)
                        {
                            cell.Paragraphs.Add(new TextFragment(tf.Text));
                        }
                    }
                }

                // Replace the original visual table with the newly created one
                absorber.Replace(page, absorbedTable, newTable);

                // ---------- Add logical structure (Row and Cell elements) ----------
                var tagged = pdfDocument.TaggedContent; // use var to avoid missing type reference

                // Create a TableElement and attach it to the document root
                TableElement tableElement = tagged.CreateTableElement();
                tagged.RootElement.AppendChild(tableElement, true);

                // Create a table body (TBody) element
                TableTBodyElement tbody = tableElement.CreateTBody();
                tableElement.AppendChild(tbody, true);

                // For each visual row, create a TR element and corresponding TD elements
                foreach (Row visualRow in newTable.Rows)
                {
                    TableTRElement tr = tbody.CreateTR();
                    tbody.AppendChild(tr, true);

                    foreach (Cell visualCell in visualRow.Cells)
                    {
                        TableTDElement td = tr.CreateTD();
                        tr.AppendChild(td, true);

                        // Concatenate all text fragments inside the cell to set ActualText
                        string cellText = "";
                        foreach (var para in visualCell.Paragraphs)
                        {
                            if (para is TextFragment tf)
                                cellText += tf.Text;
                        }
                        td.ActualText = cellText;
                    }
                }
            }

            // Save the modified PDF (uses the provided document-save rule)
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Accessibility‑enhanced PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
