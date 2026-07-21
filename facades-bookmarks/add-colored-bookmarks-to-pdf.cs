using System;
using System.Drawing; // required for System.Drawing.Color used by PdfContentEditor
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_bookmarked.pdf";

        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Sample data: titles, target pages and whether the section is a warning (red) or informational (green)
        string[] titles   = { "Warning: Check Data", "Info: Overview", "Warning: Missing Signatures", "Info: Summary" };
        int[]    pages    = { 2, 5, 8, 12 };
        bool[]   isWarning = { true, false, true, false };

        // Initialize the facade
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        for (int i = 0; i < titles.Length; i++)
        {
            // Choose color based on section type
            Color bookmarkColor = isWarning[i] ? Color.Red : Color.Green;

            // Create a bookmark that jumps to the specified page.
            // Parameters: title, color, boldFlag, italicFlag, file (null), actionType ("GoTo"), destination (page number as string)
            editor.CreateBookmarksAction(
                titles[i],
                bookmarkColor,
                false,               // boldFlag
                false,               // italicFlag
                null,                // no external file needed
                "GoTo",              // action type
                pages[i].ToString() // destination page
            );
        }

        // Save the modified PDF
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPdf}'.");
    }
}