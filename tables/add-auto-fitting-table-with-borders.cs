using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // for ColumnAdjustment
using Aspose.Pdf.Drawing; // for Color (Aspose.Pdf.Color is also in Aspose.Pdf, but keep explicit)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a table and configure its layout
            Table table = new Table
            {
                // Adjust column widths automatically based on cell content
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent,

                // Set a solid border around the whole table using the constructor that accepts color
                Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black),

                // Set a solid border for each cell using the same constructor
                DefaultCellBorder = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black)
            };

            // Add a header row
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Product");
            headerRow.Cells.Add("Quantity");
            headerRow.Cells.Add("Price");

            // Add a data row
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Widget A");
            dataRow.Cells.Add("10");
            dataRow.Cells.Add("$25.00");

            // Insert the table into the first page's paragraph collection
            Page firstPage = doc.Pages[1];
            firstPage.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with table to '{outputPath}'.");
    }
}
