using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "footnote_image.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image for footnote not found: {imagePath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the footnote will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Create a text fragment that will contain the footnote reference
            TextFragment tf = new TextFragment("Sample text with footnote reference[1]");
            tf.Position = new Position(100, 700); // place the text on the page

            // Create a footnote (Note) and attach it to the TextFragment
            Note footNote = new Note();
            footNote.Text = "This is the footnote content.";
            // Add an image to the footnote's Paragraphs collection
            Image footImg = new Image();
            footImg.File = imagePath;
            footNote.Paragraphs.Add(footImg);

            // Assign the footnote to the TextFragment
            tf.FootNote = footNote;

            // Add the TextFragment (with its footnote) to the page
            page.Paragraphs.Add(tf);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with footnote saved to '{outputPdf}'.");
    }
}