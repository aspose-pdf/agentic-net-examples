using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create a table with one row and one cell
            Table table = new Table
            {
                ColumnWidths = "200" // set column width as needed
            };
            Row row = table.Rows.Add();
            Cell cell = row.Cells.Add();

            // Create a TextFragment with the desired text
            TextFragment tf = new TextFragment("Sample Text");

            // Set font and size via the TextState of the fragment
            tf.TextState.Font = FontRepository.FindFont("TimesNewRoman");
            tf.TextState.FontSize = 14; // specific font size

            // Ensure the cell uses the TextFragment's TextState
            cell.IsOverrideByFragment = true;

            // Add the TextFragment to the cell's paragraphs collection
            cell.Paragraphs.Add(tf);

            // Add the table to the first page (or any desired page)
            Page page = doc.Pages[1];
            page.Paragraphs.Add(table);

            // Save the modified PDF (wrapped in using ensures the document stays alive until Save completes)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}