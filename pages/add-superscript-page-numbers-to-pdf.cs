using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for TextState, FontStyles, FontRepository

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_page_numbers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a PageNumberStamp – this will render the page number.
            PageNumberStamp pageNumberStamp = new PageNumberStamp();

            // Use a simple format where "#" is replaced by the page number.
            pageNumberStamp.Format = "#";

            // Position the stamp at the bottom‑center of each page.
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin        = 20; // distance from the bottom edge

            // Configure appearance.
            // Superscript effect is achieved by using a smaller font size.
            // The FontStyles enum in older Aspose.PDF versions does not contain a Superscript member,
            // so we omit that assignment to keep the code compatible across versions.
            pageNumberStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            pageNumberStamp.TextState.FontSize = 8;                     // smaller than normal body text
            // pageNumberStamp.TextState.FontStyle = FontStyles.Superscript; // removed – not available in all versions
            pageNumberStamp.TextState.ForegroundColor = Color.Black;   // text color

            // Apply the stamp to every page in the document.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified PDF (lifecycle rule: save inside the using block).
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers with superscript‑like formatting added: {outputPath}");
    }
}
