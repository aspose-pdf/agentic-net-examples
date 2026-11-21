using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for TableAbsorber

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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least two pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document contains fewer than 2 pages.");
                return;
            }

            // Get page 2
            Page page2 = doc.Pages[2];

            // Use TableAbsorber to locate tables on page 2
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page2);

            // Create a new table (the content you want to insert)
            Table newTable = new Table
            {
                // Example column widths (adjust as needed)
                ColumnWidths = "100 100 100"
            };

            // Header row
            Row header = new Row();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");
            newTable.Rows.Add(header);

            // Data row
            Row data = new Row();
            data.Cells.Add("Cell A");
            data.Cells.Add("Cell B");
            data.Cells.Add("Cell C");
            newTable.Rows.Add(data);

            if (absorber.TableList.Count > 0)
            {
                // Replace the first absorbed table with the new one, preserving its position.
                // TableAbsorber.Replace handles positioning; the Table class itself has no Rect property.
                absorber.Replace(page2, absorber.TableList[0], newTable);
            }
            else
            {
                // No existing table – add the new table at a default location.
                // Position it using an absolute FloatingBox so it appears at a known spot.
                var box = new FloatingBox((float)newTable.GetWidth(), (float)newTable.GetHeight())
                {
                    PositioningMode = ParagraphPositioningMode.Absolute,
                    Left = 0,
                    Top = page2.PageInfo.Height // top‑aligned (Y coordinate measured from bottom)
                };
                box.Paragraphs.Add(newTable);
                page2.Paragraphs.Add(box);
            }

            // Save the modified PDF (save is performed inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
