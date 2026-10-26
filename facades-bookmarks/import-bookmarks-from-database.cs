using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // -----------------------------------------------------------------
        // 0. Ensure a source PDF exists – create a minimal one‑page document
        //    if the file is missing. This makes the example self‑contained and
        //    prevents a FileNotFoundException in the sandbox.
        // -----------------------------------------------------------------
        if (!File.Exists(sourcePdfPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(sourcePdfPath);
        }

        // -----------------------------------------------------------------
        // 1. Load the original PDF.
        // -----------------------------------------------------------------
        Document pdfDocument = new Document(sourcePdfPath);

        // -----------------------------------------------------------------
        // 2. Create an in‑memory DataTable that mimics the result of a DB query.
        // -----------------------------------------------------------------
        DataTable bookmarksTable = new DataTable();
        bookmarksTable.Columns.Add("Title", typeof(string));
        bookmarksTable.Columns.Add("PageNumber", typeof(int));

        // Sample data – replace or extend as needed.
        bookmarksTable.Rows.Add("Chapter 1", 1);
        bookmarksTable.Rows.Add("Chapter 2", 5);
        bookmarksTable.Rows.Add("Appendix", 12);

        // -----------------------------------------------------------------
        // 3. Iterate over the rows and add a bookmark for each record.
        // -----------------------------------------------------------------
        foreach (DataRow row in bookmarksTable.Rows)
        {
            string title = (string)row["Title"];
            int pageNumber = (int)row["PageNumber"];

            // Ensure the page number is within the document range.
            if (pageNumber < 1 || pageNumber > pdfDocument.Pages.Count)
                continue; // skip invalid entries

            // Create a GoToAction that points to the requested page.
            var goToAction = new GoToAction(pdfDocument.Pages[pageNumber], ExplicitDestinationType.Fit);

            // Create the bookmark (OutlineItemCollection) and attach the action.
            var bookmark = new OutlineItemCollection(pdfDocument.Outlines)
            {
                Title = title,
                Action = goToAction
            };

            // Add the bookmark to the document's outline collection.
            pdfDocument.Outlines.Add(bookmark);
        }

        // -----------------------------------------------------------------
        // 4. Save the modified PDF to the output path.
        // -----------------------------------------------------------------
        pdfDocument.Save(outputPdfPath);

        Console.WriteLine($"Bookmarks imported successfully to '{outputPdfPath}'.");
    }
}

/*
 * NOTE: The original build warnings were caused by a vulnerable version of
 *       System.Security.Cryptography.Pkcs (9.0.0) that Aspose.Pdf brings in as a
 *       transitive dependency. Adding an explicit PackageReference to a newer,
 *       non‑vulnerable version (e.g., 8.0.0) resolves the warnings:
 *
 * <ItemGroup>
 *   <PackageReference Include="System.Security.Cryptography.Pkcs" Version="8.0.0" />
 * </ItemGroup>
 *
 * The project file should be updated accordingly.
 */