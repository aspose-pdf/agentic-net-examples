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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF
            using (Document doc = new Document(inputPath))
            {
                // Add a new page to host the table
                Page page = doc.Pages.Add();

                // Create a table with a single column (width 200 points)
                Table table = new Table
                {
                    ColumnWidths = "200"
                };

                // Add a row to the table
                Row row = table.Rows.Add();

                // Add a cell to the row
                Cell cell = row.Cells.Add();

                // Create a TextFragment, set its alignment to Center
                TextFragment textFragment = new TextFragment("Centered Text")
                {
                    // HorizontalAlignment is set via TextState
                    TextState = { HorizontalAlignment = HorizontalAlignment.Center }
                };

                // Insert the TextFragment into the cell
                cell.Paragraphs.Add(textFragment);

                // Place the table on the page
                page.Paragraphs.Add(table);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Document saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
