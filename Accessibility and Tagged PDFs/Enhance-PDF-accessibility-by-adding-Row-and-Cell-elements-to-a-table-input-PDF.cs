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
        const string outputPath = "accessible_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Optional: set language and title for the tagged PDF
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast required)
            StructureElement root = taggedContent.RootElement;

            // Find all existing table structure elements in the document
            var tables = root.FindElements<TableElement>(true);

            foreach (TableElement table in tables)
            {
                // Create a new row (Table Row Element) using the factory method
                TableTRElement newRow = taggedContent.CreateTableTRElement();

                // Append the row to the table (single‑argument AppendChild)
                table.AppendChild(newRow);

                // Create a new cell (Table Cell Element) using the factory method
                TableTDElement newCell = taggedContent.CreateTableTDElement();

                // Set the cell's text content
                newCell.SetText("New cell content");

                // Append the cell to the newly created row
                newRow.AppendChild(newCell);
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
    }
}