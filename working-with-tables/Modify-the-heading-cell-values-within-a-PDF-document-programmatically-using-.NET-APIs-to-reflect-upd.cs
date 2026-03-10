using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "updated_headings.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables in the document
            TableAbsorber tableAbsorber = new TableAbsorber();

            // Extract tables from the entire document
            tableAbsorber.Visit(doc);

            // Example new heading values (replace with your actual data)
            string[] newHeadings = { "Product", "Quantity", "Price", "Total" };
            int headingIndex = 0;

            // Iterate over each found table
            foreach (AbsorbedTable table in tableAbsorber.TableList)
            {
                // Assume the first row of the table contains the headings
                if (table.RowList.Count == 0) continue;

                AbsorbedRow headerRow = table.RowList[0];

                // Iterate over each cell in the header row
                foreach (AbsorbedCell cell in headerRow.CellList)
                {
                    // Each cell may contain multiple text fragments; update the first one
                    if (cell.TextFragments.Count > 0 && headingIndex < newHeadings.Length)
                    {
                        TextFragment fragment = cell.TextFragments[0];
                        fragment.Text = newHeadings[headingIndex];
                        headingIndex++;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}