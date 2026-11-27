using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "tables_summary.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document is tagged; otherwise table structure may not be accessible
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged. Table structure cannot be extracted.");
                return;
            }

            ITaggedContent taggedContent = doc.TaggedContent;

            // Find all TableElement objects in the structure tree (recursive search)
            var tables = taggedContent.RootElement.FindElements<TableElement>(true);

            using (StreamWriter writer = new StreamWriter(outputCsvPath))
            {
                // CSV header
                writer.WriteLine("TableIndex,RowCount,ColumnCount,TotalCellCount");

                int tableIndex = 1;
                foreach (TableElement table in tables)
                {
                    // Collect all row elements from head, body, and foot sections
                    var rows = table.ChildElements
                                    .OfType<Element>()
                                    .SelectMany(section => section.ChildElements
                                                                 .OfType<TableTRElement>())
                                    .ToList();

                    int rowCount = rows.Count;

                    int maxColumns = 0;
                    int totalCellCount = 0;

                    foreach (TableTRElement row in rows)
                    {
                        // Count header cells (TH) and data cells (TD) in the row
                        int thCount = row.ChildElements.OfType<TableTHElement>().Count();
                        int tdCount = row.ChildElements.OfType<TableTDElement>().Count();
                        int cellCount = thCount + tdCount;

                        totalCellCount += cellCount;
                        if (cellCount > maxColumns)
                            maxColumns = cellCount;
                    }

                    // Write CSV line for the current table
                    writer.WriteLine($"{tableIndex},{rowCount},{maxColumns},{totalCellCount}");
                    tableIndex++;
                }
            }

            Console.WriteLine($"Table summary CSV written to '{outputCsvPath}'.");
        }
    }
}
