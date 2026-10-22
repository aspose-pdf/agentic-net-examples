using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document doc = new Document(inputPath);

            // Create a table with a single column of width 200 points
            Table table = new Table { ColumnWidths = "200" };

            // Add a row and a cell to the table
            Row row = table.Rows.Add();
            Cell cell = row.Cells.Add();

            // Create a TextFragment and rotate it by 60 degrees
            TextFragment tf = new TextFragment("Rotated Text");
            tf.TextState.FontSize = 12;
            tf.TextState.Rotation = 60; // rotation in degrees

            // Optional: give the cell a small margin so the rotated text is not clipped
            cell.Margin = new MarginInfo { Top = 5, Bottom = 5, Left = 5, Right = 5 };

            // Insert the rotated text into the cell
            cell.Paragraphs.Add(tf);

            // Add the table to the first page of the document
            Page page = doc.Pages[1];
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);

            Console.WriteLine($"PDF saved with rotated text in table cell: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
