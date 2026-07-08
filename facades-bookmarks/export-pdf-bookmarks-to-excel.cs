using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputExcelPath = "bookmarks.xlsx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load source PDF and extract its bookmarks
        using (Document srcDoc = new Document(inputPdfPath))
        {
            PdfBookmarkEditor editor = new PdfBookmarkEditor();
            editor.BindPdf(srcDoc);
            Bookmarks rootBookmarks = editor.ExtractBookmarks(); // all levels

            // Create a new PDF that will hold the bookmark data as a table
            using (Document outDoc = new Document())
            {
                // Add a blank page
                Page page = outDoc.Pages.Add();

                // Create a table with three columns: Title, Level, Destination
                Table table = new Table
                {
                    ColumnWidths = "200 80 80", // adjust as needed
                    Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
                };

                // Header row
                Row header = table.Rows.Add();
                header.Cells.Add("Title");
                header.Cells.Add("Level");
                header.Cells.Add("Destination Page");
                // Style header (bold)
                foreach (Cell cell in header.Cells)
                {
                    cell.DefaultCellTextState = new TextState
                    {
                        FontSize = 12,
                        FontStyle = FontStyles.Bold,
                        ForegroundColor = Aspose.Pdf.Color.White
                    };
                    cell.BackgroundColor = Aspose.Pdf.Color.Gray;
                }

                // Recursively add bookmark rows
                AddBookmarkRows(rootBookmarks, table, 1);

                // Add the table to the page
                page.Paragraphs.Add(table);

                // Save the result as an Excel workbook using ExcelSaveOptions
                ExcelSaveOptions excelOpts = new ExcelSaveOptions();
                outDoc.Save(outputExcelPath, excelOpts);
            }

            Console.WriteLine($"Bookmarks exported to Excel file: {outputExcelPath}");
        }
    }

    // Recursive helper to walk the bookmark hierarchy
    static void AddBookmarkRows(Bookmarks bookmarks, Table table, int level)
    {
        foreach (Bookmark bm in bookmarks)
        {
            Row row = table.Rows.Add();
            // Title cell
            row.Cells.Add(bm.Title ?? string.Empty);
            // Level cell
            row.Cells.Add(level.ToString());
            // Destination page cell (if set)
            row.Cells.Add(bm.PageNumber > 0 ? bm.PageNumber.ToString() : string.Empty);

            // Process child bookmarks (if any)
            if (bm.ChildItem != null && bm.ChildItem.Count > 0)
            {
                AddBookmarkRows(bm.ChildItem, table, level + 1);
            }
        }
    }
}