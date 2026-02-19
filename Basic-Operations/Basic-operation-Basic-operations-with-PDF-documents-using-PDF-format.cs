using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        Document pdfDocument = new Document();

        // Add a blank page
        Page page = pdfDocument.Pages.Add();

        // Insert a text fragment
        TextFragment text = new TextFragment("Hello Aspose.Pdf!");
        text.TextState.FontSize = 24;
        text.TextState.Font = FontRepository.FindFont("Arial");
        text.TextState.ForegroundColor = Color.Blue;
        page.Paragraphs.Add(text);

        // Insert an image if the file exists
        string imagePath = "sample.png";
        if (File.Exists(imagePath))
        {
            using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                Image img = new Image
                {
                    ImageStream = imgStream,
                    FixWidth = 200,
                    FixHeight = 200,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                page.Paragraphs.Add(img);
            }
        }

        // Save the PDF (uses the provided document-save rule)
        pdfDocument.Save("output.pdf");
    }
}