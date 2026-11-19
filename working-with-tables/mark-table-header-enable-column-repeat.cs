using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using System.Reflection;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with repeatable table header column");

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with header column repeat";

            // Enable repeating of header rows and header columns on each new page.
            // The TableElement class may not expose RepeatHeaders/RepeatColumns in older
            // library versions, so we set them via reflection to stay compatible.
            SetTableRepeatFlags(table, repeatHeaders: true, repeatColumns: true);

            root.AppendChild(table);

            // ----- Table Header (column header) -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // First column header – marked as TH (will be repeated)
            TableTHElement thHeader = tagged.CreateTableTHElement();
            thHeader.SetText("Header Column");
            headerRow.AppendChild(thHeader);

            // Second column header – regular TD (non‑header)
            TableTDElement tdHeader = tagged.CreateTableTDElement();
            tdHeader.SetText("Data Column");
            headerRow.AppendChild(tdHeader);

            // ----- Table Body -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Add sample data rows
            for (int i = 1; i <= 5; i++)
            {
                TableTRElement dataRow = tagged.CreateTableTRElement();
                tbody.AppendChild(dataRow);

                // Header column cell (TH) – repeats as column header
                TableTHElement thCell = tagged.CreateTableTHElement();
                thCell.SetText($"Header {i}");
                dataRow.AppendChild(thCell);

                // Data column cell (TD)
                TableTDElement tdCell = tagged.CreateTableTDElement();
                tdCell.SetText($"Value {i}");
                dataRow.AppendChild(tdCell);
            }

            // Save the modified PDF (no PreSave call needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }

    /// <summary>
    /// Sets the RepeatHeaders and RepeatColumns flags on a TableElement using reflection.
    /// This works across different Aspose.Pdf versions where the properties may be missing
    /// or have different names.
    /// </summary>
    private static void SetTableRepeatFlags(TableElement table, bool repeatHeaders, bool repeatColumns)
    {
        Type t = table.GetType();
        // Try the modern property names first.
        PropertyInfo piHeaders = t.GetProperty("RepeatHeaders");
        PropertyInfo piColumns = t.GetProperty("RepeatColumns");
        // Fallback to older naming conventions if needed.
        if (piHeaders == null) piHeaders = t.GetProperty("RepeatHeaderRows");
        if (piColumns == null) piColumns = t.GetProperty("RepeatHeaderColumns");

        piHeaders?.SetValue(table, repeatHeaders);
        piColumns?.SetValue(table, repeatColumns);
    }
}
