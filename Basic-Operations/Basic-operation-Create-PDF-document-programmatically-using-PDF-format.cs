using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        try
        {
            // Create a new empty PDF document
            Document pdfDocument = new Document();

            // Add a new page to the document
            Page page = pdfDocument.Pages.Add();

            // Insert a text fragment onto the page
            TextFragment text = new TextFragment("Hello, Aspose.Pdf!")
            {
                TextState = { FontSize = 24, Font = FontRepository.FindFont("Arial"), ForegroundColor = Color.Black }
            };
            // Position the text near the top‑left corner
            text.Position = new Position(0, page.PageInfo.Height - 50);
            page.Paragraphs.Add(text);

            // Insert an image onto the page (if the file exists)
            const string imagePath = "sample.jpg"; // replace with a valid image file path
            if (File.Exists(imagePath))
            {
                using (MemoryStream imgStream = new MemoryStream(File.ReadAllBytes(imagePath)))
                {
                    Image image = new Image
                    {
                        ImageStream = imgStream,
                        FixWidth = 200,
                        FixHeight = 200,
                        // Absolute positioning via the Position property is not available in the current Aspose.Pdf version.
                        // Use alignment properties or omit positioning to place the image with default layout.
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    page.Paragraphs.Add(image);
                }
            }
            else
            {
                Console.Error.WriteLine($"Image file not found: {imagePath}");
            }

            // Save the PDF (regardless of whether the image was added)
            pdfDocument.Save("output.pdf");
            Console.WriteLine("PDF created successfully: output.pdf");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
