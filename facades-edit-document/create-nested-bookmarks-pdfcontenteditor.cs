using System;
using System.Drawing; // System.Drawing.Color is required by PdfContentEditor.CreateBookmarksAction
using Aspose.Pdf; // For creating a seed PDF
using Aspose.Pdf.Facades; // For PdfContentEditor

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_bookmarked.pdf";

        // ---------------------------------------------------------------------
        // 1. Create a minimal PDF so the sandbox has a file to open.
        // ---------------------------------------------------------------------
        using (Document seed = new Document())
        {
            // Add a single blank page (more pages can be added if needed).
            seed.Pages.Add();
            // Save the seed PDF to the expected path.
            seed.Save(inputPdf);
        }

        // ---------------------------------------------------------------------
        // 2. Open the PDF with PdfContentEditor and add bookmarks.
        // ---------------------------------------------------------------------
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Top‑level bookmark for "Chapter One" (page 1).
        editor.CreateBookmarksAction(
            title:      "Chapter One",
            color:      System.Drawing.Color.Blue, // System.Drawing.Color required
            boldFlag:   true,
            italicFlag: false,
            file:       null,
            actionType: "GoTo",
            destination: "1" // page number as string
        );

        // Sub‑section bookmarks – added after the parent to obtain a nested outline
        // in most PDF viewers.
        editor.CreateBookmarksAction(
            title:      "Section 1.1",
            color:      System.Drawing.Color.Black,
            boldFlag:   false,
            italicFlag: false,
            file:       null,
            actionType: "GoTo",
            destination: "2"
        );

        editor.CreateBookmarksAction(
            title:      "Section 1.2",
            color:      System.Drawing.Color.Black,
            boldFlag:   false,
            italicFlag: false,
            file:       null,
            actionType: "GoTo",
            destination: "3"
        );

        // Save the modified PDF with the new bookmark hierarchy.
        editor.Save(outputPdf);
        editor.Close();
    }
}
