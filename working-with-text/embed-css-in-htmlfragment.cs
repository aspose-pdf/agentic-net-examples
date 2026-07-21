using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for output PDF
        const string outputPath = "HtmlFragmentWithCss.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define HTML with embedded CSS to control font, color, and spacing
            string htmlContent = @"
                <style>
                    .styledText {
                        font-family: Arial, Helvetica, sans-serif;   /* Font */
                        color: #0033CC;                               /* Text color */
                        margin: 15px 0 10px 0;                        /* Top, right, bottom, left margins */
                        line-height: 1.5;                             /* Line spacing */
                    }
                </style>
                <div class='styledText'>
                    This is a sample paragraph styled with embedded CSS.
                </div>";

            // Create an HtmlFragment from the HTML string
            HtmlFragment htmlFragment = new HtmlFragment(htmlContent);

            // Optionally set horizontal alignment for the fragment (centered)
            htmlFragment.HorizontalAlignment = HorizontalAlignment.Center;

            // Add the HtmlFragment to the page's paragraph collection
            page.Paragraphs.Add(htmlFragment);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with styled HtmlFragment saved to '{outputPath}'.");
    }
}