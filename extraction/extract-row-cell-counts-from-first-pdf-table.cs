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
        const string inputPdf = "input.pdf";
        const string outputTxt = "RowCellCounts.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Access tagged content (must be a tagged PDF)
            ITaggedContent tagged = doc.TaggedContent;
            if (tagged == null)
            {
                Console.Error.WriteLine("Document is not tagged. No structure tree available.");
                return;
            }

            // Find the first table element in the structure tree
            TableElement firstTable = tagged.RootElement
                                            .FindElements<TableElement>(true)
                                            .FirstOrDefault();

            if (firstTable == null)
            {
                Console.Error.WriteLine("No table element found in the document.");
                return;
            }

            // Collect all rows (TableTRElement) within the table (including head, body, foot)
            var rows = firstTable.FindElements<TableTRElement>(true);

            // Prepare the output lines: each line contains the cell count of a row
            var lines = rows.Select(row =>
            {
                // Count cells: both header cells (TableTHElement) and data cells (TableTDElement)
                int cellCount = row.ChildElements
                                   .Count(child => child is TableTHElement || child is TableTDElement);
                return cellCount.ToString();
            }).ToArray();

            // Write the counts to the text file
            File.WriteAllLines(outputTxt, lines);
            Console.WriteLine($"Row cell counts written to '{outputTxt}'.");
        }
    }
}