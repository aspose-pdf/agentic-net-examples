using System;
using System.IO;
using Aspose.Pdf;               // Core PDF classes
using Aspose.Pdf.Text;          // Required for HtmlFragment

class Program
{
    static void Main()
    {
        const string outputPath = "styled_html.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a blank page to the document
            Aspose.Pdf.Page page = doc.Pages.Add();

            // HTML content with embedded CSS to control font, color, and spacing
            string html = @"
                <html>
                <head>
                    <style>
                        .title {
                            font-family: Arial;
                            font-size: 24pt;
                            color: #2E86C1;
                            margin-bottom: 10px;
                        }
                        .paragraph {
                            font-family: Verdana;
                            font-size: 12pt;
                            color: #555555;
                            line-height: 1.5;
                        }
                    </style>
                </head>
                <body>
                    <div class='title'>Sample Title</div>
                    <div class='paragraph'>
                        This is a paragraph styled with CSS. It demonstrates font, color, and spacing control.
                    </div>
                </body>
                </html>";

            // Create an HtmlFragment using the HTML string
            Aspose.Pdf.HtmlFragment htmlFragment = new Aspose.Pdf.HtmlFragment(html);

            // Add the HtmlFragment to the page's paragraph collection
            page.Paragraphs.Add(htmlFragment);

            // Save the PDF to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with styled HTML saved to '{outputPath}'.");
    }
}