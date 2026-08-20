using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for BorderSide enum if needed (actually BorderSide is in Aspose.Pdf namespace)

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

        // Load the existing PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a new table (lifecycle rule: use default constructor)
            Aspose.Pdf.Table table = new Aspose.Pdf.Table
            {
                // Apply a solid black border of 1 point to all sides
                Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black),

                // Optional: include border width in column calculations
                IsBordersIncluded = true,

                // Set some basic layout properties
                ColumnWidths = "100 100 100", // three columns of equal width
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Cell A1");
            data.Cells.Add("Cell A2");
            data.Cells.Add("Cell A3");

            // Position the table on the first page
            Page page = doc.Pages[1];
            page.Paragraphs.Add(table);

            // Save the modified PDF (lifecycle rule: use Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with solid border saved to '{outputPath}'.");
    }
}