using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";
        const string imagePath  = "sample.png";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // First text fragment (inline)
            TextFragment txt1 = new TextFragment("Hello ");
            txt1.IsInLineParagraph = true;
            txt1.TextState.FontSize = 12;
            txt1.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica");
            txt1.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Image fragment (inline)
            Image img = new Image
            {
                File = imagePath,
                IsInLineParagraph = true,
                // Optional: set explicit size in points (1 point = 1/72 inch)
                // Width = 50,
                // Height = 50
            };

            // Second text fragment (inline)
            TextFragment txt2 = new TextFragment(" world!");
            txt2.IsInLineParagraph = true;
            txt2.TextState.FontSize = 12;
            txt2.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica");
            txt2.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add fragments to the page in the desired order
            page.Paragraphs.Add(txt1);
            page.Paragraphs.Add(img);
            page.Paragraphs.Add(txt2);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created successfully: {outputPath}");
    }
}