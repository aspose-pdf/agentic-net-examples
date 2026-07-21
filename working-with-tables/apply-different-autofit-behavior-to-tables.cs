using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // existing PDF to modify
        const string outputPath = "output.pdf";  // result PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the recommended lifecycle pattern)
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to add tables to
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // ------------------------------------------------------------
            // Table 1 – AutoFit to content (ColumnAdjustment.AutoFitToContent)
            // ------------------------------------------------------------
            Table tableContentFit = new Table
            {
                // Position the table on the page
                Left = 50,
                Top  = 500,
                // Apply AutoFit to content behavior
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent,
                // Optional visual styling
                Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black)
            };

            // Add a header row
            Row headerRow1 = tableContentFit.Rows.Add();
            headerRow1.Cells.Add("Header 1");
            headerRow1.Cells.Add("Header 2");

            // Add a data row
            Row dataRow1 = tableContentFit.Rows.Add();
            dataRow1.Cells.Add("Short");
            dataRow1.Cells.Add("A much longer piece of text that should expand the column width");

            // Add the table to the page
            page.Paragraphs.Add(tableContentFit);

            // ------------------------------------------------------------
            // Table 2 – AutoFit to window (ColumnAdjustment.AutoFitToWindow)
            // ------------------------------------------------------------
            Table tableWindowFit = new Table
            {
                Left = 50,
                Top  = 300,
                // Apply AutoFit to window behavior
                ColumnAdjustment = ColumnAdjustment.AutoFitToWindow,
                // Optional visual styling
                Border = new BorderInfo(BorderSide.All, 0.5f, Color.DarkGray)
            };

            // Add a header row
            Row headerRow2 = tableWindowFit.Rows.Add();
            headerRow2.Cells.Add("Column A");
            headerRow2.Cells.Add("Column B");
            headerRow2.Cells.Add("Column C");

            // Add a data row
            Row dataRow2 = tableWindowFit.Rows.Add();
            dataRow2.Cells.Add("Data 1");
            dataRow2.Cells.Add("Data 2");
            dataRow2.Cells.Add("Data 3");

            // Add the second table to the same page (below the first one)
            page.Paragraphs.Add(tableWindowFit);

            // Save the modified PDF (standard Save without extra options is fine for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}