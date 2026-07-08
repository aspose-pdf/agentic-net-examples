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
        const int targetRowIndex = 3; // zero‑based index of the row after which a page break is required

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Assume the first page contains the table we want to modify
            Page page = doc.Pages[1];

            // Find the first Table element on the page (if any)
            Table table = null;
            foreach (var element in page.Paragraphs)
            {
                if (element is Table t)
                {
                    table = t;
                    break;
                }
            }

            if (table == null)
            {
                Console.Error.WriteLine("No table found on the first page.");
                return;
            }

            // Validate the target row index
            if (targetRowIndex < 0 || targetRowIndex >= table.Rows.Count - 1)
            {
                Console.Error.WriteLine("Target row index is out of range.");
                return;
            }

            // The row after which we want a page break
            Row nextRow = table.Rows[targetRowIndex + 1];

            // Set IsInNewPage = true to force this row onto a new page
            nextRow.IsInNewPage = true;

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with page break after row {targetRowIndex} → '{outputPath}'.");
    }
}