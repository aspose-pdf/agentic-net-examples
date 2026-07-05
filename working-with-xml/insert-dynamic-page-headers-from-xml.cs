using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlDataPath = "headers.xml";   // XML file containing header data per page
        const string outputPdfPath = "output.pdf";

        // Verify that the XML file exists
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"Error: XML file not found at '{xmlDataPath}'.");
            return;
        }

        // Load the XML data once – it contains elements like:
        // <PageHeader Page="1">Header text for page 1</PageHeader>
        XDocument xmlDoc = XDocument.Load(xmlDataPath);

        // Create a new PDF document (or load an existing template)
        using (Document pdfDoc = new Document())
        {
            // Add a few pages for demonstration (in real scenario the document may already have pages)
            for (int i = 0; i < 5; i++)
                pdfDoc.Pages.Add();

            // Apply a header to each page based on the XML data
            foreach (Page page in pdfDoc.Pages)
            {
                // Retrieve the header text for the current page number from the XML
                string headerText = xmlDoc.Descendants("PageHeader")
                    .FirstOrDefault(e => (int?)e.Attribute("Page") == page.Number)
                    ?.Value ?? $"Default Header – Page {page.Number}";

                // Create a HeaderFooter object and add the text fragment
                HeaderFooter header = new HeaderFooter();
                header.Paragraphs.Add(new TextFragment(headerText));

                // Assign the header to the page
                page.Header = header;
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF successfully saved to '{outputPdfPath}'.");
    }
}
