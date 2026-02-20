using System;
using System.IO;
using Aspose.Pdf.Facades;

class UpdateBookmarkExample
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output PDF file path (will contain the updated bookmark)
        const string outputPdf = "output.pdf";

        // Title of the bookmark to be updated and the new title
        const string oldTitle = "Old Bookmark";
        const string newTitle = "New Bookmark";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdf}");
            return;
        }

        try
        {
            // Initialize the PdfBookmarkEditor facade
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                // Bind the existing PDF document
                editor.BindPdf(inputPdf);

                // Update the bookmark title
                editor.ModifyBookmarks(oldTitle, newTitle);

                // Save the modified PDF
                editor.Save(outputPdf);
            }

            Console.WriteLine($"Bookmark \"{oldTitle}\" successfully updated to \"{newTitle}\".");
            Console.WriteLine($"Modified PDF saved as \"{outputPdf}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while updating the bookmark: {ex.Message}");
        }
    }
}