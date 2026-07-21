using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "ligatures_output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a TextFragment containing characters that could form ligatures (e.g., "office")
            TextFragment tf = new TextFragment("office");

            // Configure the TextState (the object returned is mutable)
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 24;
            // Ligatures property does not exist in Aspose.Pdf.Text.TextFragmentState.
            // If you need to adjust spacing, use CharacterSpacing or WordSpacing instead.
            // tf.TextState.CharacterSpacing = 0; // optional adjustment
            tf.TextState.ForegroundColor = Color.Black;

            // Add the TextFragment to the page's paragraph collection
            page.Paragraphs.Add(tf);

            // Save the PDF to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with text saved to '{outputPath}'.");
    }
}
