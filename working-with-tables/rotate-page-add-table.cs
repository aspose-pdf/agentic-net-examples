using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based
            Page page = doc.Pages[1];

            // Apply a 90° clockwise rotation to the page using the correct property
            page.Rotate = Rotation.on90; // valid values: Rotation.None, Rotation.on90, Rotation.on180, Rotation.on270

            // Create a simple 2‑column table
            Table table = new Table
            {
                // Define column widths (in points). Adjust as needed.
                ColumnWidths = "150 150"
            };

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 1") } });
            header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 2") } });

            // Data row
            Row data = table.Rows.Add();
            data.Cells.Add(new Cell { Paragraphs = { new TextFragment("Cell A") } });
            data.Cells.Add(new Cell { Paragraphs = { new TextFragment("Cell B") } });

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF (Document.Save without SaveOptions writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
