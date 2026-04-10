using System;
using System.IO;
using System.Text;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Define the XMP fields that must be present before publishing.
    private static readonly string[] MandatoryXmpFields = new[]
    {
        "dc:title",
        "dc:creator",
        "dc:description"
    };

    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "published.pdf";
        const string validationLog = "validation.log";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Bind the PDF to the XMP metadata facade.
            PdfXmpMetadata xmp = new PdfXmpMetadata();
            xmp.BindPdf(inputPdfPath);

            // Retrieve the XMP metadata as a byte array and convert to string.
            byte[] rawXmp = xmp.GetXmpMetadata();
            string xmpXml = Encoding.UTF8.GetString(rawXmp);

            // Load the XML for easy querying.
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmpXml);

            // Prepare namespace manager (XMP uses various namespaces).
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            foreach (XmlAttribute attr in xmlDoc.DocumentElement.Attributes)
            {
                if (attr.Prefix == "xmlns")
                {
                    nsMgr.AddNamespace(attr.LocalName, attr.Value);
                }
            }

            // Check each mandatory field.
            var missingFields = new System.Collections.Generic.List<string>();
            foreach (string field in MandatoryXmpFields)
            {
                // Split prefix and local name (e.g., "dc:title").
                string[] parts = field.Split(':');
                if (parts.Length != 2)
                {
                    missingFields.Add(field);
                    continue;
                }

                string prefix = parts[0];
                string localName = parts[1];
                string xpath = $"//{prefix}:{localName}";
                XmlNode node = xmlDoc.SelectSingleNode(xpath, nsMgr);
                if (node == null || string.IsNullOrWhiteSpace(node.InnerText))
                {
                    missingFields.Add(field);
                }
            }

            // If any mandatory XMP fields are missing, abort publishing.
            if (missingFields.Count > 0)
            {
                Console.Error.WriteLine("Cannot publish PDF. The following mandatory XMP fields are missing or empty:");
                foreach (string f in missingFields)
                {
                    Console.Error.WriteLine($" - {f}");
                }
                // Optionally write a validation log.
                File.WriteAllText(validationLog, "Missing XMP fields:\n" + string.Join("\n", missingFields));
                return;
            }

            // All mandatory XMP fields are present. Optionally run PDF validation.
            // The Validate method writes a log file and returns true if validation succeeds.
            bool isValid = pdfDoc.Validate(validationLog, PdfFormat.PDF_A_1B);
            if (!isValid)
            {
                Console.Error.WriteLine("PDF validation failed. See log for details.");
                return;
            }

            // Save the validated and approved PDF.
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"PDF published successfully to '{outputPdfPath}'.");
        }
    }
}