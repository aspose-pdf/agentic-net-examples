using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // for text handling if needed

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 25 pages
            if (doc.Pages.Count < 25)
            {
                Console.Error.WriteLine("The document has fewer than 25 pages.");
                return;
            }

            // Create a new Table
            Table table = new Table();

            // Set a default cell border (1 point, all sides)
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 1);

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add cells with sample text
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");
            row.Cells.Add("Cell 3");

            // Access page 25 (1‑based indexing)
            Page page25 = doc.Pages[25];

            // Replace existing content on page 25 with the new table
            page25.Paragraphs.Clear();      // remove previous paragraphs/elements
            page25.Paragraphs.Add(table);   // add the table as the sole element

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}