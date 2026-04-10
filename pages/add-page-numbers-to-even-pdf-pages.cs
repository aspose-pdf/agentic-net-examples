using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // needed for FontRepository and TextState

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "even_page_numbers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Loop through pages using 1‑based indexing and process only even pages
            for (int i = 2; i <= doc.Pages.Count; i += 2)
            {
                Page page = doc.Pages[i];

                // Create a page number stamp; default format is "#"
                PageNumberStamp stamp = new PageNumberStamp();

                // Configure stamp appearance via TextState (FontSize is read‑only on the stamp itself)
                stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                stamp.TextState.FontSize = 12;
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Position the stamp (center‑bottom of the page)
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Bottom;

                // Add the stamp to the current even page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Even‑page numbers added and saved to '{outputPath}'.");
    }
}
