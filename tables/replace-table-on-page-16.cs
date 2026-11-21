using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // for BorderInfo, BorderSide
using Aspose.Pdf; // for Color (Aspose.Pdf.Color)

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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 16 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 16)
            {
                Console.Error.WriteLine("The document contains fewer than 16 pages.");
                return;
            }

            // -------------------------------------------------
            // Create a new Table with a default cell border
            // -------------------------------------------------
            Table newTable = new Table
            {
                // Apply a thin black border to all cells
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };

            // Add a header row
            Row headerRow = new Row();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");
            newTable.Rows.Add(headerRow);

            // Add a data row
            Row dataRow = new Row();
            dataRow.Cells.Add("Cell A1");
            dataRow.Cells.Add("Cell B1");
            newTable.Rows.Add(dataRow);

            // -------------------------------------------------
            // Replace (or insert) the table on page 16
            // -------------------------------------------------
            Page page16 = doc.Pages[16]; // 1‑based index

            // Locate the first existing Table in the page's Paragraphs collection
            int existingTableIndex = -1;
            for (int i = 0; i < page16.Paragraphs.Count; i++)
            {
                if (page16.Paragraphs[i] is Table)
                {
                    existingTableIndex = i;
                    break;
                }
            }

            if (existingTableIndex >= 0)
            {
                // Replace the found table with the new one
                page16.Paragraphs[existingTableIndex] = newTable;
            }
            else
            {
                // No table found; simply add the new table to the page
                page16.Paragraphs.Add(newTable);
            }

            // -------------------------------------------------
            // Save the modified PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}