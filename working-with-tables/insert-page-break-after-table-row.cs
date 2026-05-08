using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Assume the first page contains a table.
            // Locate the first Table element in the page's paragraphs.
            Table table = null;
            foreach (var paragraph in doc.Pages[1].Paragraphs)
            {
                if (paragraph is Table tbl)
                {
                    table = tbl;
                    break;
                }
            }

            if (table == null)
            {
                Console.Error.WriteLine("No table found on the first page.");
                return;
            }

            // Choose the row after which we want a page break.
            // Row indices are zero‑based in the Rows collection.
            int targetRowIndex = 2; // break after the third row (index 2)

            if (targetRowIndex < 0 || targetRowIndex >= table.Rows.Count)
            {
                Console.Error.WriteLine("Target row index is out of range.");
                return;
            }

            // Set IsInNewPage on the row that should start on a new page.
            // This forces the row (and all following rows) to be printed on the next page.
            table.Rows[targetRowIndex].IsInNewPage = true;

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with page break after row {2} to '{outputPath}'.");
    }
}