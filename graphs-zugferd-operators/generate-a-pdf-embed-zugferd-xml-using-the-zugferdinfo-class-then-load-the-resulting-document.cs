using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "output.pdf";
        const string xmlPath = "zugferd.xml";

        // Verify that the ZUGFeRD XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {xmlPath}");
            return;
        }

        // Create a new PDF document, add a page, and embed the ZUGFeRD XML
        using (Document doc = new Document())
        {
            // Add a simple page with some placeholder text
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Invoice PDF with ZUGFeRD data"));

            // -----------------------------------------------------------------
            // NOTE: In newer Aspose.Pdf versions the ZUGFeRD data can be attached
            // via the ZugferdInfo class (doc.ZugferdInfo).  The version used for
            // this project does not contain that class, therefore we embed the
            // XML as a regular embedded file which is compatible with the ZUGFeRD
            // specification (the file name must be "ZUGFeRD-invoice.xml").
            // -----------------------------------------------------------------
            var zugferdFileSpec = new FileSpecification(xmlPath)
            {
                // The name used inside the PDF – ZUGFeRD expects this exact name.
                Name = "ZUGFeRD-invoice.xml"
            };
            doc.EmbeddedFiles.Add(zugferdFileSpec);

            // Save the PDF containing the embedded ZUGFeRD data
            doc.Save(pdfPath);
        }

        // Load the saved PDF to verify that the ZUGFeRD XML is present
        using (Document loadedDoc = new Document(pdfPath))
        {
            // Check whether the expected embedded file exists
            bool hasZugferd = false;
            foreach (FileSpecification spec in loadedDoc.EmbeddedFiles)
            {
                if (string.Equals(spec.Name, "ZUGFeRD-invoice.xml", StringComparison.OrdinalIgnoreCase))
                {
                    hasZugferd = true;
                    break;
                }
            }

            if (hasZugferd)
            {
                Console.WriteLine("ZUGFeRD XML successfully embedded and loaded.");
            }
            else
            {
                Console.WriteLine("ZUGFeRD XML not found in the loaded document.");
            }
        }
    }
}
