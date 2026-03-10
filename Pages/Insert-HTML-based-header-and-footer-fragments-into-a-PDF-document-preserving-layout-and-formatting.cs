using System;
using System.IO;
using Aspose.Pdf;               // Core PDF classes (Document, Page, HeaderFooter, HtmlFragment)

class InsertHtmlHeaderFooter
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

        // Load the existing PDF document (lifecycle: load)
        using (Document doc = new Document(inputPdf))
        {
            // ----- Create HTML‑based header -----
            HeaderFooter header = new HeaderFooter();                     // create HeaderFooter instance
            // HtmlFragment constructor takes the raw HTML string
            HtmlFragment headerHtml = new HtmlFragment(
                "<div style='font-family:Helvetica; font-size:10pt; color:#555555;'>" +
                "Sample Header – Page <page></page>" +                     // <page> placeholder will be replaced by page number
                "</div>");
            // Add the fragment to the header's paragraph collection
            header.Paragraphs.Add(headerHtml);

            // ----- Create HTML‑based footer -----
            HeaderFooter footer = new HeaderFooter();
            HtmlFragment footerHtml = new HtmlFragment(
                "<div style='font-family:Helvetica; font-size:9pt; text-align:center; color:#777777;'>" +
                "Confidential – <date></date>" +                           // <date> placeholder will be replaced by current date
                "</div>");
            footer.Paragraphs.Add(footerHtml);

            // Apply the same header/footer to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.Header = header;
                page.Footer = footer;
            }

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with HTML header/footer saved to '{outputPdf}'.");
    }
}