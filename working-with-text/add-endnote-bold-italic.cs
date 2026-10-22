using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Extract all text from the document (optional demonstration)
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                Console.WriteLine($"Extracted text length: {absorber.Text.Length}");

                // Add a new page at the end for the endnote
                Page endPage = doc.Pages.Add();

                // Create a TextFragment containing the endnote text
                TextFragment endNote = new TextFragment("Endnote: This is a sample endnote with bold and italic styling.");

                // Apply bold and italic formatting via TextState
                endNote.TextState.FontStyle = FontStyles.Bold | FontStyles.Italic;
                endNote.TextState.FontSize = 12; // optional size
                // Position the fragment on the page (optional)
                endNote.Position = new Position(50, 750);

                // Add the TextFragment to the page's paragraph collection
                endPage.Paragraphs.Add(endNote);

                // Save the modified PDF
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