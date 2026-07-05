using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for TextFragment, FontRepository, TextState

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a table with a single column (width can be adjusted as needed)
            Table table = new Table();
            table.ColumnWidths = "200"; // 200 points width for the column

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Allow the cell to use the TextFragment's TextState for styling
            cell.IsOverrideByFragment = true;

            // Create a TextFragment with the desired text
            TextFragment tf = new TextFragment("Hello Aspose PDF");

            // Set the font and size via the TextState of the fragment
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 14; // specific font size

            // Optional: set text color
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Add the TextFragment to the cell's paragraph collection
            cell.Paragraphs.Add(tf);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document (PDF format by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}