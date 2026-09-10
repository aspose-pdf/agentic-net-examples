using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ReplaceTableExample
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
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Find all tables on the current page
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(page);

                // Work on a copy of the TableList because Replace modifies the collection
                var absorbedTables = absorber.TableList.ToList();

                foreach (AbsorbedTable oldTable in absorbedTables)
                {
                    // Create a new Table that will replace the old one
                    Table newTable = new Table();

                    // Position the new table at the same rectangle as the old table
                    // Rectangle coordinates: LLX, LLY, URX, URY
                    newTable.Left = (float)oldTable.Rectangle.LLX;
                    newTable.Top  = (float)oldTable.Rectangle.URY;

                    // Optionally copy column widths if they exist – AbsorbedTable does not expose ColumnWidths,
                    // so this step is omitted. You can set custom widths here if needed.

                    // Add a single row with a single cell containing placeholder text
                    Row row = new Row();
                    Cell cell = new Cell();
                    cell.Paragraphs.Add(new TextFragment("Replaced table"));
                    row.Cells.Add(cell);
                    newTable.Rows.Add(row);

                    // Replace the absorbed table with the newly created table
                    absorber.Replace(page, oldTable, newTable);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table replacement completed. Output saved to '{outputPath}'.");
    }
}
