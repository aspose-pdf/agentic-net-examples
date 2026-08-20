using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Remove existing headers and footers from all pages
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.Header = null;
                page.Footer = null;
            }

            // Create a new header
            HeaderFooter newHeader = new HeaderFooter();
            TextFragment headerText = new TextFragment("New Header Text");
            // Optional styling
            headerText.TextState.FontSize = 12;
            headerText.TextState.Font = FontRepository.FindFont("Helvetica");
            headerText.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            newHeader.Paragraphs.Add(headerText);

            // Create a new footer
            HeaderFooter newFooter = new HeaderFooter();
            TextFragment footerText = new TextFragment("Page ");
            // Add page number placeholder
            footerText.TextState.FontSize = 12;
            footerText.TextState.Font = FontRepository.FindFont("Helvetica");
            footerText.TextState.ForegroundColor = Aspose.Pdf.Color.Green;
            newFooter.Paragraphs.Add(footerText);
            // Add pagination artifact to display page numbers
            newFooter.Paragraphs.Add(new TextFragment("{page}"));

            // Assign the new header and footer to each page
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.Header = newHeader;
                page.Footer = newFooter;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Headers and footers updated. Saved to '{outputPath}'.");
    }
}