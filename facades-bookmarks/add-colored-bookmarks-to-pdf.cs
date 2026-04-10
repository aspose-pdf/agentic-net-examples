using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // Required for Color parameters in bookmark actions

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "bookmarked_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Define sections with their titles, target page numbers and type (warning vs informational)
        var sections = new[]
        {
            new { Title = "Warning: Missing Data", Page = 2, IsWarning = true },
            new { Title = "Info: Introduction",   Page = 1, IsWarning = false },
            new { Title = "Warning: Invalid Values", Page = 5, IsWarning = true },
            new { Title = "Info: Summary",        Page = 8, IsWarning = false }
        };

        // Use PdfContentEditor (a facade) to add colored bookmarks
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF file
            editor.BindPdf(inputPdf);

            // Create a bookmark for each section with appropriate color
            foreach (var sec in sections)
            {
                // Choose red for warnings, green for informational parts
                Color bookmarkColor = sec.IsWarning ? Color.Red : Color.Green;

                // Create a "GoTo" bookmark that navigates to the specified page.
                // The destination parameter for "GoTo" is the page number as a string.
                editor.CreateBookmarksAction(
                    title:      sec.Title,
                    color:      bookmarkColor,
                    boldFlag:   false,
                    italicFlag: false,
                    file:       null,          // Not needed for "GoTo" action
                    actionType: "GoTo",
                    destination: sec.Page.ToString()
                );
            }

            // Save the modified PDF with the new colored bookmarks
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPdf}'.");
    }
}