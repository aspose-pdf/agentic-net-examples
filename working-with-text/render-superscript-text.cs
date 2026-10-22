using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "superscript.pdf";

        // Create a new PDF document and ensure it is disposed properly
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Normal text fragment
            TextFragment normal = new TextFragment("E = mc");
            normal.Position = new Position(100, 700);
            // Optionally set a base font size (default is 12)
            normal.TextState.FontSize = 12f;
            page.Paragraphs.Add(normal);

            // Superscript text fragment
            TextFragment superscript = new TextFragment("2");
            // Reduce font size for a typical superscript look
            superscript.TextState.FontSize = (float)(normal.TextState.FontSize * 0.7);
            // Position the superscript slightly higher than the baseline of the normal text
            superscript.Position = new Position(150, 720); // Y coordinate increased to raise the text
            page.Paragraphs.Add(superscript);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Superscript PDF saved to '{outputPath}'.");
    }
}
