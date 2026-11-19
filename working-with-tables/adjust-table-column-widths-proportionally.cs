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
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Example column widths in points (or any unit). These will be converted to percentages.
        double[] rawWidths = { 120.0, 80.0, 200.0, 100.0 };

        // Calculate total width.
        double total = rawWidths.Sum();

        // Compute percentage strings with one decimal place.
        string[] percentStrings = rawWidths
            .Select(w => $"{(w / total * 100):0.##}%")
            .ToArray();

        // Join percentages as required by TableElement.ColumnWidths (space‑separated).
        string columnWidthsSpec = string.Join(" ", percentStrings);

        using (Document doc = new Document(inputPath))
        {
            // Ensure the document is tagged (required for logical structure manipulation).
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the structure tree.
            StructureElement root = tagged.RootElement;

            // Create a table element and assign the calculated column widths.
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample table with proportional column widths";
            table.ColumnWidths = columnWidthsSpec; // e.g. "20% 13.33% 33.33% 26.67%"

            // Append the table to the root.
            root.AppendChild(table);

            // OPTIONAL: add a header row.
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // Create header cells.
            string[] headers = { "Col A", "Col B", "Col C", "Col D" };
            foreach (string hdr in headers)
            {
                TableTHElement th = tagged.CreateTableTHElement();
                th.SetText(hdr);
                headerRow.AppendChild(th);
            }

            // OPTIONAL: add a single data row.
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);
            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);
            string[] data = { "A1", "B1", "C1", "D1" };
            foreach (string cell in data)
            {
                TableTDElement td = tagged.CreateTableTDElement();
                td.SetText(cell);
                dataRow.AppendChild(td);
            }

            // Save the modified PDF. No PreSave() call is required.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with proportional column widths saved to '{outputPath}'.");
    }
}