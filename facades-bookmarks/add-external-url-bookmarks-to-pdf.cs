using System;
using System.IO;
using System.Drawing; // System.Drawing.Color is required by PdfContentEditor
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_bookmarks.pdf";

        // Ensure a source PDF exists – create a minimal one if it does not.
        if (!File.Exists(inputPdf))
        {
            var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPdf);
        }

        // Define bookmark titles and their target URLs
        string[] titles = { "Google", "Microsoft", "GitHub" };
        string[] urls   = { "https://www.google.com", "https://www.microsoft.com", "https://github.com" };
        // Use System.Drawing.Color because PdfContentEditor.CreateBookmarksAction expects it
        System.Drawing.Color[] colors = { System.Drawing.Color.Blue, System.Drawing.Color.Green, System.Drawing.Color.Purple };

        // Use PdfContentEditor (a Facades class) to add bookmarks with URI actions
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPdf);

            // Create a bookmark for each URL
            for (int i = 0; i < titles.Length; i++)
            {
                // actionType "URI" creates a bookmark that opens an external web address
                editor.CreateBookmarksAction(
                    title:       titles[i],
                    color:       colors[i],
                    boldFlag:    true,
                    italicFlag:  false,
                    file:        null,          // not required for URI action
                    actionType:  "URI",
                    destination: urls[i]);      // external URL
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks with external links saved to '{outputPdf}'.");
    }
}
