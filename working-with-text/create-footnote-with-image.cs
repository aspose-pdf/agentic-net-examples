using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the image that will appear in the footnote
        const string footnoteImagePath = "footnote.png";

        // Ensure the image file exists before proceeding
        if (!File.Exists(footnoteImagePath))
        {
            Console.Error.WriteLine($"Image not found: {footnoteImagePath}");
            return;
        }

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a text fragment that will contain the footnote reference
            TextFragment text = new TextFragment("This is a paragraph with a footnote reference.");

            // Create the footnote (Note) object
            Note footnote = new Note();

            // Create an Image object and set its source file
            Image footnoteImage = new Image
            {
                File = footnoteImagePath
            };

            // Add the image to the footnote's Paragraphs collection
            footnote.Paragraphs.Add(footnoteImage);

            // Assign the footnote to the TextFragment
            text.FootNote = footnote;

            // Add the TextFragment (with its footnote) to the page's content
            page.Paragraphs.Add(text);

            // Save the resulting PDF
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with footnote and image saved as 'output.pdf'.");
    }
}