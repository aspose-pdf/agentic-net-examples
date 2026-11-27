using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "fonts.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Root element for the XML document
            XElement root = new XElement("Fonts");

            // Retrieve all fonts via FontUtilities (Document.Fonts and FontInfo do not exist)
            Font[] fonts = doc.FontUtilities.GetAllFonts();

            foreach (Font font in fonts)
            {
                // Create <Font><FontName>...</FontName></Font> element
                XElement fontElement = new XElement("Font",
                    new XElement("FontName", font.FontName));
                root.Add(fontElement);
            }

            // Build the XML document and save it
            XDocument xmlDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                root);
            xmlDoc.Save(xmlPath);
        }

        Console.WriteLine($"Font list saved to '{xmlPath}'.");
    }
}
