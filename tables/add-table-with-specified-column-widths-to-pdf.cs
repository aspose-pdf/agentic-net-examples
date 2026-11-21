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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to add the table to
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a table with five columns and set the required column widths
            Table table = new Table
            {
                ColumnWidths = "50 100 150 200 250",
                // Optional visual styling
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 1") } });
            header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 2") } });
            header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 3") } });
            header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 4") } });
            header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 5") } });

            // Add a sample data row
            Row data = table.Rows.Add();
            data.Cells.Add(new Cell { Paragraphs = { new TextFragment("A") } });
            data.Cells.Add(new Cell { Paragraphs = { new TextFragment("B") } });
            data.Cells.Add(new Cell { Paragraphs = { new TextFragment("C") } });
            data.Cells.Add(new Cell { Paragraphs = { new TextFragment("D") } });
            data.Cells.Add(new Cell { Paragraphs = { new TextFragment("E") } });

            // Insert the table into the page's content
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}