using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
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

            // Create a TextFragment with the desired text
            TextFragment tf = new TextFragment("Sample Text");

            // Set the font and size for the fragment
            tf.TextState.Font = FontRepository.FindFont("TimesNewRoman");
            tf.TextState.FontSize = 12;

            // Ensure the cell respects the TextFragment's TextState
            cell.IsOverrideByFragment = true;

            // Add the TextFragment to the cell's paragraphs collection
            cell.Paragraphs.Add(tf);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with styled cell text created successfully.");
    }
}