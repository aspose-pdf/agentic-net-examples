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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a table with three columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100", // equal column widths
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Add five rows to the table
            for (int i = 1; i <= 5; i++)
            {
                // Row is the correct type (Aspose.Pdf.Row)
                Row row = table.Rows.Add();

                // Add three cells per row with simple text
                for (int j = 1; j <= 3; j++)
                {
                    // Cell is the correct type (Aspose.Pdf.Cell)
                    Cell cell = row.Cells.Add();
                    // Paragraphs collection expects BaseParagraph; TextFragment derives from it
                    cell.Paragraphs.Add(new TextFragment($"R{i}C{j}"));
                }

                // Force a new page after the fourth row
                if (i == 4)
                {
                    row.IsInNewPage = true; // Row has the IsInNewPage property
                }
            }

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}
