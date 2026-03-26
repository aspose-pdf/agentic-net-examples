using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Create a footer that will contain the page number and total page count placeholders
            HeaderFooter footer = new HeaderFooter();

            TextFragment footerText = new TextFragment("Page $p of $P");
            footerText.TextState.Font = FontRepository.FindFont("Helvetica");
            footerText.TextState.FontSize = 12;
            footerText.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
            footerText.HorizontalAlignment = HorizontalAlignment.Center;

            footer.Paragraphs.Add(footerText);

            // Assign the footer to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.Footer = footer;
            }

            // Update the placeholders ($p and $P) with actual values
            doc.Pages.UpdatePagination();

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with footer to '{outputPath}'.");
    }
}