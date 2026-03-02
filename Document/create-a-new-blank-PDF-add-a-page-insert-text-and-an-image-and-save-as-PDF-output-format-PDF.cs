using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the output PDF and the image to embed.
        const string outputPdfPath = "output.pdf";
        const string imagePath    = "sample_image.png";

        // Ensure the image file exists before proceeding.
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new blank PDF document.
        using (Document doc = new Document())
        {
            // Add a single page to the document.
            Page page = doc.Pages.Add();

            // ----- Insert Text -----
            // Create a text fragment with the desired content.
            TextFragment text = new TextFragment("Hello, Aspose.Pdf!");
            // Optional: set font, size, and color.
            text.TextState.Font = FontRepository.FindFont("Helvetica");
            text.TextState.FontSize = 14;
            text.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            // Add the text fragment to the page's paragraph collection.
            page.Paragraphs.Add(text);

            // ----- Insert Image -----
            // Create an Image object (parameterless constructor) and assign the file.
            Image img = new Image();
            img.File = imagePath;
            // Optionally set image dimensions or alignment.
            img.FixWidth  = 200; // width in points
            img.FixHeight = 150; // height in points
            // Add the image to the same page.
            page.Paragraphs.Add(img);

            // Save the document as a PDF file.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF created successfully: {outputPdfPath}");
    }
}