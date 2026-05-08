using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // for TextFragment
using Aspose.Pdf.LogicalStructure; // not required for core Table, but kept for completeness

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output_mincolwidth.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a new table and set a minimum column width (in points)
            Table table = new Table
            {
                // DefaultColumnWidth is applied to any column that does not have an explicit width.
                // Setting it to 50 points ensures no column becomes narrower than 50 points.
                DefaultColumnWidth = "50"
            };

            // Optionally define explicit widths for each column.
            // The widths are space‑separated; here we define three columns.
            table.ColumnWidths = "50 70 90";

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 1") } });
            header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 2") } });
            header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 3") } });

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add(new Cell { Paragraphs = { new TextFragment("Data A") } });
            data.Cells.Add(new Cell { Paragraphs = { new TextFragment("Data B") } });
            data.Cells.Add(new Cell { Paragraphs = { new TextFragment("Data C") } });

            // Insert the table into the first page of the document
            Page firstPage = doc.Pages[1]; // 1‑based indexing
            firstPage.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with minimum column width to '{outputPath}'.");
    }
}