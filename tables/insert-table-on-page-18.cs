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

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 18 pages
            if (doc.Pages.Count < 18)
            {
                Console.Error.WriteLine("The document does not contain page 18.");
                return;
            }

            // Get page 18 (1‑based indexing)
            Page page = doc.Pages[18];

            // Remove any existing tables on the page using TableAbsorber
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page);
            foreach (var absorbedTable in absorber.TableList)
            {
                absorber.Remove(absorbedTable);
            }

            // Create a new table
            Table table = new Table();

            // Define column widths (space‑separated string)
            table.ColumnWidths = "100 200 150";

            // Apply a solid border to the whole table (width = 1 point, black color)
            table.Border = new BorderInfo(BorderSide.All, 1f, Color.Black);

            // Apply default cell border (width = 1 point, gray color)
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 1f, Color.Gray);

            // Add a row with three cells
            Row row = table.Rows.Add();
            row.Cells.Add(new TextFragment("Cell 1"));
            row.Cells.Add(new TextFragment("Cell 2"));
            row.Cells.Add(new TextFragment("Cell 3"));

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted on page 18 and saved to '{outputPath}'.");
    }
}
