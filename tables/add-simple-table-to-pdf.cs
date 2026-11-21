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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page; add a new page if the document is empty
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a visual table
            Table table = new Table
            {
                // Define three equal columns
                ColumnWidths = "100 100 100",
                // Optional: add a thin border around the table
                Border = new BorderInfo(BorderSide.All, 0.5f),
                // Optional: set margins inside the page
                Margin = new MarginInfo(10, 10, 10, 10)
            };

            // Add a single row
            Row row = table.Rows.Add();

            // Add three cells with texts "A", "B", "C"
            Cell cellA = row.Cells.Add();
            cellA.Paragraphs.Add(new TextFragment("A"));

            Cell cellB = row.Cells.Add();
            cellB.Paragraphs.Add(new TextFragment("B"));

            Cell cellC = row.Cells.Add();
            cellC.Paragraphs.Add(new TextFragment("C"));

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}