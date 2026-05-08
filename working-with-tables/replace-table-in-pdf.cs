using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // TableAbsorber resides here
using Aspose.Pdf.Drawing; // For Rectangle if needed

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Assume we work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Find all tables on the page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page);

            // If no tables were found, nothing to replace
            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found on the page.");
                doc.Save(outputPath);
                return;
            }

            // Take the first absorbed table as the one to replace
            AbsorbedTable oldTable = absorber.TableList[0];

            // Create a new Table that will occupy the same rectangle
            Table newTable = new Table();

            // Preserve the original position
            // AbsorbedTable.Rectangle provides LLX, LLY, URX, URY (double)
            Aspose.Pdf.Rectangle rect = oldTable.Rectangle;
            newTable.Left = (float)rect.LLX; // cast double -> float
            newTable.Top  = (float)rect.URY; // cast double -> float

            // Example: define a simple 2×2 table with some content
            newTable.ColumnWidths = "100 100"; // two columns, 100 points each

            // Row 1
            newTable.Rows.Add(new Row());
            newTable.Rows[0].Cells.Add(new Cell { Paragraphs = { new TextFragment("New Cell 1") } });
            newTable.Rows[0].Cells.Add(new Cell { Paragraphs = { new TextFragment("New Cell 2") } });

            // Row 2
            newTable.Rows.Add(new Row());
            newTable.Rows[1].Cells.Add(new Cell { Paragraphs = { new TextFragment("New Cell 3") } });
            newTable.Rows[1].Cells.Add(new Cell { Paragraphs = { new TextFragment("New Cell 4") } });

            // Replace the absorbed table with the new Table.
            // This method updates the internal TableList collection automatically.
            absorber.Replace(page, oldTable, newTable);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table replaced and saved to '{outputPath}'.");
    }
}
