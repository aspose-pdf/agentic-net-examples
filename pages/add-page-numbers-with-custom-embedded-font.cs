using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string ttfPath = "custom.ttf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(ttfPath))
        {
            Console.Error.WriteLine($"TrueType font not found: {ttfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Load the external TTF font and ensure it is embedded
            Font customFont = FontRepository.FindFont(ttfPath);
            customFont.IsEmbedded = true;

            // Embed any standard Type1 fonts that might be used
            doc.EmbedStandardFonts = true;

            // Create a page number stamp (default format "#")
            PageNumberStamp stamp = new PageNumberStamp();
            stamp.TextState.Font = customFont;
            stamp.TextState.FontSize = 12; // desired font size
            stamp.TextState.ForegroundColor = Color.Black;

            // Position the stamp at the bottom center of each page
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment = VerticalAlignment.Bottom;
            stamp.BottomMargin = 20; // distance from the bottom edge

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Page numbers added with custom font to '{outputPdf}'.");
    }
}
