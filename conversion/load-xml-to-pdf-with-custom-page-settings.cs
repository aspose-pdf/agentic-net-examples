using System;
using System.IO;
using Aspose.Pdf; // Load option classes (e.g., XmlLoadOptions) are in this namespace

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML representation of a PDF document.
        // XmlLoadOptions tells Aspose.Pdf to interpret the file as XML.
        using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Define custom page settings.
            PageInfo pageInfo = new PageInfo
            {
                IsLandscape = false, // Portrait orientation
                Margin = new MarginInfo
                {
                    Top = 50,    // points
                    Bottom = 50,
                    Left = 40,
                    Right = 40
                }
            };

            // Apply the custom PageInfo to the document.
            doc.PageInfo = pageInfo;

            // Save the bound PDF.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully at '{pdfPath}'.");
    }
}
