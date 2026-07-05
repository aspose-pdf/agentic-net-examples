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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Find all tables on the page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page);

            // If no tables are present, just save the original document
            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found on the page.");
                doc.Save(outputPath);
                return;
            }

            // Take the first absorbed table as the one to replace
            AbsorbedTable oldTable = absorber.TableList[0];

            // Create a new Table that will occupy the same rectangle as the old one
            Aspose.Pdf.Table newTable = new Aspose.Pdf.Table
            {
                // Set a visible border
                Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black)
            };

            // Position the new table using the rectangle of the old table (cast to float because Table.Left/Top expect float)
            newTable.Left = (float)oldTable.Rectangle.LLX; // left coordinate
            newTable.Top = (float)oldTable.Rectangle.URY;  // top coordinate (upper‑right Y)

            // Define column widths (example: split the old table width into two equal columns)
            double tableWidth = oldTable.Rectangle.URX - oldTable.Rectangle.LLX;
            newTable.ColumnWidths = $"{tableWidth / 2} {tableWidth / 2}";

            // Add a single row with two cells containing sample text
            Row row = newTable.Rows.Add();
            row.Cells.Add("New Cell 1");
            row.Cells.Add("New Cell 2");

            // Replace the absorbed table with the newly created table
            absorber.Replace(page, oldTable, newTable);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table replaced and saved to '{outputPath}'.");
    }
}
