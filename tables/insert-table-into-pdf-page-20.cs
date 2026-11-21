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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 20 pages
            if (doc.Pages.Count < 20)
            {
                Console.Error.WriteLine("The document has fewer than 20 pages.");
                return;
            }

            // Create a new Table element
            Table table = new Table
            {
                // ColumnWidths expects a string with space‑separated values (in points)
                ColumnWidths = "100 150 100"
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 1") } });
            header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 2") } });
            header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 3") } });

            // Add a data row
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Row 1, Col 1") } });
            dataRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Row 1, Col 2") } });
            dataRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Row 1, Col 3") } });

            // Add another data row (example)
            Row dataRow2 = table.Rows.Add();
            dataRow2.Cells.Add(new Cell { Paragraphs = { new TextFragment("Row 2, Col 1") } });
            dataRow2.Cells.Add(new Cell { Paragraphs = { new TextFragment("Row 2, Col 2") } });
            dataRow2.Cells.Add(new Cell { Paragraphs = { new TextFragment("Row 2, Col 3") } });

            // Access page 20 (1‑based indexing)
            Page page20 = doc.Pages[20];

            // Remove existing content on page 20 (optional: keep other elements)
            page20.Paragraphs.Clear();

            // Add the newly created table to page 20
            page20.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted on page 20 and saved to '{outputPath}'.");
    }
}
