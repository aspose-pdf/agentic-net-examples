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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables on the first page
            TableAbsorber absorber = new TableAbsorber();
            Page page = doc.Pages[1];
            absorber.Visit(page);

            // If no tables are found, just save the original document
            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found on page 1.");
                doc.Save(outputPath);
                return;
            }

            // Get the first absorbed table (the one we will replace)
            AbsorbedTable oldTable = absorber.TableList[0];

            // Create a new Table that will occupy the same rectangle as the old table
            Table newTable = new Table
            {
                // The Rectangle property does not exist on Table in recent Aspose.PDF versions.
                // The Replace method uses the location of the absorbed table, so we only need to set the content.
                ColumnWidths = "100 100" // two columns, each 100 units wide
            };

            // Build a simple 1‑row, 2‑column table with sample text
            Row row = new Row();
            Cell cell1 = new Cell();
            cell1.Paragraphs.Add(new TextFragment("New Cell 1"));
            Cell cell2 = new Cell();
            cell2.Paragraphs.Add(new TextFragment("New Cell 2"));
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            newTable.Rows.Add(row);

            // Replace the absorbed table with the newly created table
            absorber.Replace(page, oldTable, newTable);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table replaced and saved to '{outputPath}'.");
    }
}
