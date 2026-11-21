using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TableAbsorber resides here

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

        // Open the PDF document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least seven pages (1‑based indexing)
            if (doc.Pages.Count < 7)
            {
                Console.Error.WriteLine("Document contains fewer than 7 pages.");
                return;
            }

            Page page = doc.Pages[7];

            // Locate tables on page 7
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page);

            if (absorber.TableList.Count > 0)
            {
                // Take the first detected table
                AbsorbedTable oldTable = absorber.TableList[0];

                // Build a replacement table with explicit column widths
                Table newTable = new Table { ColumnWidths = "100 150 200" };

                // Header row
                Row header = newTable.Rows.Add();
                header.Cells.Add("Header 1");
                header.Cells.Add("Header 2");
                header.Cells.Add("Header 3");

                // Data row (sample content)
                Row data = newTable.Rows.Add();
                data.Cells.Add("Cell A");
                data.Cells.Add("Cell B");
                data.Cells.Add("Cell C");

                // Replace the absorbed table in‑place
                absorber.Replace(page, oldTable, newTable);
            }
            else
            {
                Console.WriteLine("No tables found on page 7.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
