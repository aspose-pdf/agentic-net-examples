using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // source PDF
        const string outputPath = "output.pdf";     // destination PDF
        const string fontPath   = "custom.ttf";     // external TrueType font file

        // Verify required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
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
            // Load the custom TTF font and mark it for embedding
            Font customFont = FontRepository.FindFont(fontPath);
            customFont.IsEmbedded = true;

            // Ensure standard Type1 fonts are also embedded if used
            doc.EmbedStandardFonts = true;

            // Create a page number stamp
            PageNumberStamp pageNumberStamp = new PageNumberStamp();
            pageNumberStamp.StartingNumber = 1;                     // start numbering at 1
            pageNumberStamp.TextState.Font = customFont;            // apply custom font
            pageNumberStamp.TextState.FontSize = 12;                // desired font size
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin = 20;                      // distance from bottom edge

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added with custom font to '{outputPath}'.");
    }
}