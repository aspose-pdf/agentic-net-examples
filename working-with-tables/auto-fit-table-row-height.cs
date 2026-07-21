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
        const string outputPath = "output_autofit_row.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample table with auto‑fit rows";
            root.AppendChild(table);

            // Create a table row (TableTRElement) via the factory
            TableTRElement row = tagged.CreateTableTRElement();

            // Attach the row to the table
            table.AppendChild(row);

            // IMPORTANT: Enable automatic height adjustment for the row.
            // Setting FixedRowHeight to 0 and MinRowHeight to 0 lets the row
            // height be determined by its intrinsic content (auto‑fit).
            row.FixedRowHeight = 0;   // No fixed height
            row.MinRowHeight   = 0;   // No minimum height constraint

            // Add a header cell
            TableTHElement th = tagged.CreateTableTHElement();
            th.SetText("Header");
            row.AppendChild(th);

            // Add a data cell with multi‑line content to demonstrate auto‑fit
            TableTDElement td = tagged.CreateTableTDElement();
            td.SetText("Line 1\nLine 2\nLine 3");
            row.AppendChild(td);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with auto‑fit row height: '{outputPath}'");
    }
}