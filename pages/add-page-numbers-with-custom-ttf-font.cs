using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_numbered.pdf";
        const string fontPath   = "custom_font.ttf"; // path to external TTF file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Load the external TrueType font and ensure it will be embedded
            Font customFont = FontRepository.FindFont(fontPath);
            customFont.IsEmbedded = true;

            // Required for embedding standard Type1 fonts if they are used later
            doc.EmbedStandardFonts = true;

            // Create a page number stamp with default format "#"
            PageNumberStamp pageNumberStamp = new PageNumberStamp();
            pageNumberStamp.StartingNumber = 1;                     // first page number
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin = 20;                     // distance from bottom edge
            pageNumberStamp.TextState.Font = customFont;           // apply custom font
            pageNumberStamp.TextState.FontSize = 12;               // desired font size
            pageNumberStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added with custom font. Output saved to '{outputPath}'.");
    }
}