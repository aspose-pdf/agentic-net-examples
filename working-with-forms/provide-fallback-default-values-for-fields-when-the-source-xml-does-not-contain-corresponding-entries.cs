using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class XmlFieldFallback
{
    static void Main()
    {
        // Paths to the source XML and the PDF that will receive the fallback values
        const string xmlPath = "data.xml";
        const string pdfPath = "output.pdf";

        // Load the source XML (no XSL required). If the file does not exist, create an empty document.
        XmlDocument xmlDoc = new XmlDocument();
        if (File.Exists(xmlPath))
        {
            xmlDoc.Load(xmlPath);
        }
        else
        {
            Console.WriteLine($"Warning: XML file '{xmlPath}' not found. Using fallback metadata values only.");
            // Create a minimal XML structure so that XPath queries do not throw.
            xmlDoc.LoadXml("<Root></Root>");
        }

        // Define the fields we expect and their fallback values
        var expectedFields = new[]
        {
            new { Name = "Title",   Default = "Untitled Document" },
            new { Name = "Author",  Default = "Unknown Author"    },
            new { Name = "Subject", Default = "No Subject"        }
        };

        // Create a new PDF document (ensure at least one page exists)
        using (Document pdfDoc = new Document())
        {
            pdfDoc.Pages.Add();

            // Iterate over each expected field, read from XML or use the fallback
            foreach (var field in expectedFields)
            {
                // Try to locate the element in the source XML
                XmlNode node = xmlDoc.SelectSingleNode($"//{field.Name}");
                string value = node?.InnerText ?? field.Default;

                // Populate the standard PDF document information dictionary
                // (Title, Author, Subject are part of Document.Info)
                switch (field.Name)
                {
                    case "Title":
                        pdfDoc.Info.Title = value;
                        break;
                    case "Author":
                        pdfDoc.Info.Author = value;
                        break;
                    case "Subject":
                        pdfDoc.Info.Subject = value;
                        break;
                    default:
                        // For any custom metadata, use the XMP metadata dictionary if needed.
                        // Since the XMP classes are not available in the current package, we skip them.
                        break;
                }
            }

            // Save the PDF with the populated metadata
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF saved to '{pdfPath}' with fallback metadata fields.");
    }
}
