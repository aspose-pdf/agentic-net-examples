using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 13 pages
            if (doc.Pages.Count < 13)
            {
                Console.Error.WriteLine("The document does not contain page 13.");
                return;
            }

            // Get page 13 (Aspose.Pdf uses 1‑based indexing)
            Page page13 = doc.Pages[13];

            // ------------------------------------------------------------
            // Create a new Table with specific column widths
            // ------------------------------------------------------------
            Table newTable = new Table();

            // ColumnWidths is a string containing comma‑ or space‑separated values.
            // Example: "100 200 100" (or "100,200,100")
            newTable.ColumnWidths = "100 200 100";

            // Add a single row and populate its cells
            Row row = newTable.Rows.Add();
            row.Cells.Add("First cell");
            row.Cells.Add("Second cell");
            row.Cells.Add("Third cell");

            // ------------------------------------------------------------
            // Replace the first existing Table on page 13 with the new one
            // ------------------------------------------------------------
            bool replaced = false;
            for (int i = 0; i < page13.Paragraphs.Count; i++)
            {
                // Table objects are stored in the Paragraphs collection
                if (page13.Paragraphs[i] is Table)
                {
                    page13.Paragraphs[i] = newTable; // Replace in‑place
                    replaced = true;
                    break;
                }
            }

            // If no table was found, simply add the new table at the end
            if (!replaced)
            {
                page13.Paragraphs.Add(newTable);
            }

            // Save the modified PDF (still inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
