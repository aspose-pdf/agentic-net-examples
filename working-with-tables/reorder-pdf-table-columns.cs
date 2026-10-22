using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "reordered_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Desired column order (zero‑based indices). Example: move column 3 to first, then 1, then 2, then 0.
        int[] newOrder = { 2, 0, 1, 3 };

        using (Document doc = new Document(inputPath))
        {
            // Ensure the PDF is tagged; otherwise create a tagged structure.
            ITaggedContent tagged = doc.TaggedContent;
            if (tagged == null)
            {
                Console.Error.WriteLine("Document is not tagged. Tagged content is required for table manipulation.");
                return;
            }

            // Locate the first table in the document.
            TableElement originalTable = tagged.RootElement
                                                .FindElements<TableElement>(true)
                                                .FirstOrDefault();

            if (originalTable == null)
            {
                Console.Error.WriteLine("No table found in the document.");
                return;
            }

            // Extract cell texts from each row.
            var rows = originalTable.FindElements<TableTRElement>(true);
            var tableData = new List<List<string>>();

            foreach (TableTRElement row in rows)
            {
                var cellTexts = new List<string>();
                foreach (Element cell in row.ChildElements)
                {
                    // All cell types inherit from StructureElement; ActualText holds the displayed text.
                    if (cell is StructureElement se)
                        cellTexts.Add(se.ActualText ?? string.Empty);
                    else
                        cellTexts.Add(string.Empty);
                }
                tableData.Add(cellTexts);
            }

            // Remove the original table (optional). Here we simply add a new table after it.
            // Create a new table with reordered columns.
            TableElement newTable = tagged.CreateTableElement();
            newTable.AlternativeText = "Table with reordered columns";
            tagged.RootElement.AppendChild(newTable);

            for (int rowIndex = 0; rowIndex < tableData.Count; rowIndex++)
            {
                bool isHeaderRow = rowIndex == 0; // assume first row is the header
                TableTRElement newRow = tagged.CreateTableTRElement();
                newTable.AppendChild(newRow);

                List<string> originalCells = tableData[rowIndex];

                // Ensure the newOrder does not exceed the number of columns.
                int columnCount = Math.Min(newOrder.Length, originalCells.Count);

                for (int i = 0; i < columnCount; i++)
                {
                    int srcIdx = newOrder[i];
                    if (srcIdx >= originalCells.Count) continue; // safety check

                    string cellText = originalCells[srcIdx];

                    if (isHeaderRow)
                    {
                        TableTHElement th = tagged.CreateTableTHElement();
                        th.SetText(cellText);
                        newRow.AppendChild(th);
                    }
                    else
                    {
                        TableTDElement td = tagged.CreateTableTDElement();
                        td.SetText(cellText);
                        newRow.AppendChild(td);
                    }
                }
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Reordered table saved to '{outputPath}'.");
    }
}