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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Search for tables on the first page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(pdfDocument.Pages[1]);

            if (absorber.TableList.Count == 0)
            {
                // No table found – create a new one and add it to the page
                Table newTable = CreateTableWithInsertedRow();
                pdfDocument.Pages[1].Paragraphs.Add(newTable);
            }
            else
            {
                // Take the first detected table
                AbsorbedTable absorbedTable = absorber.TableList[0];

                // Build a new table (could copy existing content; omitted for brevity)
                Table newTable = CreateTableWithInsertedRow();

                // Replace the absorbed table with the new table
                absorber.Replace(pdfDocument.Pages[1], absorbedTable, newTable);
            }

            // Save the modified PDF
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Creates a table containing an additional row with two cells
    private static Table CreateTableWithInsertedRow()
    {
        Table table = new Table
        {
            // Define two equal column widths (points)
            ColumnWidths = "200 200"
        };

        // Add a new row
        Row row = new Row();

        // First cell with sample text
        Cell cell1 = row.Cells.Add();
        cell1.Paragraphs.Add(new TextFragment("Inserted Cell 1"));

        // Second cell with sample text
        Cell cell2 = row.Cells.Add();
        cell2.Paragraphs.Add(new TextFragment("Inserted Cell 2"));

        // Append the row to the table
        table.Rows.Add(row);

        return table;
    }
}