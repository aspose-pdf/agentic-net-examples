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
        string inputFolder = "InputPdfs";
        // Folder where updated PDFs will be written
        string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName + "_updated.pdf");

            try
            {
                // Load the PDF document (using statement ensures proper disposal)
                using (Document doc = new Document(inputPath))
                {
                    // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                    for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                    {
                        Page page = doc.Pages[pageNum];

                        // Locate tables on the current page
                        TableAbsorber absorber = new TableAbsorber();
                        absorber.Visit(page);

                        // Copy the list because Replace modifies the collection
                        var tables = absorber.TableList.Cast<AbsorbedTable>().ToList();

                        foreach (AbsorbedTable oldTable in tables)
                        {
                            // Create a replacement table (example: two columns, one row)
                            Table newTable = new Table();
                            newTable.ColumnWidths = "150 150"; // column widths in points

                            // Set default cell border using the constructor that includes color
                            newTable.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black);

                            // Add a row with placeholder text
                            Row newRow = newTable.Rows.Add();
                            Cell cell1 = newRow.Cells.Add();
                            cell1.Paragraphs.Add(new TextFragment("Updated 1"));
                            Cell cell2 = newRow.Cells.Add();
                            cell2.Paragraphs.Add(new TextFragment("Updated 2"));

                            // Replace the original table with the new one
                            absorber.Replace(page, oldTable, newTable);
                        }
                    }

                    // Save the modified document
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {inputPath} -> {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}
