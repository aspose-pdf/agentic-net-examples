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
            // ------------------------------------------------------------
            // Remove any existing headers and footers from all pages
            // ------------------------------------------------------------
            foreach (Page page in doc.Pages)
            {
                page.Header = null;
                page.Footer = null;
            }

            // ------------------------------------------------------------
            // Create a new header
            // ------------------------------------------------------------
            HeaderFooter newHeader = new HeaderFooter();
            TextFragment headerTf = new TextFragment("My New Header");
            headerTf.TextState.FontSize = 12;
            headerTf.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
            newHeader.Paragraphs.Add(headerTf);
            // Optional margin for the header
            newHeader.Margin = new MarginInfo { Top = 10 };

            // ------------------------------------------------------------
            // Create a new footer (with page number placeholders)
            // ------------------------------------------------------------
            HeaderFooter newFooter = new HeaderFooter();
            // $p = current page number, $P = total pages
            TextFragment footerTf = new TextFragment("Page $p of $P");
            footerTf.TextState.FontSize = 10;
            footerTf.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
            newFooter.Paragraphs.Add(footerTf);
            // Optional margin for the footer
            newFooter.Margin = new MarginInfo { Bottom = 10 };

            // ------------------------------------------------------------
            // Assign the new header and footer to each page
            // ------------------------------------------------------------
            foreach (Page page in doc.Pages)
            {
                page.Header = newHeader;
                page.Footer = newFooter;
            }

            // Update pagination placeholders ($p, $P)
            doc.Pages.UpdatePagination();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
