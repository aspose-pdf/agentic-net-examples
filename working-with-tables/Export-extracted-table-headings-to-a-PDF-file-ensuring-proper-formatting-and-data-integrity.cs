using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ExportTableHeadings
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF containing tables
        const string outputPdfPath = "headings.pdf";   // PDF that will contain extracted headings

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF and extract tables
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // TableAbsorber extracts tables from the document
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(srcDoc); // extracts tables from all pages

            // Create a new PDF that will hold the headings
            using (Document outDoc = new Document())
            {
                // Add a blank page to the output document
                Page outPage = outDoc.Pages.Add();

                // Create a table to list the headings
                Table headingsTable = new Table();

                // Basic formatting for the table
                headingsTable.Border = new BorderInfo(BorderSide.All, 1);
                headingsTable.DefaultCellBorder = new BorderInfo(BorderSide.All, 1);
                headingsTable.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);
                headingsTable.DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    ForegroundColor = Aspose.Pdf.Color.Black
                };
                headingsTable.BackgroundColor = Aspose.Pdf.Color.LightGray;

                // Process each extracted table
                foreach (var table in absorber.TableList)
                {
                    // Ensure the table has at least one row (the heading row)
                    if (table.RowList.Count == 0) continue;

                    // Extract text from the first row (assumed to be the heading)
                    var headingRow = table.RowList[0];

                    // Create a new row in the output table
                    var outRow = headingsTable.Rows.Add();

                    // Add a cell for each heading column
                    foreach (var cell in headingRow.CellList)
                    {
                        // Concatenate all text fragments inside the cell
                        string cellText = string.Empty;
                        foreach (var fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }

                        // Create a new cell in the output row and add the text
                        var outCell = outRow.Cells.Add();
                        outCell.Paragraphs.Add(new TextFragment(cellText));
                    }
                }

                // Add the populated table to the page
                outPage.Paragraphs.Add(headingsTable);

                // Save the result
                outDoc.Save(outputPdfPath);
                Console.WriteLine($"Headings exported to '{outputPdfPath}'.");
            }
        }
    }
}