using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";
        const string searchText = "Sample Text"; // text to rotate

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use constructor inside using)
            using (Document doc = new Document(inputPath))
            {
                // Find the text fragment containing the target phrase
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
                // Apply absorber to the first page (adjust page index as needed)
                doc.Pages[1].Accept(absorber);

                // Ensure at least one fragment was found
                if (absorber.TextFragments.Count > 0)
                {
                    // Rotate the first found fragment by 45 degrees
                    TextFragment fragment = absorber.TextFragments[1];
                    fragment.TextState.Rotation = 45; // rotation in degrees
                }
                else
                {
                    Console.WriteLine($"Text \"{searchText}\" not found on page 1.");
                }

                // Save the modified document (lifecycle rule: use Save inside using)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Document saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}