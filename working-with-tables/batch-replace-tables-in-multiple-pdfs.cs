using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class BatchTableReplacer
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Folder where modified PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName + "_updated.pdf");

            // Load the PDF document (using statement ensures proper disposal)
            using (Document doc = new Document(inputPath))
            {
                // Create a TableAbsorber to locate tables in the document
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables from the entire document
                absorber.Visit(doc);

                // Copy the TableList to avoid collection modification issues during replacement
                var tables = absorber.TableList.ToList();

                // Iterate over each absorbed table and replace it with a new table
                foreach (AbsorbedTable oldTable in tables)
                {
                    // Create a simple replacement table
                    Table newTable = new Table
                    {
                        // Example: set a light gray background for visibility
                        BackgroundColor = Aspose.Pdf.Color.LightGray,
                        // Define column widths (single column in this example)
                        ColumnWidths = "200"
                    };

                    // Add a single row with one cell containing updated text
                    Row row = new Row();
                    Cell cell = new Cell();
                    cell.Paragraphs.Add(new TextFragment("Updated"));
                    row.Cells.Add(cell);
                    newTable.Rows.Add(row);

                    // Replace the old table on its page with the new table
                    // PageNum is 1‑based, matching Aspose.Pdf indexing
                    absorber.Replace(doc.Pages[oldTable.PageNum], oldTable, newTable);
                }

                // Save the modified document to the output folder
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed '{inputPath}' → '{outputPath}'");
        }
    }
}