using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Use TableAbsorber (core API) to locate absorbed tables on the pages
            var absorber = new TableAbsorber();
            bool replaced = false;

            foreach (Page page in doc.Pages)
            {
                absorber.Visit(page);
                if (absorber.TableList.Count == 0)
                    continue;

                // Take the first absorbed table on the page
                var oldTable = absorber.TableList[0];

                // Build a new table with the core Table API
                var newTable = new Table
                {
                    ColumnWidths = "120 120",
                    Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black),
                    DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Gray),
                    DefaultCellPadding = new MarginInfo(4, 4, 4, 4)
                };

                // Header row
                var headerRow = newTable.Rows.Add();
                headerRow.Cells.Add("Column A");
                headerRow.Cells.Add("Column B");

                // Data row
                var dataRow = newTable.Rows.Add();
                dataRow.Cells.Add("Value 1");
                dataRow.Cells.Add("Value 2");

                // Replace the absorbed table with the new one – the new table will be placed at the same position
                absorber.Replace(page, oldTable, newTable);
                replaced = true;
                break; // replace only the first found table
            }

            if (!replaced)
            {
                Console.Error.WriteLine("No table element found to replace.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table replaced and saved to '{outputPath}'.");
    }
}
