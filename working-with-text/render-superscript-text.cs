using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class SuperscriptExample
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Normal text "H"
            TextFragment normal = new TextFragment("H");
            page.Paragraphs.Add(normal);

            // Superscript text "2"
            // Setting Superscript = true internally raises the text baseline,
            // which is equivalent to applying a positive TextRise.
            TextFragment superscript = new TextFragment("2");
            superscript.TextState.Superscript = true;
            page.Paragraphs.Add(superscript);

            // Normal text "O"
            TextFragment normal2 = new TextFragment("O");
            page.Paragraphs.Add(normal2);

            // Save the PDF
            string outputPath = "SuperscriptOutput.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}