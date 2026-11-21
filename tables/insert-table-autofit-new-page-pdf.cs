using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a new table
            Table table = new Table();

            // Adjust columns to fit the window width
            table.ColumnAdjustment = ColumnAdjustment.AutoFitToWindow;

            // Place the table on a new page
            table.IsInNewPage = true;

            // Add a simple header row with two cells
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            // Add a data row with two cells
            Row data = table.Rows.Add();
            data.Cells.Add("Cell A");
            data.Cells.Add("Cell B");

            // Insert the table into the first page's paragraph collection
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}