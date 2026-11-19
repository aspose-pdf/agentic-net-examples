using System;
using System.IO;
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

        // Desired column order (zero‑based indices of original columns)
        // Example: original columns A,B,C,D -> new order C,A,D,B
        int[] newOrder = { 2, 0, 3, 1 };

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content
                ITaggedContent tagged = doc.TaggedContent;

                // Find the first table in the document
                TableElement originalTable = null;
                var tables = tagged.RootElement.FindElements<TableElement>(true);
                if (tables.Count > 0)
                    originalTable = tables[0];

                if (originalTable == null)
                {
                    Console.WriteLine("No table found in the document.");
                    doc.Save(outputPath); // Save unchanged
                    return;
                }

                // Create a new table that will hold the reordered columns
                TableElement newTable = tagged.CreateTableElement();
                newTable.AlternativeText = originalTable.AlternativeText; // preserve description
                // Append the new table to the root (or any desired parent)
                tagged.RootElement.AppendChild(newTable);

                // Helper to copy a cell (TH or TD) preserving its text and alternative text
                static void CopyCell(Element srcCell, TableTRElement targetRow)
                {
                    if (srcCell is TableTHElement thSrc)
                    {
                        TableTHElement thNew = targetRow.CreateTH();
                        thNew.SetText(thSrc.ActualText);
                        thNew.AlternativeText = thSrc.AlternativeText;
                    }
                    else if (srcCell is TableTDElement tdSrc)
                    {
                        TableTDElement tdNew = targetRow.CreateTD();
                        tdNew.SetText(tdSrc.ActualText);
                        tdNew.AlternativeText = tdSrc.AlternativeText;
                    }
                }

                // Process each row (both header and body rows)
                foreach (Element rowElem in originalTable.ChildElements)
                {
                    if (rowElem is TableTHeadElement thead)
                    {
                        // Header rows
                        foreach (Element headerRow in thead.ChildElements)
                        {
                            if (headerRow is TableTRElement tr)
                            {
                                TableTRElement newHeaderRow = newTable.CreateTHead().CreateTR();
                                var cells = tr.ChildElements;
                                for (int i = 0; i < newOrder.Length && i < cells.Count; i++)
                                {
                                    int srcIdx = newOrder[i];
                                    if (srcIdx < cells.Count)
                                        CopyCell(cells[srcIdx], newHeaderRow);
                                }
                            }
                        }
                    }
                    else if (rowElem is TableTBodyElement tbody)
                    {
                        // Body rows
                        foreach (Element bodyRow in tbody.ChildElements)
                        {
                            if (bodyRow is TableTRElement tr)
                            {
                                TableTRElement newBodyRow = newTable.CreateTBody().CreateTR();
                                var cells = tr.ChildElements;
                                for (int i = 0; i < newOrder.Length && i < cells.Count; i++)
                                {
                                    int srcIdx = newOrder[i];
                                    if (srcIdx < cells.Count)
                                        CopyCell(cells[srcIdx], newBodyRow);
                                }
                            }
                        }
                    }
                    else if (rowElem is TableTFootElement tfoot)
                    {
                        // Footer rows (handled similarly)
                        foreach (Element footRow in tfoot.ChildElements)
                        {
                            if (footRow is TableTRElement tr)
                            {
                                TableTRElement newFootRow = newTable.CreateTFoot().CreateTR();
                                var cells = tr.ChildElements;
                                for (int i = 0; i < newOrder.Length && i < cells.Count; i++)
                                {
                                    int srcIdx = newOrder[i];
                                    if (srcIdx < cells.Count)
                                        CopyCell(cells[srcIdx], newFootRow);
                                }
                            }
                        }
                    }
                }

                // Save the document (no PreSave needed)
                doc.Save(outputPath);
                Console.WriteLine($"Reordered table saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}