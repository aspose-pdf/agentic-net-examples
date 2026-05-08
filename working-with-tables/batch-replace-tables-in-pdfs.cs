using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class BatchTableReplacer
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where updated PDFs will be saved
        const string outputFolder = "OutputPdfs";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify that the input folder exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. No files to process.");
            return;
        }

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Find all tables on the current page
                    TableAbsorber absorber = new TableAbsorber();
                    absorber.Visit(page);

                    // Work on a copy of the TableList to avoid collection modification issues
                    List<AbsorbedTable> tables = absorber.TableList.ToList();

                    // Replace each absorbed table with a newly created table
                    foreach (AbsorbedTable oldTable in tables)
                    {
                        // Create a new table with the same number of rows and columns as the old one
                        Table newTable = new Table();

                        // Replicate rows and cells (here we simply fill each cell with placeholder text)
                        foreach (var oldRow in oldTable.RowList)
                        {
                            var newRow = newTable.Rows.Add();
                            foreach (var oldCell in oldRow.CellList)
                            {
                                var newCell = newRow.Cells.Add();
                                // Insert placeholder text; customize as needed
                                newCell.Paragraphs.Add(new TextFragment("Updated"));
                            }
                        }

                        // Replace the old table with the new table on the page
                        absorber.Replace(page, oldTable, newTable);
                    }
                }

                // Save the modified document to the output folder (PDF format)
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed and saved: {Path.GetFileName(inputPath)}");
        }
    }
}
