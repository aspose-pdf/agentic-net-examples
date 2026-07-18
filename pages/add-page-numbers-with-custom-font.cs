using System;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for FontRepository and TextState

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // Ensure an input PDF exists for the demo. In the sandbox there are no
        // pre‑existing files, so we create a minimal PDF with a single blank page.
        // ---------------------------------------------------------------------
        if (!System.IO.File.Exists(inputPath))
        {
            using (Document seed = new Document())
            {
                seed.Pages.Add(); // add one empty page
                seed.Save(inputPath);
            }
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a page number stamp with default format "#"
            PageNumberStamp pageNumberStamp = new PageNumberStamp();

            // Set custom font and size
            pageNumberStamp.TextState.Font = FontRepository.FindFont("Arial");
            pageNumberStamp.TextState.FontSize = 14;

            // Position the stamp at the bottom centre of each page
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;

            // Apply the stamp to every page (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                pageNumberStamp.Put(doc.Pages[i]);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}
