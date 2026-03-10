using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string outputPath = "table_example.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Table instance (visual table) and configure its appearance
            Table table = new Table
            {
                // Optional: set table position on the page
                Left = 50,
                Top = 700,
                // Optional: set background color and border
                BackgroundColor = Aspose.Pdf.Color.LightGray,
                Border = new BorderInfo(BorderSide.All, 1, Aspose.Pdf.Color.Black)
            };

            // Define column widths (in points). Here we create three equal columns.
            table.ColumnWidths = "150 150 150";

            // Add a header row
            Row headerRow = table.Rows.Add();
            // Header cells (TH) – use TextFragment with bold style
            TextFragment th1 = new TextFragment("Product")
            {
                TextState = { FontSize = 12, FontStyle = FontStyles.Bold }
            };
            headerRow.Cells.Add(th1);

            TextFragment th2 = new TextFragment("Quantity")
            {
                TextState = { FontSize = 12, FontStyle = FontStyles.Bold }
            };
            headerRow.Cells.Add(th2);

            TextFragment th3 = new TextFragment("Price")
            {
                TextState = { FontSize = 12, FontStyle = FontStyles.Bold }
            };
            headerRow.Cells.Add(th3);

            // Add data rows
            string[,] data = {
                { "Widget A", "10", "$5.00" },
                { "Widget B", "7",  "$8.50" },
                { "Widget C", "15", "$3.20" }
            };

            for (int i = 0; i < data.GetLength(0); i++)
            {
                Row dataRow = table.Rows.Add();
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    TextFragment tf = new TextFragment(data[i, j])
                    {
                        TextState = { FontSize = 11 }
                    };
                    dataRow.Cells.Add(tf);
                }
            }

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF document (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}