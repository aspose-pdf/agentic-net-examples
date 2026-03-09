using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "floatingbox_output.pdf";
        const string imagePath = "sample.png";

        // Verify that the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a FloatingBox with a specific width and height
            FloatingBox box = new FloatingBox(300, 150);
            // Set visual appearance of the box
            box.BackgroundColor = Aspose.Pdf.Color.LightGray;
            // Position the box on the page (coordinates are from bottom-left)
            box.Left = 50;
            box.Top = 700;

            // ----- Insert a text paragraph into the FloatingBox -----
            TextFragment tf = new TextFragment("This is a paragraph inside a FloatingBox.");
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Arial");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.DarkBlue;
            // Add the text fragment to the box's paragraph collection
            box.Paragraphs.Add(tf);

            // ----- Insert an image into the FloatingBox -----
            Image img = new Image();
            img.File = imagePath;
            // Optionally set image dimensions
            img.FixWidth = 100;
            img.FixHeight = 50;
            // Add the image to the box's paragraph collection
            box.Paragraphs.Add(img);

            // Add the configured FloatingBox to the page's paragraphs
            page.Paragraphs.Add(box);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with FloatingBox saved to '{outputPath}'.");
    }
}