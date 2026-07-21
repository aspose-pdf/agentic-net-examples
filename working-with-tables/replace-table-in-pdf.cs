using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // TableAbsorber resides here

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

        // Open the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Visit each page to collect tables
            foreach (Page page in doc.Pages)
                absorber.Visit(page);

            // Work on a copy of the TableList because Replace modifies the collection
            var absorbedTables = absorber.TableList.Cast<AbsorbedTable>().ToList();

            foreach (AbsorbedTable oldTable in absorbedTables)
            {
                // Retrieve the page that contains the old table
                Page page = doc.Pages[oldTable.PageNum];

                // Create a new Table that will replace the old one
                Table newTable = new Table();

                // Position the new table at the same rectangle as the old table
                // Rectangle: (LLX, LLY) lower‑left, (URX, URY) upper‑right
                newTable.Left = (float)oldTable.Rectangle.LLX;
                newTable.Top  = (float)oldTable.Rectangle.URY;

                // (Optional) copy column widths from the old table if desired
                // newTable.ColumnWidths = oldTable.ColumnWidths;

                // Replace the absorbed table with the new Table
                absorber.Replace(page, oldTable, newTable);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table replacement completed. Saved to '{outputPath}'.");
    }
}
