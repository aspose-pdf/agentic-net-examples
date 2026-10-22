using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";
        const string imagePath = "image.png";

        // Verify that the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        try
        {
            // Create a new PDF document
            using (Document doc = new Document())
            {
                // Add a page to the document
                Page page = doc.Pages.Add();

                // First text fragment
                TextFragment textBefore = new TextFragment("This is inline image: ");
                page.Paragraphs.Add(textBefore);

                // Image fragment (inline with the text)
                Image img = new Image();
                using (FileStream imgStream = File.OpenRead(imagePath))
                {
                    img.ImageStream = imgStream;
                    // Optional: set fixed dimensions for the image
                    img.FixWidth = 50;
                    img.FixHeight = 50;
                }
                page.Paragraphs.Add(img);

                // Optional trailing text after the image
                TextFragment textAfter = new TextFragment(" end of paragraph.");
                page.Paragraphs.Add(textAfter);

                // Save the PDF document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
