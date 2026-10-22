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
        const string imagePath = "footnote_image.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (1‑based indexing)
            Aspose.Pdf.Page page = doc.Pages[1];

            // Create a footnote text fragment
            TextFragment footnote = new TextFragment("1. This is a footnote reference.");
            footnote.TextState.FontSize = 8;
            footnote.TextState.Font = FontRepository.FindFont("Arial");
            footnote.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

            // Add the footnote to the page's paragraph collection
            page.Paragraphs.Add(footnote);

            // Create an image object and load the image file
            Image img = new Image();
            img.ImageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            // Optional: set image dimensions (points)
            img.FixWidth = 50;
            img.FixHeight = 50;

            // Insert the image after the footnote
            page.Paragraphs.Add(img);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}