using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string xmlDataPath   = "headers.xml";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlDataPath}");
            return;
        }

        // Load XML that contains header text per page.
        // Expected format:
        // <Headers>
        //   <Header page="1">First page header</Header>
        //   <Header page="2">Second page header</Header>
        //   ...
        // </Headers>
        XDocument xDoc = XDocument.Load(xmlDataPath);
        Dictionary<int, string> headerByPage = xDoc.Root
            .Elements("Header")
            .Where(e => e.Attribute("page") != null)
            .ToDictionary(
                e => (int) e.Attribute("page"),
                e => (string) e.Value);

        // Open the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Attach a BeforePageGenerate handler to each page.
            foreach (Page page in pdfDoc.Pages)
            {
                page.OnBeforePageGenerate += (Page p) =>
                {
                    // Ensure a HeaderFooter object exists.
                    p.Header = new HeaderFooter();

                    // Retrieve the header text for the current page.
                    string headerText;
                    if (!headerByPage.TryGetValue(p.Number, out headerText))
                    {
                        // Fallback if no specific entry exists.
                        headerText = "Default Header";
                    }

                    // Create a TextFragment with the desired styling.
                    TextFragment headerFragment = new TextFragment(headerText);
                    headerFragment.TextState.Font = FontRepository.FindFont("Helvetica");
                    headerFragment.TextState.FontSize = 12;
                    headerFragment.TextState.ForegroundColor = Color.Gray;

                    // Optionally adjust position inside the header.
                    // The Position is relative to the header's rectangle.
                    // Here we place it 10 points from the left edge.
                    headerFragment.Position = new Position(10, 0);

                    // Add the fragment to the header.
                    p.Header.Paragraphs.Add(headerFragment);
                };
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with dynamic headers saved to '{outputPdfPath}'.");
    }
}