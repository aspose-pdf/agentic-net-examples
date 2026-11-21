using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_cell.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a table with two columns
            Table table = new Table
            {
                ColumnWidths = "200 200", // set column widths
                Border = new BorderInfo(BorderSide.All, 1f) // optional border for visibility
            };

            // Add a single row to the table
            Row row = table.Rows.Add();

            // First cell with normal orientation
            Cell cell1 = row.Cells.Add("Normal");

            // Second cell with rotated text
            Cell cell2 = row.Cells.Add();
            // Create a TextFragment that will hold the rotated text
            TextFragment rotatedFragment = new TextFragment("Rotated");
            // Define basic text appearance (modify the existing TextState instance)
            rotatedFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            rotatedFragment.TextState.FontSize = 12F;
            rotatedFragment.TextState.ForegroundColor = Color.Black;
            // Rotate the text inside the cell (45 degrees)
            rotatedFragment.TextState.Rotation = 45;
            // Add the fragment to the cell
            cell2.Paragraphs.Add(rotatedFragment);

            // Add the table to the first page of the document
            Page page = doc.Pages[1];
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
