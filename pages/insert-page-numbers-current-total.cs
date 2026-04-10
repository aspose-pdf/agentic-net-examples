using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_page_numbers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a PageNumberStamp with a custom format "current/total"
            PageNumberStamp pageNumberStamp = new PageNumberStamp("#/#");
            pageNumberStamp.StartingNumber = 1;                     // start numbering at 1
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            // Font size must be set via TextState (FontSize property on TextStamp is read‑only)
            pageNumberStamp.TextState.FontSize = 12;
            pageNumberStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            pageNumberStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            pageNumberStamp.Background = false;                    // stamp on top of page content

            // Apply the stamp to every page in the document
            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
            {
                doc.Pages[i].AddStamp(pageNumberStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}
