using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputEpubPath = "output.epub";
        const string searchPhrase = "sample text";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Create a TextFragmentAbsorber to search for the specified phrase
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);

                // Apply the absorber to all pages of the document
                pdfDoc.Pages.Accept(absorber);

                // Output information about each found fragment
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // The Page property may be null if the fragment spans multiple pages;
                    // use the first page number from the fragment's rectangle as a fallback.
                    int pageNumber = fragment.Page?.Number ?? pdfDoc.Pages[1].Number;
                    Console.WriteLine($"Found \"{fragment.Text}\" on page {pageNumber}");
                }

                // Save the (unchanged) PDF as EPUB using explicit save options
                EpubSaveOptions epubOptions = new EpubSaveOptions
                {
                    // Use the Flow content recognition mode (valid enum path)
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };
                pdfDoc.Save(outputEpubPath, epubOptions);
            }

            Console.WriteLine($"EPUB file saved to '{outputEpubPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}