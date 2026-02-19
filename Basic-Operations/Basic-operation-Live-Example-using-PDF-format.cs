using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";
        const string imagePath = "sample.jpg";

        Document pdfDocument = null;
        FileStream imgStream = null;

        try
        {
            // Create a new PDF document
            pdfDocument = new Document();
            Page page = pdfDocument.Pages.Add();

            // Add a text fragment
            TextFragment text = new TextFragment("Hello, Aspose.Pdf!");
            text.TextState.FontSize = 24;
            text.TextState.ForegroundColor = Color.Blue; // Aspose.Pdf.Color
            page.Paragraphs.Add(text);

            // Add an image if the file exists. Keep the stream open until after the document is saved.
            if (File.Exists(imagePath))
            {
                imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                Image img = new Image
                {
                    ImageStream = imgStream,
                    FixWidth = page.PageInfo.Width - 50,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                page.Paragraphs.Add(img);
            }

            // Save the PDF document
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Dispose resources after the document has been saved
            imgStream?.Dispose();
            pdfDocument?.Dispose();
        }
    }
}
