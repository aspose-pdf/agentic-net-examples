using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class MergeTableHeadingCells
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "merged_headings.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Find all table structure elements in the document
            var tables = tagged.RootElement.FindElements<TableElement>(true);

            foreach (TableElement table in tables)
            {
                // Locate the table header (THead) element
                TableTHeadElement thead = table.FindElements<TableTHeadElement>(true).FirstOrDefault();
                if (thead == null) continue; // No header, skip

                // Locate the first header row (TR) inside the THead
                TableTRElement headerRow = thead.FindElements<TableTRElement>(true).FirstOrDefault();
                if (headerRow == null) continue; // No rows, skip

                // Collect all TH cells in the header row
                var headerCells = headerRow.ChildElements
                                            .OfType<TableTHElement>()
                                            .ToList();

                // Example: merge the first two heading cells if at least two exist
                if (headerCells.Count >= 2)
                {
                    TableTHElement firstCell  = headerCells[0];
                    TableTHElement secondCell = headerCells[1];

                    // Preserve formatting from the second cell (if needed)
                    // Background color
                    if (secondCell.BackgroundColor != null)
                        firstCell.BackgroundColor = secondCell.BackgroundColor;

                    // Border
                    if (secondCell.Border != null)
                        firstCell.Border = secondCell.Border;

                    // Text state (font, size, color, etc.)
                    if (secondCell.DefaultCellTextState != null)
                        firstCell.DefaultCellTextState = secondCell.DefaultCellTextState;

                    // Increase column span to cover both cells
                    // Default ColSpan is 1; add the second cell's span
                    firstCell.ColSpan = (firstCell.ColSpan > 0 ? firstCell.ColSpan : 1) +
                                        (secondCell.ColSpan > 0 ? secondCell.ColSpan : 1);

                    // Remove the second cell from the structure tree
                    secondCell.Remove();
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table heading cells merged and saved to '{outputPath}'.");
    }
}