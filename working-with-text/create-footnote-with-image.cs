using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_footnote.pdf";
        const string footnoteImagePath = "footnote_image.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(footnoteImagePath))
        {
            Console.Error.WriteLine($"Footnote image not found: {footnoteImagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a text fragment that will contain the footnote reference
            TextFragment tf = new TextFragment("Sample text with footnote reference[1]");
            tf.Position = new Position(100, 700); // place the text on the page

            // Create a footnote (Note) and attach it to the text fragment
            Note footnote = new Note();
            footnote.Text = "This is the footnote text.";
            // Add an image to the footnote's paragraph collection
            Image footnoteImg = new Image();
            footnoteImg.File = footnoteImagePath;
            footnote.Paragraphs.Add(footnoteImg);
            // Assign the footnote to the fragment
            tf.FootNote = footnote;

            // Add the fragment (with footnote) to the page
            page.Paragraphs.Add(tf);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with footnote: {outputPath}");
    }
}