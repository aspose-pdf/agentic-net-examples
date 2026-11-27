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
        const string inputPath = "input.pdf";
        const string outputDir = "TableHeaders";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        using (Document doc = new Document(inputPath))
        {
            // Ensure the PDF is tagged; otherwise there is no logical structure to inspect.
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged. No table structures available.");
                return;
            }

            ITaggedContent taggedContent = doc.TaggedContent;
            StructureElement root = taggedContent.RootElement;

            // Find all table elements in the document (recursive search).
            var tables = root.FindElements<TableElement>(true);
            int tableIndex = 1;

            foreach (TableElement table in tables)
            {
                // Locate the table header (THead) element, if any.
                TableTHeadElement thead = table.FindElements<TableTHeadElement>(true).FirstOrDefault();
                if (thead == null)
                {
                    // No header for this table; skip to next.
                    tableIndex++;
                    continue;
                }

                // Typically the header contains a single row (TRElement).
                TableTRElement headerRow = thead.FindElements<TableTRElement>(true).FirstOrDefault();
                if (headerRow == null)
                {
                    tableIndex++;
                    continue;
                }

                // Extract text from each header cell (TH element).
                var headerCells = headerRow.FindElements<TableTHElement>(true);
                string[] cellTexts = headerCells
                    .Select(th => th.ActualText ?? string.Empty)
                    .ToArray();

                // Join cell texts with a tab delimiter.
                string headerLine = string.Join("\t", cellTexts);

                // Write the header line to a separate text file.
                string headerFilePath = Path.Combine(outputDir, $"Table_{tableIndex}_Header.txt");
                File.WriteAllText(headerFilePath, headerLine);

                Console.WriteLine($"Exported header of table {tableIndex} to '{headerFilePath}'.");
                tableIndex++;
            }
        }
    }
}
