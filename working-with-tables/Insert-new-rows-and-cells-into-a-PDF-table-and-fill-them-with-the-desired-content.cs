using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Assume the table is on the first page
            Page page = doc.Pages[1];

            // Find the first Table object in the page's paragraphs
            Aspose.Pdf.Table table = page.Paragraphs
                                         .OfType<Aspose.Pdf.Table>()
                                         .FirstOrDefault();

            if (table == null)
            {
                Console.Error.WriteLine("No table found on the first page.");
                return;
            }

            // OPTIONAL: set table border if not already set
            if (table.Border == null)
                table.Border = new BorderInfo(BorderSide.All, 1f, Color.Black);
            if (table.DefaultCellBorder == null)
                table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Gray);

            // Insert a new row at the end of the table
            Row newRow = table.Rows.Add();

            // Add cells to the new row and fill them with desired content
            newRow.Cells.Add("New Cell 1");
            newRow.Cells.Add("New Cell 2");
            newRow.Cells.Add("New Cell 3");

            // Example: add another row with custom styling
            Row styledRow = table.Rows.Add();
            Cell styledCell = styledRow.Cells.Add("Styled Cell");
            styledCell.BackgroundColor = Color.LightGray;
            styledCell.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Color.DarkBlue
            };

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}