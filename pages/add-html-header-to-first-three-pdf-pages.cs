using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Define the HTML header with embedded CSS
            string headerHtml = @"
                <style>
                    .myHeader { 
                        font-family: Arial, sans-serif; 
                        font-size: 14pt; 
                        color: #003366; 
                        text-align: center; 
                        margin-bottom: 5pt;
                    }
                </style>
                <div class='myHeader'>Confidential Report – Page {page}</div>";

            // Apply the header to the first three pages
            for (int i = 1; i <= Math.Min(3, doc.Pages.Count); i++)
            {
                Page page = doc.Pages[i];

                // Create an HtmlFragment (HTML rendering is supported via HtmlFragment)
                HtmlFragment headerFragment = new HtmlFragment(headerHtml);

                // Create a HeaderFooter object and add the fragment to its Paragraphs collection
                HeaderFooter header = new HeaderFooter();
                header.Paragraphs.Add(headerFragment);

                // Assign the header to the page
                page.Header = header;
            }

            // Prepare HTML save options with CSS embedded into the HTML file
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Ensure the header/footer is rendered
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml
            };

            // Save the document as HTML
            doc.Save(outputHtml, htmlOpts);
        }

        Console.WriteLine($"HTML with embedded CSS header saved to '{outputHtml}'.");
    }
}
