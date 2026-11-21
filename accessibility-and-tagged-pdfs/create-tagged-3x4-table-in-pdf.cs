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
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF and obtain the tagged content interface
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent taggedContent = doc.TaggedContent;

            // Ensure the document is marked as tagged (if not, this will create the structure)
            StructureElement root = taggedContent.RootElement;

            // Create a table element and set its alternative text (description)
            TableElement table = taggedContent.CreateTableElement();
            table.AlternativeText = "Sample 3‑row by 4‑column table";
            root.AppendChild(table); // attach table to the root

            // Create the table body container
            TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Generate 3 rows
            for (int rowIndex = 1; rowIndex <= 3; rowIndex++)
            {
                // Create a table row
                TableTRElement row = taggedContent.CreateTableTRElement();
                tbody.AppendChild(row);

                // Generate 4 columns (cells) for each row
                for (int colIndex = 1; colIndex <= 4; colIndex++)
                {
                    TableTDElement cell = taggedContent.CreateTableTDElement();
                    cell.SetText($"R{rowIndex}C{colIndex}"); // set cell text
                    row.AppendChild(cell);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}