using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Locate tables on the first page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[1]);

            // Ensure at least one table was found
            if (absorber.TableList.Count == 0)
            {
                Console.Error.WriteLine("No tables found on the first page.");
                return;
            }

            // The table we want to modify
            AbsorbedTable originalTable = absorber.TableList[0];

            // Create a new table that will replace the original one
            Table newTable = new Table();

            // Define column widths (space‑separated values). Adjust as needed.
            newTable.ColumnWidths = "150 150";

            // Example: add a header row
            Row headerRow = newTable.Rows.Add();
            Cell headerCell1 = headerRow.Cells.Add();
            headerCell1.Paragraphs.Add(new TextFragment("Header 1"));
            Cell headerCell2 = headerRow.Cells.Add();
            headerCell2.Paragraphs.Add(new TextFragment("Header 2"));

            // Example: add a data row
            Row dataRow = newTable.Rows.Add();
            Cell dataCell1 = dataRow.Cells.Add();
            dataCell1.Paragraphs.Add(new TextFragment("Data A"));
            Cell dataCell2 = dataRow.Cells.Add();
            dataCell2.Paragraphs.Add(new TextFragment("Data B"));

            // Add more rows/cells as required
            // ...

            // Replace the original table with the newly built table
            absorber.Replace(doc.Pages[1], originalTable, newTable);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table updated and saved to '{outputPath}'.");
    }
}