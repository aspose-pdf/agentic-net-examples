using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Prepare a DataTable with a column that will be used for naming PDF pages/files
        DataTable table = new DataTable();
        table.Columns.Add("PageName", typeof(string));
        table.Rows.Add("Introduction");
        table.Rows.Add("Chapter1");
        table.Rows.Add("Chapter2");
        table.Rows.Add("Conclusion");

        // Path for the temporary combined PDF that will hold all pages
        string combinedPdfPath = Path.Combine(Path.GetTempPath(), "combined.pdf");

        // Create a PDF document and add one page per DataRow, inserting the column value as text
        using (Document doc = new Document())
        {
            foreach (DataRow row in table.Rows)
            {
                // Add a new blank page
                Page page = doc.Pages.Add();

                // Create a text fragment with the column value
                string pageTitle = row["PageName"].ToString();
                TextFragment tf = new TextFragment(pageTitle);
                tf.Position = new Position(100, 700); // place text near top‑left
                tf.TextState.FontSize = 24;
                tf.TextState.FontStyle = FontStyles.Bold;

                // Add the text to the page
                page.Paragraphs.Add(tf);
            }

            // Save the combined PDF to the temporary location
            doc.Save(combinedPdfPath);
        }

        // Use Aspose.Pdf.Facades.PdfFileEditor to extract each page into a separate PDF file
        // named according to the DataTable column value.
        PdfFileEditor editor = new PdfFileEditor();

        int pageIndex = 1; // Aspose.Pdf uses 1‑based page indexing
        foreach (DataRow row in table.Rows)
        {
            string pageName = row["PageName"].ToString();
            string outputPath = $"{pageName}.pdf";

            // Correct overload: Extract(sourcePdf, startPage, endPage, outputPdf)
            editor.Extract(combinedPdfPath, pageIndex, pageIndex, outputPath);

            Console.WriteLine($"Created '{outputPath}' from page {pageIndex}.");
            pageIndex++;
        }

        // Optionally delete the temporary combined PDF
        if (File.Exists(combinedPdfPath))
        {
            File.Delete(combinedPdfPath);
        }
    }
}
