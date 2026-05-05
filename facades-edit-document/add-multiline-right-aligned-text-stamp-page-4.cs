using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facade namespace is referenced as required
using Aspose.Pdf.Text;      // For TextStamp related types

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: using block for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Multiline text for the stamp (use CRLF or \n)
            string multilineText = "First line\r\nSecond line\r\nThird line";

            // Create a TextStamp with the multiline content
            TextStamp stamp = new TextStamp(multilineText);

            // Align the stamp to the right and bottom of the page
            stamp.HorizontalAlignment = HorizontalAlignment.Right;
            stamp.VerticalAlignment   = VerticalAlignment.Bottom;

            // Optional: set a bottom margin so the stamp is not flush with the edge
            stamp.BottomMargin = 20;   // points from the bottom edge

            // Ensure the document has at least four pages (1‑based indexing)
            if (doc.Pages.Count >= 4)
            {
                // Retrieve page 4 and add the stamp to it
                Page pageFour = doc.Pages[4];
                pageFour.AddStamp(stamp);
            }
            else
            {
                Console.Error.WriteLine("The document contains fewer than 4 pages.");
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Multiline right‑aligned stamp added to page 4 and saved as '{outputPath}'.");
    }
}