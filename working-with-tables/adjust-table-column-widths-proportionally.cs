using System;
using System.IO;
using System.Globalization;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

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
            // Access tagged content (creates it if missing) and set basic metadata
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            root.AppendChild(table);

            // Initial column widths (as a space‑separated string, the API expects a string)
            table.ColumnWidths = "120 80 200";

            // ----- Proportional width adjustment -----
            // Parse the string into a float array
            float[] originalWidths = table.ColumnWidths
                .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => float.Parse(s, CultureInfo.InvariantCulture))
                .ToArray();

            float totalWidth = originalWidths.Sum();

            // Guard against division by zero
            if (totalWidth > 0f)
            {
                float[] percentWidths = new float[originalWidths.Length];
                for (int i = 0; i < originalWidths.Length; i++)
                {
                    // Convert each width to a percentage of the total
                    percentWidths[i] = (originalWidths[i] / totalWidth) * 100f;
                }
                // Convert back to the string format expected by the API
                string percentString = string.Join(" ", percentWidths.Select(p => p.ToString(CultureInfo.InvariantCulture)));
                table.ColumnWidths = percentString;
            }

            // Use the final column count for building rows
            int columnCount = originalWidths.Length;

            // Build a simple header row
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);
            for (int i = 0; i < columnCount; i++)
            {
                TableTHElement th = tagged.CreateTableTHElement();
                th.SetText($"Header {i + 1}");
                headerRow.AppendChild(th);
            }

            // Build a single data row
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);
            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);
            for (int i = 0; i < columnCount; i++)
            {
                TableTDElement td = tagged.CreateTableTDElement();
                td.SetText($"Cell {i + 1}");
                dataRow.AppendChild(td);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
