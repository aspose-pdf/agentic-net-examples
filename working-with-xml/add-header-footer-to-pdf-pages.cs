using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // ------------------------------------------------------------
            // Create a header template
            // ------------------------------------------------------------
            HeaderFooter header = new HeaderFooter();

            // TextFragment with replaceable symbols $p (current page) and $P (total pages)
            TextFragment headerFragment = new TextFragment("Header – Page $p of $P");
            headerFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            headerFragment.TextState.FontSize = 12;
            headerFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
            headerFragment.HorizontalAlignment = HorizontalAlignment.Center;

            header.Paragraphs.Add(headerFragment);

            // ------------------------------------------------------------
            // Create a footer template
            // ------------------------------------------------------------
            HeaderFooter footer = new HeaderFooter();

            TextFragment footerFragment = new TextFragment("Footer – Confidential");
            footerFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            footerFragment.TextState.FontSize = 10;
            footerFragment.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGray;
            footerFragment.HorizontalAlignment = HorizontalAlignment.Center;

            footer.Paragraphs.Add(footerFragment);

            // ------------------------------------------------------------
            // Assign header and footer to every page
            // ------------------------------------------------------------
            foreach (Page page in doc.Pages)
            {
                page.Header = header;
                page.Footer = footer;
            }

            // Update pagination symbols ($p, $P) in all headers/footers
            doc.Pages.UpdatePagination();

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with header/footer saved to '{outputPdf}'.");
    }
}