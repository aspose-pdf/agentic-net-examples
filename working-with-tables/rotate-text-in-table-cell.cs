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
            using (Document doc = new Document(inputPath))
            {
                // Ensure there is at least one page to place the table on
                Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

                // Create a table with a single column (adjust widths as needed)
                Table table = new Table();
                table.ColumnWidths = "200"; // width of the single column

                // ---------- Header Row ----------
                Row headerRow = table.Rows.Add();
                Cell headerCell = headerRow.Cells.Add("Header");
                // Optional styling for header
                headerCell.BackgroundColor = Color.LightGray;
                headerCell.DefaultCellTextState = new TextState
                {
                    FontSize = 12,
                    FontStyle = FontStyles.Bold
                };

                // ---------- Body Row with Rotated Text ----------
                Row bodyRow = table.Rows.Add();
                Cell bodyCell = bodyRow.Cells.Add(); // add an empty cell first
                // Create a TextFragment, set its rotation, and add it to the cell
                TextFragment rotatedFragment = new TextFragment("Rotated Text");
                rotatedFragment.TextState.Rotation = 45; // rotate 45 degrees
                rotatedFragment.TextState.FontSize = 12;
                bodyCell.Paragraphs.Add(rotatedFragment);

                // Add the table to the page
                page.Paragraphs.Add(table);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with rotated cell text to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
