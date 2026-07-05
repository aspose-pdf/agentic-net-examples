using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "multicolumn.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Layout parameters
            const float marginLeft = 50f;
            const float marginTop = 750f;
            const float columnWidth = 250f;
            const float columnSpacing = 10f;
            const float lineHeight = 15f; // approximate height for 12‑pt font

            // Add 20 lines – first 10 go to column 1, next 10 to column 2
            for (int i = 1; i <= 20; i++)
            {
                // Determine column (0‑based) and row inside the column
                int columnIndex = (i - 1) / 10;               // 0 for lines 1‑10, 1 for 11‑20
                int rowIndex = (i - 1) % 10;                  // 0‑9 inside each column

                // Calculate X/Y position for the current line
                float x = marginLeft + columnIndex * (columnWidth + columnSpacing);
                float y = marginTop - rowIndex * lineHeight;

                // Create a TextFragment for the line
                TextFragment tf = new TextFragment($"Line {i} – this text will appear in column {columnIndex + 1}.");
                tf.Position = new Position(x, y);
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Color.Black;

                // Append the fragment to the page
                page.Paragraphs.Add(tf);
            }

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Multi‑column PDF saved to '{outputPath}'.");
    }
}
