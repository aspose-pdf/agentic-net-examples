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

        // Open the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Create a new table
            Table table = new Table
            {
                // Define column widths (in points). Adjust as needed.
                ColumnWidths = "120 180 150"
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.BackgroundColor = Color.LightGray; // use Aspose.Pdf.Color for cross‑platform compatibility
            header.Cells.Add(new TextFragment("Header 1"));
            header.Cells.Add(new TextFragment("Header 2"));
            header.Cells.Add(new TextFragment("Header 3"));

            // Add a few data rows
            for (int i = 1; i <= 5; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add(new TextFragment($"Row {i} - Col 1"));
                row.Cells.Add(new TextFragment($"Row {i} - Col 2"));
                row.Cells.Add(new TextFragment($"Row {i} - Col 3"));
            }

            // Replace any existing content on page 3 with the new table
            Page pageThree = doc.Pages[3]; // 1‑based indexing
            pageThree.Paragraphs.Clear();   // remove existing paragraphs (including any previous tables)
            pageThree.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
