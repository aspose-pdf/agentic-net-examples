// ------------------------------------------------------------
// File: Program.cs
// ------------------------------------------------------------
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextFragment if needed

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_split.pdf";

        Document doc;

        // Load the source PDF if it exists; otherwise create a fresh document.
        if (File.Exists(inputPath))
        {
            doc = new Document(inputPath);
        }
        else
        {
            // Creating a new PDF avoids the runtime error that occurs when Aspose tries to
            // start its internal helper executable for a missing source file.
            doc = new Document();
            doc.Pages.Add();
            Console.Error.WriteLine($"Input file not found: {inputPath}. A new PDF will be created.");
        }

        // Use a deterministic disposal pattern.
        using (doc)
        {
            // Ensure there is at least one page to host the table.
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a table and explicitly disable automatic page breaking.
            Table table = new Table
            {
                IsBroken = false,               // Prevent the table from splitting across pages.
                ColumnWidths = "100 100 100"   // Three equal‑width columns.
            };

            // Header row.
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add many data rows – enough to normally cause a split.
            for (int i = 0; i < 50; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Item {i + 1}A");
                row.Cells.Add($"Item {i + 1}B");
                row.Cells.Add($"Item {i + 1}C");
            }

            // Insert the table into the page.
            page.Paragraphs.Add(table);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with table forced onto a single page.");
    }
}

// ------------------------------------------------------------
// File: AsposePdfApi.GeneratedMSBuildEditorConfig.editorconfig
// ------------------------------------------------------------
// This file exists solely to satisfy the project file's Compile include.
// It contains no executable code and can remain empty.
namespace DummyNamespace
{
    // Empty placeholder class – the compiler will ignore this file's content.
    internal class Placeholder { }
}
