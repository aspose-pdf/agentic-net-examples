using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_transitions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor bound to the document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Attempt to read a title from PageInfo (if available)
                string title = string.Empty;
                try
                {
                    // PageInfo may contain a Title property; guard against missing members
                    var pageInfo = page.PageInfo;
                    var titleProp = pageInfo?.GetType().GetProperty("Title");
                    if (titleProp != null)
                    {
                        title = titleProp.GetValue(pageInfo) as string ?? string.Empty;
                    }
                }
                catch { /* ignore any reflection errors */ }

                // Determine transition duration based on title length
                // Minimum duration = 1 second, each 10 characters adds 1 second
                int duration = Math.Max(1, title.Length / 10);

                // Set the page range to the current page only
                editor.ProcessPages = new int[] { i };

                // Choose a transition type (e.g., Blind Horizontal)
                editor.TransitionType = PdfPageEditor.BLINDH;

                // Apply the calculated duration
                editor.TransitionDuration = duration;

                // Apply changes for this page
                editor.ApplyChanges();
            }

            // Save the modified document
            editor.Save(outputPath);
            editor.Close(); // Dispose the facade
        }

        Console.WriteLine($"PDF saved with transitions to '{outputPath}'.");
    }
}