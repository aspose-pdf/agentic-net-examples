using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputEpubPath = "output.epub";
        const string searchPhrase = "sample text"; // phrase to search

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

                // Perform the search on the whole document
                absorber.Visit(pdfDoc);

                // Output the found fragments
                if (absorber.TextFragments.Count > 0)
                {
                    Console.WriteLine($"Found {absorber.TextFragments.Count} occurrence(s) of \"{searchPhrase}\":");
                    int index = 1;
                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        Console.WriteLine($"{index++}: Page {fragment.Page.Number}, Text = \"{fragment.Text}\"");
                    }
                }
                else
                {
                    Console.WriteLine($"No occurrences of \"{searchPhrase}\" were found.");
                }

                // Save the document as EPUB using explicit save options
                EpubSaveOptions epubOptions = new EpubSaveOptions
                {
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };
                pdfDoc.Save(outputEpubPath, epubOptions);
                Console.WriteLine($"Document saved as EPUB to \"{outputEpubPath}\".");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}