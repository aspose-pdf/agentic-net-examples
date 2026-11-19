using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class BatchTableReplacer
{
    // Entry point
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\Pdf\Input";
        // Folder where modified PDFs will be saved
        const string outputFolder = @"C:\Pdf\Output";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Gather all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        foreach (string inputPath in pdfFiles)
        {
            // Determine output file path (same file name in output folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            // Load and process each PDF inside a using block (deterministic disposal)
            using (Document doc = new Document(inputPath))
            {
                // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Absorb existing tables on the current page
                    TableAbsorber absorber = new TableAbsorber();
                    absorber.Visit(page); // <-- correct method to populate TableList

                    // Copy the list because we will modify the page while iterating
                    var tablesToReplace = new System.Collections.Generic.List<AbsorbedTable>(absorber.TableList);

                    foreach (AbsorbedTable absorbedTable in tablesToReplace)
                    {
                        // Remove the original table from the page
                        absorber.Remove(absorbedTable);

                        // Insert a replacement table (custom logic can be placed in CreateReplacementTable)
                        Table newTable = CreateReplacementTable();
                        page.Paragraphs.Add(newTable);
                    }
                }

                // Save the modified document (save inside the using block)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {outputPath}");
        }
    }

    // Generates a simple replacement table.
    // In a real scenario this could load data from a database, another PDF, etc.
    private static Table CreateReplacementTable()
    {
        // Create a table with 2 columns and 2 rows as an example
        Table table = new Table
        {
            ColumnWidths = "200 200", // widths in points
            Border = new BorderInfo(BorderSide.All, 0.5f)
        };

        // Header row
        Row header = table.Rows.Add();
        header.Cells.Add("Header 1");
        header.Cells.Add("Header 2");
        header.BackgroundColor = Aspose.Pdf.Color.LightGray;

        // Data row
        Row data = table.Rows.Add();
        data.Cells.Add("Value A");
        data.Cells.Add("Value B");

        return table;
    }
}
