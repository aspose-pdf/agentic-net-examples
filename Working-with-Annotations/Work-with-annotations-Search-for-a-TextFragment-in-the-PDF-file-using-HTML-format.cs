using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputHtml = "output.html";
        const string phrase     = "sample"; // phrase to search for

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Create an absorber that searches for the specified phrase
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(phrase);

                // Perform the search on all pages of the document
                doc.Pages.Accept(absorber);

                // Output the number of matches and details of each fragment
                Console.WriteLine($"Found {absorber.TextFragments.Count} occurrence(s) of \"{phrase}\".");
                int i = 1;
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    Console.WriteLine($"{i++}: \"{fragment.Text}\" on page {fragment.Page.Number}");
                }

                // Prepare HTML save options (required for HTML output)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode     = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion uses GDI+ and is Windows‑only; handle possible exceptions
                try
                {
                    doc.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"HTML saved to '{outputHtml}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}