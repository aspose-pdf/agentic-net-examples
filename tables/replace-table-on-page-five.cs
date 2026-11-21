using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Simulated database data (replace with real DB access as needed)
        string[,] dbData = new string[,]
        {
            { "ID",   "Name",      "Quantity", "Price" },
            { "1",    "Apples",    "10",       "$1.20" },
            { "2",    "Bananas",   "5",        "$0.80" },
            { "3",    "Cherries",  "20",       "$2.50" }
        };

        // Open the PDF document
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Ensure the document has at least 5 pages
            if (pdfDoc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document does not contain page 5.");
                return;
            }

            // Get page 5 (1‑based indexing)
            Aspose.Pdf.Page pageFive = pdfDoc.Pages[5];

            // Remove existing paragraphs on page 5 (including any existing tables)
            // Paragraphs collection supports Clear()
            pageFive.Paragraphs.Clear();

            // Create a new table
            Aspose.Pdf.Table table = new Aspose.Pdf.Table
            {
                // Optional: set table border
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Determine number of rows and columns from the simulated data
            int rows = dbData.GetLength(0);
            int cols = dbData.GetLength(1);

            // Populate the table with data
            for (int r = 0; r < rows; r++)
            {
                // Create a new row
                Aspose.Pdf.Row row = table.Rows.Add();

                for (int c = 0; c < cols; c++)
                {
                    // Create a new cell
                    Aspose.Pdf.Cell cell = row.Cells.Add();

                    // Add text to the cell using a TextFragment
                    TextFragment tf = new TextFragment(dbData[r, c]);

                    // Optional: make header row bold
                    if (r == 0)
                    {
                        tf.TextState.FontStyle = FontStyles.Bold;
                    }

                    cell.Paragraphs.Add(tf);
                }
            }

            // Add the table to page 5
            pageFive.Paragraphs.Add(table);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Table replaced on page 5 and saved to '{outputPdfPath}'.");
    }
}