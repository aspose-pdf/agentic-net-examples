using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ExportBookmarksToExcel
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputExcelPath = "bookmarks.xlsx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Extract bookmarks using PdfBookmarkEditor
        Bookmarks bookmarks;
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdfPath);
            bookmarks = editor.ExtractBookmarks(); // all levels
        }

        // Create a temporary PDF that will hold the bookmark data in a table
        using (Document tempDoc = new Document())
        {
            // Add a page
            Page page = tempDoc.Pages.Add();

            // Create a table with three columns: Title, Level, Destination
            Table table = new Table();
            // Set column widths (adjust as needed)
            table.ColumnWidths = "300 100 100";

            // Add header row
            Row header = table.Rows.Add();
            header.Cells.Add("Title");
            header.Cells.Add("Level");
            header.Cells.Add("Destination Page");

            // Recursively add bookmark rows
            AddBookmarkRows(bookmarks, table, 1);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the temporary PDF as an Excel workbook
            ExcelSaveOptions excelOpts = new ExcelSaveOptions();
            tempDoc.Save(outputExcelPath, excelOpts);
        }

        Console.WriteLine($"Bookmarks exported to Excel file: {outputExcelPath}");
    }

    // Recursive helper to add bookmark information to the table
    static void AddBookmarkRows(Bookmarks bms, Table table, int level)
    {
        foreach (Bookmark bm in bms)
        {
            Row row = table.Rows.Add();
            row.Cells.Add(bm.Title ?? string.Empty);
            row.Cells.Add(level.ToString());
            // Destination page may be stored in PageNumber; fallback to empty string
            row.Cells.Add(bm.PageNumber > 0 ? bm.PageNumber.ToString() : string.Empty);

            // Process child bookmarks, if any
            if (bm.ChildItem != null && bm.ChildItem.Count > 0)
            {
                AddBookmarkRows(bm.ChildItem, table, level + 1);
            }
        }
    }
}