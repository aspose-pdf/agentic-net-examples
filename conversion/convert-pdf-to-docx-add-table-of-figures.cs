using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDocx = "output.docx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (Document doc = new Document(inputPdf))
        {
            // Add a new page at the end for the Table of Figures
            Page tocPage = doc.Pages.Add();

            // Title for the table
            TextFragment title = new TextFragment("Table of Figures");
            title.TextState.FontSize = 16;
            title.TextState.FontStyle = FontStyles.Bold;
            tocPage.Paragraphs.Add(title);
            tocPage.Paragraphs.Add(new TextFragment("\n"));

            // Create a table with two columns: Figure and Page
            Table table = new Table();
            table.ColumnWidths = "30% 70%";

            // Set default text style for all cells (used for header bold text)
            table.DefaultCellTextState = new TextState { FontStyle = FontStyles.Bold };

            // Header row
            Row header = table.Rows.Add();
            Cell h1 = header.Cells.Add("Figure");
            Cell h2 = header.Cells.Add("Page");
            foreach (Cell cell in header.Cells)
            {
                cell.BackgroundColor = Color.LightGray;
                // Font style is already applied via Table.DefaultCellTextState
            }

            int figureIndex = 1;
            // Iterate through all pages and collect images
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                foreach (XImage img in page.Resources.Images)
                {
                    Row row = table.Rows.Add();
                    row.Cells.Add($"Figure {figureIndex}");
                    row.Cells.Add(i.ToString());
                    figureIndex++;
                }
            }

            // Add the table to the page
            tocPage.Paragraphs.Add(table);

            // Convert the PDF (with the added table) to DOCX
            DocSaveOptions saveOptions = new DocSaveOptions();
            saveOptions.Mode = DocSaveOptions.RecognitionMode.Flow;
            doc.Save(outputDocx, saveOptions);
        }

        Console.WriteLine($"PDF converted to DOCX with table of figures: {outputDocx}");
    }
}
