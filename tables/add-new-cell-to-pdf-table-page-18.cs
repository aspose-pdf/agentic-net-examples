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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 18 pages
            if (doc.Pages.Count < 18)
            {
                Console.Error.WriteLine("Document does not contain page 18.");
                return;
            }

            // Use TableAbsorber to extract tables from page 18
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[18]);

            // Proceed only if a table was found
            if (absorber.TableList.Count > 0)
            {
                // Get the first absorbed table
                AbsorbedTable absorbedTable = absorber.TableList[0];

                // Create a new Table object to overlay on the same page
                Table newTable = new Table();

                // Position the new table at the same location as the absorbed table
                // Table.Left uses absolute X coordinate (points from left)
                // Table.Top uses distance from the top of the page, so we convert Y
                newTable.Left = (float)absorbedTable.Rectangle.LLX;
                newTable.Top = (float)(doc.Pages[18].PageInfo.Height - absorbedTable.Rectangle.URY);

                // Define column widths (example: two equal columns)
                newTable.ColumnWidths = "100 100";

                // Add a single row
                Row row = newTable.Rows.Add();

                // Add a new cell with text
                Cell cell = row.Cells.Add();
                cell.Paragraphs.Add(new TextFragment("New Cell Text"));

                // Add the new table to page 18
                doc.Pages[18].Paragraphs.Add(newTable);
            }
            else
            {
                Console.WriteLine("No tables found on page 18.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
