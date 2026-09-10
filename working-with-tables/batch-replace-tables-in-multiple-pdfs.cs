using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder  = @"C:\InputPdfs";
        // Folder where updated PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath  = Path.Combine(outputFolder, fileName + "_updated.pdf");

            try
            {
                // Load the PDF document (using ensures proper disposal)
                using (Document doc = new Document(inputPath))
                {
                    // Find all tables in the document
                    TableAbsorber absorber = new TableAbsorber();
                    absorber.Visit(doc); // extracts tables from the whole document

                    // Work on a copy of the TableList to avoid collection modification issues
                    var tables = absorber.TableList.ToList();

                    foreach (AbsorbedTable oldTable in tables)
                    {
                        // Build a simple replacement table (customize as needed)
                        Table newTable = new Table
                        {
                            // Example: single column width covering the whole old table width
                            ColumnWidths = $"{oldTable.Rectangle.Width}"
                        };

                        // Add one row with one cell containing the replacement text
                        Row row = new Row();
                        Cell cell = new Cell();
                        cell.Paragraphs.Add(new TextFragment("Updated content"));
                        row.Cells.Add(cell);
                        newTable.Rows.Add(row);

                        // Replace the old table with the new one on the appropriate page
                        // oldTable.PageNum is 1‑based, matching Aspose.Pdf page indexing
                        absorber.Replace(doc.Pages[oldTable.PageNum], oldTable, newTable);
                    }

                    // Save the modified PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {inputPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}