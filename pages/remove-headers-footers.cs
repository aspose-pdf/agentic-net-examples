using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

namespace AsposePdfHeaderFooterExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // -----------------------------------------------------------------
            // Step 1: Create a sample PDF that already contains a header and footer
            // -----------------------------------------------------------------
            using (Document sampleDoc = new Document())
            {
                // Add a single page
                Page samplePage = sampleDoc.Pages.Add();

                // Existing header
                HeaderFooter oldHeader = new HeaderFooter();
                oldHeader.Paragraphs.Add(new TextFragment("Old Header"));
                samplePage.Header = oldHeader;

                // Existing footer
                HeaderFooter oldFooter = new HeaderFooter();
                oldFooter.Paragraphs.Add(new TextFragment("Old Footer"));
                samplePage.Footer = oldFooter;

                // Save the sample PDF
                sampleDoc.Save("sample.pdf");
            }

            // ---------------------------------------------------------------
            // Step 2: Load the PDF, remove existing headers/footers, add new ones
            // ---------------------------------------------------------------
            using (Document doc = new Document("sample.pdf"))
            {
                // Remove any existing header/footer from every page
                foreach (Page pg in doc.Pages)
                {
                    pg.Header = null;
                    pg.Footer = null;
                }

                // -----------------------------------------------------------
                // Create a new header
                // -----------------------------------------------------------
                HeaderFooter newHeader = new HeaderFooter();
                newHeader.Margin = new MarginInfo { Top = 10 };
                TextFragment headerFragment = new TextFragment("New Header - Page $p of $P");
                headerFragment.TextState.Font = FontRepository.FindFont("Helvetica");
                headerFragment.TextState.FontSize = 12;
                headerFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
                newHeader.Paragraphs.Add(headerFragment);

                // -----------------------------------------------------------
                // Create a new footer
                // -----------------------------------------------------------
                HeaderFooter newFooter = new HeaderFooter();
                newFooter.Margin = new MarginInfo { Bottom = 10 };
                TextFragment footerFragment = new TextFragment("New Footer - Confidential");
                footerFragment.TextState.Font = FontRepository.FindFont("Helvetica");
                footerFragment.TextState.FontSize = 10;
                footerFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                newFooter.Paragraphs.Add(footerFragment);

                // Assign the new header/footer to all pages
                foreach (Page pg in doc.Pages)
                {
                    pg.Header = newHeader;
                    pg.Footer = newFooter;
                }

                // Update pagination symbols (e.g., $p, $P) in the header/footer
                doc.Pages.UpdatePagination();

                // Save the modified PDF
                doc.Save("output.pdf");
            }
        }
    }
}
