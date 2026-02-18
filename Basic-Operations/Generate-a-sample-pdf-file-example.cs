using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class SamplePdfGenerator
{
    static void Main()
    {
        const string outputPath = "output.pdf";   // generic output name
        const string imagePath = "image.png";    // generic input image name

        try
        {
            // Verify that the image file exists before trying to open it
            if (!File.Exists(imagePath))
                throw new FileNotFoundException($"Image file not found: {imagePath}");

            // Use a using‑statement to ensure the Document is disposed properly
            using (var pdfDocument = new Document())
            {
                // Add a new page
                var page = pdfDocument.Pages.Add();

                // Add a text fragment
                var text = new TextFragment("Hello, Aspose.Pdf!");
                page.Paragraphs.Add(text);

                // Add the image – keep the stream open only while the image is being added
                using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    var image = new Image
                    {
                        ImageStream = imgStream,
                        FixWidth = 200,
                        FixHeight = 200
                    };
                    page.Paragraphs.Add(image);
                }

                // Save the PDF
                pdfDocument.Save(outputPath);
                Console.WriteLine($"PDF saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Centralised error handling – works on all platforms
            Console.Error.WriteLine($"Error generating PDF: {ex.Message}");
        }
    }
}
