using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the existing PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a new table
            Table table = new Table();

            // Optional: set table position and width
            table.Left = 50;
            table.Top  = 700;
            // Use a valid ColumnAdjustment enum value
            table.ColumnAdjustment = ColumnAdjustment.AutoFitToContent;

            // Define number of rows and columns (example values)
            int rowCount    = 10; // total rows to create
            int columnCount = 4;  // total columns (first column will hold numbers)

            // Populate the table
            for (int i = 0; i < rowCount; i++)
            {
                // Add a new row
                Row row = table.Rows.Add();

                // First cell: auto‑numbered value (1‑based)
                Cell numberCell = row.Cells.Add();
                numberCell.Paragraphs.Add(new TextFragment((i + 1).ToString()));

                // Remaining cells: placeholder text
                for (int j = 1; j < columnCount; j++)
                {
                    Cell cell = row.Cells.Add();
                    cell.Paragraphs.Add(new TextFragment($"R{i + 1}C{j + 1}"));
                }
            }

            // Add the table to the first page of the document
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF using the provided save rule (inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with auto‑numbered column saved to '{outputPath}'.");
    }
}
