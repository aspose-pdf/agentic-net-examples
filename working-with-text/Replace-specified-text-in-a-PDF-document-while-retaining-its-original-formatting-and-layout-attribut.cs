using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfTextReplacer
{
    /// <summary>
    /// Replaces all occurrences of <paramref name="searchText"/> with <paramref name="replaceText"/>
    /// in the PDF located at <paramref name="inputPath"/> and saves the result to <paramref name="outputPath"/>.
    /// The original formatting (font, size, color, etc.) of the replaced text is preserved.
    /// </summary>
    public static void ReplaceText(string inputPath, string outputPath, string searchText, string replaceText)
    {
        if (!File.Exists(inputPath))
            throw new FileNotFoundException($"Input PDF not found: {inputPath}");

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber that searches for the specified phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);

            // Accept the absorber for all pages – it will collect all matching fragments
            doc.Pages.Accept(absorber);

            // Iterate over each found fragment and replace its text.
            // The TextFragment retains its original TextState (font, size, color, etc.).
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = replaceText;
            }

            // Save the modified document
            doc.Save(outputPath);
        }
    }

    // Example usage
    static void Main()
    {
        string inputPdf  = @"C:\Docs\sample.pdf";
        string outputPdf = @"C:\Docs\sample_modified.pdf";
        string search    = "Hello World";
        string replace   = "Hi Universe";

        try
        {
            ReplaceText(inputPdf, outputPdf, search, replace);
            Console.WriteLine($"Text replacement completed. Saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}