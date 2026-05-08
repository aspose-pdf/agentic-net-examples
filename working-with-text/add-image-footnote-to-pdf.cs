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
        const string imagePath  = "footnote_image.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image for footnote not found: {imagePath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a text fragment that will contain the footnote reference
            TextFragment fragment = new TextFragment("See footnote");
            // Create the footnote (Note) and assign it to the fragment
            fragment.FootNote = new Note();

            // Create an Image object for the footnote content
            Image footnoteImage = new Image();
            footnoteImage.File = imagePath;

            // Add the image to the footnote's paragraph collection
            fragment.FootNote.Paragraphs.Add(footnoteImage);

            // Add the text fragment (with footnote) to the page
            page.Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with footnote: {outputPath}");
    }
}