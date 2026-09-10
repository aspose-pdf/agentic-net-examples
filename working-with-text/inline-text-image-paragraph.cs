using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";
        const string imagePath  = "image.png";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document and ensure proper disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a single page
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Create a text fragment that will appear inline
            Aspose.Pdf.Text.TextFragment textFragment = new Aspose.Pdf.Text.TextFragment("Hello ");
            textFragment.IsInLineParagraph = true;
            textFragment.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica");
            textFragment.TextState.FontSize = 12;
            textFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Create an image fragment that will appear inline with the text
            Aspose.Pdf.Image imageFragment = new Aspose.Pdf.Image();
            imageFragment.File = imagePath;
            imageFragment.IsInLineParagraph = true;
            // Optional: set explicit size for the image
            imageFragment.FixWidth = 50;
            imageFragment.FixHeight = 50;

            // Add both fragments to the page's paragraph collection
            page.Paragraphs.Add(textFragment);
            page.Paragraphs.Add(imageFragment);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with inline text and image saved to '{outputPath}'.");
    }
}