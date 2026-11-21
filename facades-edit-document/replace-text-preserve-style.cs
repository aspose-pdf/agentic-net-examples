using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string searchText = "OldText";
        const string replaceText = "NewText";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Create a TextFragmentAbsorber to locate the text to replace
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);

                // Search all pages
                doc.Pages.Accept(absorber);

                // Iterate over each found fragment
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // Preserve the original text style
                    TextState originalState = fragment.TextState;

                    // Replace the text content
                    fragment.Text = replaceText;

                    // Reapply the original style (font, size, color)
                    fragment.TextState.Font = originalState.Font;
                    fragment.TextState.FontSize = originalState.FontSize;
                    fragment.TextState.ForegroundColor = originalState.ForegroundColor;
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Text replacement completed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}