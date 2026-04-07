using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class InsertFootnotes
{
    static void Main()
    {
        // Paths to the input PDF, the XML containing footnote definitions, and the output PDF.
        const string pdfPath = "input.pdf";
        const string xmlPath = "footnotes.xml";
        const string outputPath = "output_with_footnotes.pdf";

        // Verify that the required files exist.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document that defines footnotes.
        // Expected format (example):
        // <Footnotes>
        //   <Footnote>
        //     <Id>1</Id>
        //     <Text>This is the first footnote.</Text>
        //   </Footnote>
        //   <Footnote>
        //     <Id>2</Id>
        //     <Text>Second footnote text.</Text>
        //   </Footnote>
        // </Footnotes>
        XDocument footnotesXml = XDocument.Load(xmlPath);

        // Load the PDF document.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // For demonstration, we will add each footnote to the first page.
            // In a real scenario you would locate the reference point in the text.
            Page firstPage = pdfDoc.Pages[1];

            // Iterate over each <Footnote> element in the XML.
            foreach (XElement fnElement in footnotesXml.Root.Elements("Footnote"))
            {
                // Extract footnote identifier and text.
                string footnoteId = (string)fnElement.Element("Id") ?? string.Empty;
                string footnoteText = (string)fnElement.Element("Text") ?? string.Empty;

                // Create a visible reference marker (e.g., a superscript number).
                // Position can be adjusted as needed.
                TextFragment referenceFragment = new TextFragment(footnoteId);
                referenceFragment.Position = new Position(100, 700); // Example coordinates.
                referenceFragment.TextState.FontSize = 8;
                referenceFragment.TextState.Font = FontRepository.FindFont("Helvetica");
                referenceFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Create the actual footnote content using the Note class.
                Note footnote = new Note
                {
                    Text = footnoteText
                };

                // Associate the footnote with the reference fragment.
                referenceFragment.FootNote = footnote;

                // Add the fragment (with its footnote) to the page.
                firstPage.Paragraphs.Add(referenceFragment);
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Footnotes inserted and saved to '{outputPath}'.");
    }
}