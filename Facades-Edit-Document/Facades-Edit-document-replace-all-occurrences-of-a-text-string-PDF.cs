using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Text to find and its replacement
        const string searchText = "OldString";
        const string replaceText = "NewString";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdf);

            // Use TextFragmentAbsorber to locate all occurrences of the search text
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            pdfDocument.Pages.Accept(absorber);

            // Replace each found fragment with the new text
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = replaceText;
            }

            // Save the modified PDF
            pdfDocument.Save(outputPdf);

            Console.WriteLine($"All occurrences of \"{searchText}\" have been replaced with \"{replaceText}\".");
            Console.WriteLine($"Modified PDF saved to: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
