using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string reportPath = "TableDimensions.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        var lines = new List<string>();

        // Open the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Tagged PDFs expose their logical structure via ITaggedContent.
            ITaggedContent tagged = doc.TaggedContent;

            // Find every TableElement in the structure tree (recursive search).
            var tables = tagged.RootElement.FindElements<TableElement>(true);

            int tableIndex = 1;
            foreach (var table in tables)
            {
                int rowCount = 0;
                int maxColCount = 0;

                // Helper local function to process a container (thead, tbody, tfoot).
                void ProcessContainer(Element container)
                {
                    if (container is TableTHeadElement thead)
                    {
                        foreach (Element rowEl in thead.ChildElements)
                        {
                            if (rowEl is TableTRElement row)
                            {
                                rowCount++;
                                int cols = CountCells(row);
                                if (cols > maxColCount) maxColCount = cols;
                            }
                        }
                    }
                    else if (container is TableTBodyElement tbody)
                    {
                        foreach (Element rowEl in tbody.ChildElements)
                        {
                            if (rowEl is TableTRElement row)
                            {
                                rowCount++;
                                int cols = CountCells(row);
                                if (cols > maxColCount) maxColCount = cols;
                            }
                        }
                    }
                    else if (container is TableTFootElement tfoot)
                    {
                        foreach (Element rowEl in tfoot.ChildElements)
                        {
                            if (rowEl is TableTRElement row)
                            {
                                rowCount++;
                                int cols = CountCells(row);
                                if (cols > maxColCount) maxColCount = cols;
                            }
                        }
                    }
                }

                // Iterate over direct children of the table (thead, tbody, tfoot, etc.).
                foreach (Element child in table.ChildElements)
                {
                    ProcessContainer(child);
                }

                lines.Add($"Table {tableIndex}: Rows = {rowCount}, Columns = {maxColCount}");
                tableIndex++;
            }
        }

        // Write the collected dimensions to a text file.
        File.WriteAllLines(reportPath, lines);
        Console.WriteLine($"Report saved to '{reportPath}'.");
    }

    // Counts the number of cell elements (TH or TD) in a given row.
    static int CountCells(TableTRElement row)
    {
        int count = 0;
        foreach (Element cell in row.ChildElements)
        {
            if (cell is TableTHElement || cell is TableTDElement)
                count++;
        }
        return count;
    }
}