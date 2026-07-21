using System;
using System.IO;
using System.Linq;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\PdfBatch\Input";
        // Folder where updated PDFs will be saved
        const string outputFolder = @"C:\PdfBatch\Output";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process every PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build output file path (same file name, different folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Create a TableAbsorber to locate tables in the document
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables from the whole document
                absorber.Visit(doc);

                // Copy the TableList because Replace modifies the collection
                var tablesToReplace = absorber.TableList.ToList();

                // Replace each found table with a new one
                foreach (AbsorbedTable oldTable in tablesToReplace)
                {
                    // Retrieve the page that contains the current table (1‑based indexing)
                    Page page = doc.Pages[oldTable.PageNum];

                    // Build a new table that will replace the old one
                    Table newTable = CreateUpdatedTable();

                    // Perform the replacement
                    absorber.Replace(page, oldTable, newTable);
                }

                // Save the modified PDF (PDF format, no SaveOptions needed)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {outputPath}");
        }
    }

    // Creates a simple table that will be used as a replacement.
    // Adjust this method to build the desired table structure/content.
    private static Table CreateUpdatedTable()
    {
        // New table instance
        Table table = new Table();

        // Example: one row with one cell containing the text "Updated"
        Row row = new Row();
        Cell cell = new Cell();

        // Add a text fragment to the cell
        cell.Paragraphs.Add(new TextFragment("Updated"));

        // Assemble row and table
        row.Cells.Add(cell);
        table.Rows.Add(row);

        // Optional styling (e.g., border)
        table.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);

        return table;
    }
}