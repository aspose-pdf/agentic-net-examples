using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string mhtPath      = "input.mht";      // source MHT file
        const string outputPdf    = "converted.pdf";  // PDF after conversion
        const string searchPhrase = "Aspose";         // text to search for

        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtPath}");
            return;
        }

        // 1. Load the MHT file and convert it to PDF
        //    MhtLoadOptions is the correct class for loading .mht files.
        MhtLoadOptions loadOptions = new MhtLoadOptions();

        using (Document pdfDoc = new Document(mhtPath, loadOptions))
        {
            // Save the intermediate PDF (optional, shows conversion succeeded)
            pdfDoc.Save(outputPdf);
            Console.WriteLine($"MHT converted to PDF: {outputPdf}");

            // 2. Search for the desired text using TextFragmentAbsorber
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);
            // Accept the absorber for the whole document
            pdfDoc.Pages.Accept(absorber);

            // 3. Report the results
            if (absorber.TextFragments.Count == 0)
            {
                Console.WriteLine($"No occurrences of \"{searchPhrase}\" were found.");
            }
            else
            {
                Console.WriteLine($"Found {absorber.TextFragments.Count} occurrence(s) of \"{searchPhrase}\":");
                int index = 1;
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // The page on which the fragment appears
                    int pageNumber = fragment.Page.Number; // 1‑based indexing
                    // Position of the fragment (lower‑left corner)
                    double x = fragment.Rectangle.LLX;
                    double y = fragment.Rectangle.LLY;

                    Console.WriteLine($"{index}. Page {pageNumber}, Position ({x}, {y})");
                    index++;
                }
            }

            // (Optional) If you want to add a simple free‑text annotation at each match,
            // you could use PdfContentEditor. This example adds a note with the text "Found".
            // Note: PdfContentEditor methods accept System.Drawing.Rectangle and Color.
            // To stay cross‑platform, we avoid using System.Drawing types here.
            // Instead, we simply demonstrate the search without adding annotations.
        }
    }
}