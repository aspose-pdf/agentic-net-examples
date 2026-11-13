using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputCsvPath = "tables_with_borders.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Extract all text from the document (including tables) using TextAbsorber
            TextAbsorber absorber = new TextAbsorber();
            // Use Pure formatting to keep line breaks; this helps to separate rows
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            pdfDoc.Pages.Accept(absorber);
            string fullText = absorber.Text;

            // Split the extracted text into lines (each line may represent a table row)
            string[] lines = fullText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                foreach (string line in lines)
                {
                    // Simple heuristic: split by one or more whitespace characters to get cells
                    string[] cells = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    // If we have more than one cell, treat it as a table row
                    if (cells.Length > 1)
                    {
                        // Add delimiter markers (e.g., pipe symbols) around each cell for visual border reference
                        string markedRow = string.Join(",", cells.Select(c => $"|{c}|"));
                        writer.WriteLine(markedRow);
                    }
                    else
                    {
                        // Write non‑table lines as‑is
                        writer.WriteLine(line);
                    }
                }
            }

            Console.WriteLine($"Table data exported with border markers to '{outputCsvPath}'.");
        }
    }
}