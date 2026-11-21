using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TableAbsorber resides in Aspose.Pdf.Text namespace

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
            // Visit only page 17 to locate tables on that page
            Page targetPage = doc.Pages[17];
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(targetPage);

            // Iterate over all detected tables on page 17
            foreach (var absorbedTable in absorber.TableList)
            {
                // Build a replacement table (core Table API)
                Table newTable = new Table
                {
                    // Define simple column widths – adjust as needed
                    ColumnWidths = "150 150",
                    // Optional visual styling
                    Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black),
                    DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Gray),
                    DefaultCellPadding = new MarginInfo(4, 4, 4, 4)
                };

                // Add a single row with two placeholder cells
                Row row = newTable.Rows.Add();
                row.Cells.Add("New Cell 1");
                row.Cells.Add("New Cell 2");

                // Replace the absorbed table with the newly created one.
                // The Replace method automatically positions the new table at the same rectangle as the original.
                absorber.Replace(targetPage, absorbedTable, newTable);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
