using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string imagePath = "sample.png";
        const string outputPath = "inline_text_image.pdf";

        // Verify the image exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a blank page
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Create a text fragment – this will be part of an inline paragraph
            Aspose.Pdf.Text.TextFragment textFragment = new Aspose.Pdf.Text.TextFragment("Hello ");
            textFragment.IsInLineParagraph = true;               // Enable inline flow
            textFragment.TextState.FontSize = 12;
            textFragment.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica");
            textFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Create an image fragment – also inline
            Aspose.Pdf.Image imageFragment = new Aspose.Pdf.Image();
            imageFragment.File = imagePath;
            imageFragment.IsInLineParagraph = true;              // Enable inline flow
            // Optionally set image dimensions (width/height in points)
            imageFragment.FixWidth = 50;   // 50 points wide
            imageFragment.FixHeight = 50;  // 50 points high

            // Add the fragments to the page in the desired order
            page.Paragraphs.Add(textFragment);
            page.Paragraphs.Add(imageFragment);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with inline text and image saved to '{outputPath}'.");
    }
}