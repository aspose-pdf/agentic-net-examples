using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        // Load XML and collect all CDATA section contents
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);
        List<string> cdataContents = new List<string>();

        void Traverse(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.NodeType == XmlNodeType.CDATA)
                {
                    // CDATA.Value can be null, guard against it
                    cdataContents.Add(child.Value ?? string.Empty);
                }
                else
                {
                    Traverse(child);
                }
            }
        }

        Traverse(xmlDoc);

        // Create a new PDF document and add CDATA text as formatted fragments
        using (Document pdfDoc = new Document())
        {
            // Ensure there is at least one page
            pdfDoc.Pages.Add();

            Page page = pdfDoc.Pages[1];
            double yPos = 800; // Starting Y coordinate for the first fragment

            foreach (string cdata in cdataContents)
            {
                // Create a text fragment with the CDATA content
                TextFragment fragment = new TextFragment(cdata);

                // Define appearance via the existing TextState instance (read‑only property)
                fragment.TextState.Font = FontRepository.FindFont("Helvetica");
                fragment.TextState.FontSize = 12;
                fragment.TextState.ForegroundColor = Color.Black;

                // Position the fragment on the page
                fragment.Position = new Position(50, yPos);

                // Add the fragment to the page
                page.Paragraphs.Add(fragment);

                // Move down for the next fragment
                yPos -= 20;
            }

            // Save the resulting PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF saved to '{pdfPath}'.");
    }
}
