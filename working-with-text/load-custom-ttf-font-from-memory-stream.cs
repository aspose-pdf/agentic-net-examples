using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, output PDF, and the custom TrueType font file
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string fontFilePath  = "custom.ttf";

        // Ensure the custom font file exists – otherwise the example cannot run
        if (!File.Exists(fontFilePath))
        {
            Console.WriteLine($"Font file not found: {fontFilePath}");
            return;
        }

        // Load the existing PDF document if it exists; otherwise create a new one
        Document pdfDoc;
        if (File.Exists(inputPdfPath))
        {
            pdfDoc = new Document(inputPdfPath);
        }
        else
        {
            pdfDoc = new Document();
            // Add a blank page so we have a place to put the text
            pdfDoc.Pages.Add();
        }

        // Use a using block to guarantee disposal of the document
        using (pdfDoc)
        {
            // Read the font file into a byte array and wrap it in a MemoryStream
            byte[] fontBytes = File.ReadAllBytes(fontFilePath);
            using (MemoryStream fontStream = new MemoryStream(fontBytes))
            {
                // Open the custom font from the memory stream (TTF format)
                Font customFont = FontRepository.OpenFont(fontStream, FontTypes.TTF);
                // Ensure the font will be embedded in the PDF
                customFont.IsEmbedded = true;

                // Create a TextFragment with the desired content
                TextFragment fragment = new TextFragment("Hello with custom font!");

                // Assign the custom font to the fragment's TextState
                fragment.TextState.Font = customFont;
                fragment.TextState.FontSize = 14; // example size

                // Position the text on the page (coordinates are in points)
                fragment.Position = new Position(100, 700);

                // Add the fragment to the first page of the document
                pdfDoc.Pages[1].Paragraphs.Add(fragment);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"PDF saved to {outputPdfPath}");
        }
    }
}
