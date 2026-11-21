using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Added for TableAbsorber

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

        // Open the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least four pages
            if (doc.Pages.Count < 4)
            {
                for (int i = doc.Pages.Count + 1; i <= 4; i++)
                    doc.Pages.Add();
            }

            // -------------------------------------------------
            // Create a new Table with three columns
            // -------------------------------------------------
            Table newTable = new Table();
            newTable.ColumnWidths = "100 150 200";

            // Add a header row
            Row header = newTable.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add a first data row
            Row row1 = newTable.Rows.Add();
            row1.Cells.Add("Row1 Col1");
            row1.Cells.Add("Row1 Col2");
            row1.Cells.Add("Row1 Col3");

            // Add a second data row
            Row row2 = newTable.Rows.Add();
            row2.Cells.Add("Row2 Col1");
            row2.Cells.Add("Row2 Col2");
            row2.Cells.Add("Row2 Col3");

            // -------------------------------------------------
            // Replace the existing table on page 4 (if any)
            // -------------------------------------------------
            Page pageFour = doc.Pages[4]; // 1‑based indexing

            // Use TableAbsorber to locate tables on the page
            var absorber = new TableAbsorber();
            absorber.Visit(pageFour);

            if (absorber.TableList.Count > 0)
            {
                // Replace the first found table with the new one, preserving its position
                var oldTable = absorber.TableList[0];
                absorber.Replace(pageFour, oldTable, newTable);
            }
            else
            {
                // No table found – simply add the new table at the end of the page content
                pageFour.Paragraphs.Add(newTable);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
