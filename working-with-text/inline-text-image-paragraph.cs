using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the output PDF and the image to embed
        const string outputPdfPath = "inline_text_image.pdf";
        const string imagePath     = "sample.png";

        // Verify that the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // First text fragment (before the image)
            TextFragment tfBefore = new TextFragment("Hello ");
            tfBefore.TextState.Font = FontRepository.FindFont("Helvetica");
            tfBefore.TextState.FontSize = 12;
            tfBefore.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            page.Paragraphs.Add(tfBefore);

            // Inline image fragment
            Image img = new Image();
            img.File = imagePath;               // Load image from file
            img.IsInLineParagraph = true;       // Make the image appear inline with surrounding text
            img.FixWidth = 30;                  // Optional: set displayed width
            img.FixHeight = 30;                 // Optional: set displayed height
            page.Paragraphs.Add(img);

            // Second text fragment (after the image)
            TextFragment tfAfter = new TextFragment("world!");
            tfAfter.TextState.Font = FontRepository.FindFont("Helvetica");
            tfAfter.TextState.FontSize = 12;
            tfAfter.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            page.Paragraphs.Add(tfAfter);

            // Save the resulting PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with inline text and image saved to '{outputPdfPath}'.");
    }
}