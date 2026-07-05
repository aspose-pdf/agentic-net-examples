using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";
        const string tocHtml = "toc.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document.
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Extract text fragments (treated as paragraphs) from the PDF.
            // ParagraphAbsorber does not expose a Paragraphs collection in recent versions;
            // use TextFragmentAbsorber instead, which provides a TextFragments collection.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.Visit(pdfDoc);

            // Build a simple Table of Contents based on fragments with a larger font size.
            StringBuilder tocBuilder = new StringBuilder();
            tocBuilder.AppendLine("<!DOCTYPE html>");
            tocBuilder.AppendLine("<html><head><meta charset=\"UTF-8\"><title>Table of Contents</title></head><body>");
            tocBuilder.AppendLine("<h1>Table of Contents</h1>");
            tocBuilder.AppendLine("<ul>");

            foreach (TextFragment fragment in absorber.TextFragments)
            {
                double fontSize = fragment.TextState.FontSize;
                // Treat fragments with font size >= 14 as headings.
                if (fontSize >= 14)
                {
                    string text = System.Net.WebUtility.HtmlEncode(fragment.Text);
                    // NOTE: TextFragment does not expose a PageNumber property in the current API.
                    // If page‑specific linking is required, consider using a different approach such as
                    // creating Heading objects with TocInfo or extracting page numbers via a custom
                    // TextFragmentAbsorber that records the page index during visitation.
                    // For this example we link to the whole HTML document.
                    string href = outputHtml; // simple link without page anchor
                    tocBuilder.AppendLine($"<li><a href=\"{href}\">{text}</a></li>");
                }
            }

            tocBuilder.AppendLine("</ul>");
            tocBuilder.AppendLine("</body></html>");

            // Write the TOC to a separate HTML file.
            File.WriteAllText(tocHtml, tocBuilder.ToString());

            // Convert the PDF to a single HTML file.
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // SplitIntoPages = false generates one HTML file; page IDs are still added for linking.
                SplitIntoPages = false
            };
            pdfDoc.Save(outputHtml, htmlOptions);
        }

        Console.WriteLine("PDF to HTML conversion completed.");
        Console.WriteLine($"HTML output: {outputHtml}");
        Console.WriteLine($"Table of Contents: {tocHtml}");
    }
}
