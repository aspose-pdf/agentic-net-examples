using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for HtmlFragment (inherits from FormattedFragment)

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define CSS rules to control font, color, and spacing
            string css = @"
                <style>
                    p {
                        font-family: Arial, Helvetica, sans-serif; /* Font */
                        color: #003366;                           /* Text color */
                        line-height: 1.6;                         /* Line spacing */
                        margin-top: 12pt;                         /* Top margin */
                        margin-bottom: 12pt;                      /* Bottom margin */
                    }
                </style>";

            // HTML content that will be rendered using the above CSS
            string htmlContent = $"{css}<p>This paragraph is styled with embedded CSS.</p>";

            // Create an HtmlFragment with the combined CSS and HTML
            HtmlFragment htmlFragment = new HtmlFragment(htmlContent);

            // Optionally, customize HtmlLoadOptions for this fragment (e.g., base path for resources)
            // htmlFragment.HtmlLoadOptions = new HtmlLoadOptions { BasePath = "resources" };

            // Add the HtmlFragment to the page's paragraph collection
            page.Paragraphs.Add(htmlFragment);

            // Save the resulting PDF
            string outputPath = "StyledHtmlFragment.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}