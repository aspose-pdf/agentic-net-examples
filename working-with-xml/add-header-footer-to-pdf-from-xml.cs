using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string headerXmlPath = "header.xml";
        const string footerXmlPath = "footer.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the XML templates (simple text for this example)
        string headerTemplate = File.Exists(headerXmlPath) ? File.ReadAllText(headerXmlPath) : "Default Header";
        string footerTemplate = File.Exists(footerXmlPath) ? File.ReadAllText(footerXmlPath) : "Page #";

        // Open the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Aspose.Pdf.Page page = doc.Pages[i];

                // ----- Header -----
                Aspose.Pdf.HeaderFooter header = new Aspose.Pdf.HeaderFooter();
                // Add static header text from XML template
                TextFragment headerFragment = new TextFragment(headerTemplate);
                headerFragment.TextState.FontSize = 12;
                headerFragment.TextState.Font = FontRepository.FindFont("Helvetica");
                headerFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                header.Paragraphs.Add(headerFragment);
                page.Header = header;

                // ----- Footer -----
                Aspose.Pdf.HeaderFooter footer = new Aspose.Pdf.HeaderFooter();
                // Footer may contain a page number placeholder (#)
                // Replace placeholder with actual page number
                string footerText = footerTemplate.Replace("#", i.ToString());
                TextFragment footerFragment = new TextFragment(footerText);
                footerFragment.TextState.FontSize = 10;
                footerFragment.TextState.Font = FontRepository.FindFont("Helvetica");
                footerFragment.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGray;
                footer.Paragraphs.Add(footerFragment);
                page.Footer = footer;
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with header/footer saved to '{outputPdfPath}'.");
    }
}