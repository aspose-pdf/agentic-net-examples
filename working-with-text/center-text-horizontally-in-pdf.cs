using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "centered_text.pdf";

        Document doc;
        if (File.Exists(inputPath))
        {
            doc = new Document(inputPath);
        }
        else
        {
            doc = new Document();
            doc.Pages.Add();
        }

        Page page = doc.Pages[1];

        // Create a TextFragment with the desired content
        TextFragment fragment = new TextFragment("Centered Text on the Page");

        // Configure TextState properties (TextState is read‑only, modify its members)
        fragment.TextState.Font = FontRepository.FindFont("Helvetica");
        fragment.TextState.FontSize = 24;
        fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

        // Center the text horizontally (HorizontalAlignment belongs to TextFragment)
        fragment.HorizontalAlignment = HorizontalAlignment.Center;

        // Position the fragment roughly in the middle of the page
        fragment.Position = new Position(0, page.PageInfo.Height / 2);

        page.Paragraphs.Add(fragment);

        doc.Save(outputPath);
        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
