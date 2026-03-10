using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputHtmlPath = "output.html";
        const string searchPhrase = "hello";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Create a TextFragmentAbsorber to search for the specified phrase
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);

                // Perform the search on the whole document
                doc.Pages.Accept(absorber);

                // Enumerate and display found fragments
                int idx = 1;
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    Console.WriteLine($"Fragment {idx}: \"{fragment.Text}\" on page {fragment.Page.Number}");
                    idx++;
                }

                // Save the PDF as HTML – must pass explicit HtmlSaveOptions
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };
                doc.Save(outputHtmlPath, htmlOpts);
            }

            Console.WriteLine($"Search completed. HTML saved to '{outputHtmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}