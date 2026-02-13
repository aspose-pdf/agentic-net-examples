using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPdfPath = "output.pdf";
        const string imagePath = "sample.jpg";

        try
        {
            // Verify that the image file exists before proceeding
            if (!File.Exists(imagePath))
            {
                Console.Error.WriteLine($"Image file not found: {imagePath}");
                return;
            }

            // Create a new blank PDF document
            Document pdfDocument = new Document();
            Page page = pdfDocument.Pages.Add();

            // Add a text fragment to the page
            TextFragment text = new TextFragment("Hello, Aspose.Pdf!")
            {
                TextState = { FontSize = 24, ForegroundColor = Color.Blue }
            };
            page.Paragraphs.Add(text);

            // Load the image into a MemoryStream so the stream stays open until the PDF is saved
            using (MemoryStream imgStream = new MemoryStream(File.ReadAllBytes(imagePath)))
            {
                Image image = new Image
                {
                    ImageStream = imgStream,
                    FixWidth = 200,
                    FixHeight = 200
                };
                page.Paragraphs.Add(image);

                // Save the PDF while the image stream is still alive
                pdfDocument.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF created successfully: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating PDF: {ex.Message}");
        }
    }
}