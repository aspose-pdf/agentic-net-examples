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
            // Verify that the document has at least 23 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 23)
            {
                Console.Error.WriteLine("The document contains fewer than 23 pages.");
                return;
            }

            // Create a TableAbsorber and visit page 23
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[23]);

            // If no tables were detected, inform the user
            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found on page 23.");
            }
            else
            {
                // Retrieve the first absorbed table
                AbsorbedTable absorbedTable = absorber.TableList[0];

                // Build a new Table that will replace the absorbed one
                Table newTable = new Table();
                Row newRow = new Row();

                // First cell
                Cell cell1 = new Cell();
                cell1.Paragraphs.Add(new TextFragment("New Cell 1"));
                // Second cell
                Cell cell2 = new Cell();
                cell2.Paragraphs.Add(new TextFragment("New Cell 2"));

                // Add cells to the row
                newRow.Cells.Add(cell1);
                newRow.Cells.Add(cell2);

                // Add the row to the table
                newTable.Rows.Add(newRow);

                // Replace the original table with the new one (note the required Page argument)
                absorber.Replace(doc.Pages[23], absorbedTable, newTable);

                Console.WriteLine("Added a new row with two cells to the first table on page 23.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
