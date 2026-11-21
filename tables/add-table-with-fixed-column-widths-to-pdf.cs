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
            // Create a table with specified column widths and auto-fit adjustment
            Table table = new Table
            {
                ColumnWidths = "100 200 300",
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent
            };

            // Add a single row
            Row row = table.Rows.Add();

            // Populate cells with text fragments
            Cell cell1 = row.Cells.Add();
            cell1.Paragraphs.Add(new TextFragment("Cell 1"));

            Cell cell2 = row.Cells.Add();
            cell2.Paragraphs.Add(new TextFragment("Cell 2"));

            Cell cell3 = row.Cells.Add();
            cell3.Paragraphs.Add(new TextFragment("Cell 3"));

            // Insert the table into the first page of the document
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}