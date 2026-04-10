using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

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

        // Extract bookmarks from the source PDF
        Bookmarks rootBookmarks;
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPdfPath);
            rootBookmarks = bookmarkEditor.ExtractBookmarks();
        }

        // Create a new PDF document that will hold the bookmark data
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Create a table with three columns: Title, Level, Destination Page
            Table table = new Table
            {
                ColumnWidths = "300 80 80"
            };

            // Header row
            Row header = table.Rows.Add();
            header.Cells[0].Paragraphs.Add(new TextFragment("Title"));
            header.Cells[1].Paragraphs.Add(new TextFragment("Level"));
            header.Cells[2].Paragraphs.Add(new TextFragment("Page"));

            // Recursive helper to add bookmark rows
            void AddBookmarkRows(Bookmark bm, int level)
            {
                Row row = table.Rows.Add();
                row.Cells[0].Paragraphs.Add(new TextFragment(bm.Title ?? string.Empty));
                row.Cells[1].Paragraphs.Add(new TextFragment(level.ToString()));

                // Resolve destination page number (if any)
                int destPage = 0;
                if (bm.Destination != null)
                {
                    // Use reflection to obtain a PageNumber property if it exists.
                    // This works for PageDestination and any other destination type that exposes PageNumber.
                    var pageNumberProp = bm.Destination.GetType().GetProperty("PageNumber");
                    if (pageNumberProp != null)
                    {
                        try
                        {
                            destPage = Convert.ToInt32(pageNumberProp.GetValue(bm.Destination));
                        }
                        catch { /* ignore – keep destPage as 0 */ }
                    }
                }
                row.Cells[2].Paragraphs.Add(new TextFragment(destPage.ToString()));

                if (bm.ChildItems != null)
                {
                    foreach (Bookmark child in bm.ChildItems)
                    {
                        AddBookmarkRows(child, level + 1);
                    }
                }
            }

            // Populate table with all bookmarks (starting at level 1)
            foreach (Bookmark bm in rootBookmarks)
            {
                AddBookmarkRows(bm, 1);
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the document as an Excel workbook
            doc.Save(outputExcelPath, new ExcelSaveOptions());
        }

        Console.WriteLine($"Bookmarks exported to Excel file: {outputExcelPath}");
    }
}
