using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the table will be inserted (first page in this example)
            Page page = doc.Pages[1];

            // Create a new Table instance
            Table table = new Table();

            // Optional: set table position and width (full page width with some margins)
            table.Margin = new MarginInfo { Top = 20, Bottom = 20, Left = 20, Right = 20 };
            // In recent Aspose.Pdf versions ColumnAdjustment.AutoFit is not available; the default behaviour is sufficient.
            // table.ColumnAdjustment = ColumnAdjustment.AutoFit; // removed

            // Define column widths – the property expects a space‑separated string, not a collection.
            table.ColumnWidths = "33 33 34"; // three columns with relative widths

            // ---------- Heading Row ----------
            // Add a row for the heading
            Row headingRow = table.Rows.Add();

            // Create cells for the heading row
            // Cell 1
            Cell cell1 = headingRow.Cells.Add();
            TextFragment tf1 = new TextFragment("Product");
            tf1.TextState.FontSize = 12;
            tf1.TextState.FontStyle = FontStyles.Bold;
            tf1.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            cell1.Paragraphs.Add(tf1);

            // Cell 2
            Cell cell2 = headingRow.Cells.Add();
            TextFragment tf2 = new TextFragment("Quantity");
            tf2.TextState.FontSize = 12;
            tf2.TextState.FontStyle = FontStyles.Bold;
            tf2.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            cell2.Paragraphs.Add(tf2);

            // Cell 3
            Cell cell3 = headingRow.Cells.Add();
            TextFragment tf3 = new TextFragment("Price");
            tf3.TextState.FontSize = 12;
            tf3.TextState.FontStyle = FontStyles.Bold;
            tf3.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            cell3.Paragraphs.Add(tf3);

            // ---------- Data Rows ----------
            // Example data rows (you can add as many as needed)
            string[,] data = {
                { "Widget A", "10", "$15.00" },
                { "Widget B", "5",  "$25.00" },
                { "Widget C", "20", "$8.00" }
            };

            for (int i = 0; i < data.GetLength(0); i++)
            {
                Row dataRow = table.Rows.Add();

                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Cell dataCell = dataRow.Cells.Add();
                    TextFragment tf = new TextFragment(data[i, j]);
                    tf.TextState.FontSize = 11;
                    tf.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGray;
                    dataCell.Paragraphs.Add(tf);
                }
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted and saved to '{outputPath}'.");
    }
}
