using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the OpenType (OTF) font file (relative to the executable folder)
        const string fontPath = "myfont.otf";
        // Output PDF file path
        const string outputPath = "styled.pdf";

        // Resolve the full path and verify the file exists
        string fontFullPath = Path.GetFullPath(fontPath);
        Font customFont;
        if (File.Exists(fontFullPath))
        {
            // Load the custom OTF font from file and embed it
            customFont = FontRepository.OpenFont(fontFullPath);
            customFont.IsEmbedded = true;
        }
        else
        {
            // Fallback to a system font if the OTF file is missing
            Console.WriteLine($"Font file not found at '{fontFullPath}'. Falling back to Arial.");
            customFont = FontRepository.FindFont("Arial");
        }

        // Create a new PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment tf = new TextFragment("Hello, Aspose PDF with OTF font!");

            // Apply the custom font and additional styling
            tf.TextState.Font = customFont;               // Set the OTF (or fallback) font
            tf.TextState.FontSize = 24;                   // Set font size
            tf.TextState.ForegroundColor = Color.Blue;   // Set text color (Aspose.Pdf.Color)

            // Add the text fragment to the page
            page.Paragraphs.Add(tf);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
