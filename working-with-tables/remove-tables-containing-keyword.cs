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
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string keyword    = "CONFIDENTIAL"; // keyword to search for (case‑insensitive)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content; if the document is not tagged, nothing to do
                ITaggedContent tagged = doc.TaggedContent;
                if (tagged == null || tagged.RootElement == null)
                {
                    Console.WriteLine("Document is not tagged – no tables to process.");
                    doc.Save(outputPath);
                    return;
                }

                // Recursively remove tables that contain the keyword
                RemoveTablesWithKeyword(tagged.RootElement, keyword);

                // Save the modified PDF
                doc.Save(outputPath);
                Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Recursively walks the structure tree, removing any TableElement that contains the keyword
    private static void RemoveTablesWithKeyword(StructureElement element, string keyword)
    {
        var tablesToRemove = new List<Element>();

        foreach (Element child in element.ChildElements)
        {
            if (child is TableElement table)
            {
                if (TableContainsKeyword(table, keyword))
                    tablesToRemove.Add(table);
            }
            else if (child is StructureElement se)
            {
                // Recurse into nested structure elements
                RemoveTablesWithKeyword(se, keyword);
            }
        }

        // Remove the identified tables from the current element's child collection
        foreach (Element tbl in tablesToRemove)
        {
            // ChildElements implements IList<Element>, so Remove is available
            var list = element.ChildElements as IList<Element>;
            list?.Remove(tbl);
        }
    }

    // Checks whether any cell (TH or TD) in the table contains the keyword
    private static bool TableContainsKeyword(TableElement table, string keyword)
    {
        foreach (Element part in table.ChildElements)
        {
            // Table rows are represented by TableTRElement
            if (part is TableTRElement row)
            {
                foreach (Element cell in row.ChildElements)
                {
                    if (cell is TableTHElement th && CellContainsKeyword(th, keyword))
                        return true;
                    if (cell is TableTDElement td && CellContainsKeyword(td, keyword))
                        return true;
                }
            }
        }
        return false;
    }

    // Determines if the given cell's text contains the keyword (case‑insensitive)
    private static bool CellContainsKeyword(StructureElement cell, string keyword)
    {
        string text = cell.ActualText ?? string.Empty;
        return text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
    }
}