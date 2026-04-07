using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // required for HtmlLoadOptions

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages.Add();

            // Define CSS rules to control font, color, and spacing
            string css = @"
                <style>
                    .custom {
                        font-family: 'Helvetica';
                        font-size: 14pt;
                        color: #003366;
                        line-height: 1.5;      /* line spacing */
                        margin-top: 10pt;
                        margin-bottom: 10pt;
                    }
                </style>";

            // HTML content that uses the CSS class
            string htmlContent = $@"
                {css}
                <p class='custom'>
                    This is a sample paragraph rendered from an HtmlFragment.
                    The font, color, and spacing are controlled by the embedded CSS.
                </p>";

            // Create the HtmlFragment with the combined CSS and HTML
            HtmlFragment htmlFragment = new HtmlFragment(htmlContent);

            // Optional: specify custom HtmlLoadOptions if needed (null uses defaults)
            htmlFragment.HtmlLoadOptions = new HtmlLoadOptions();

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(htmlFragment);

            // Save the resulting PDF
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with embedded CSS created successfully.");
    }
}