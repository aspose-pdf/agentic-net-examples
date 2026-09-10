using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // For TextState if needed (not used here)

class EmbedCssInHtmlFragment
{
    static void Main()
    {
        // Path for the output PDF
        const string outputPath = "HtmlWithCss.pdf";

        // HTML content with embedded CSS to control font, color, and spacing
        string html = @"
            <html>
                <head>
                    <style>
                        /* CSS rules applied to the paragraph */
                        p {
                            font-family: Arial, Helvetica, sans-serif;   /* Font */
                            color: #FF0000;                               /* Text color (red) */
                            margin-top: 15px;                             /* Top margin */
                            margin-bottom: 15px;                          /* Bottom margin */
                            line-height: 1.5;                             /* Line spacing */
                        }
                    </style>
                </head>
                <body>
                    <p>This paragraph is styled using embedded CSS.</p>
                </body>
            </html>";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create an HtmlFragment from the HTML string
            HtmlFragment htmlFragment = new HtmlFragment(html);

            // Optional: set horizontal alignment of the fragment (centered)
            htmlFragment.HorizontalAlignment = HorizontalAlignment.Center;

            // Add the HtmlFragment to the page's paragraph collection
            page.Paragraphs.Add(htmlFragment);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with embedded CSS saved to '{outputPath}'.");
    }
}