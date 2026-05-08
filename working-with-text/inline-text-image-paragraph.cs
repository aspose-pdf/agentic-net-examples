using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "inline_text_image.pdf";
        const string imagePath  = "logo.png"; // replace with a valid image file path

        // Ensure the image file exists before proceeding
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // ----- Text fragment (inline) -----
            TextFragment textFragment = new TextFragment("Hello ");
            textFragment.IsInLineParagraph = true;                     // make it inline
            textFragment.TextState.FontSize = 12;
            textFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            textFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // ----- Image fragment (inline) -----
            // Aspose.Pdf does not have an ImageFragment class; use Image instead.
            Image imageFragment = new Image();
            imageFragment.File = imagePath;                            // source image file
            imageFragment.IsInLineParagraph = true;                    // make it inline
            imageFragment.FixWidth = 50;                               // optional sizing
            imageFragment.FixHeight = 20;

            // Add the text and image fragments to the page in sequence.
            // Because both are marked as inline, they will appear on the same line.
            page.Paragraphs.Add(textFragment);
            page.Paragraphs.Add(imageFragment);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with inline text and image saved to '{outputPath}'.");
    }
}