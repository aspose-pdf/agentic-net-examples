using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Create a page number stamp. The default format "#" will be replaced by the page number.
            PageNumberStamp pageNumberStamp = new PageNumberStamp();
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin = 20; // distance from bottom edge
            pageNumberStamp.TextState.FontSize = 12;
            pageNumberStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            pageNumberStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Apply the stamp to all existing pages.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Insert a new page at position 2 (1‑based index).
            Page insertedPage = doc.Pages.Insert(2);
            // Apply the same stamp to the newly inserted page.
            insertedPage.AddStamp(pageNumberStamp);

            // Save the updated document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page‑numbered PDF saved to '{outputPath}'.");
    }
}