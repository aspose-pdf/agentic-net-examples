using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF files to be synchronized
        string[] pdfPaths = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        // XML file containing the form data
        const string xmlDataPath = "formData.xml";
        // Directory where the updated PDFs will be saved
        const string outputDir = "Output";

        // Verify XML data file exists
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Process each PDF
        foreach (string pdfPath in pdfPaths)
        {
            // Skip missing PDFs but continue with others
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"PDF not found: {pdfPath}");
                continue;
            }

            // Load PDF, import XML form data (via custom XML parser), and save
            using (Document doc = new Document(pdfPath))
            {
                ImportFormDataFromXml(xmlDataPath, doc);

                // Construct output file path
                string outputPath = Path.Combine(outputDir, Path.GetFileName(pdfPath));

                // Save the modified PDF (PDF format by default)
                doc.Save(outputPath);

                Console.WriteLine($"Processed and saved: {outputPath}");
            }
        }
    }

    /// <summary>
    /// Imports form field values from an XML file into the given PDF document.
    /// The XML is expected to have a simple structure like:
    /// <form>
    ///   <field name="FieldName1">Value1</field>
    ///   <field name="FieldName2">Value2</field>
    ///   ...
    /// </form>
    /// </summary>
    /// <param name="xmlPath">Path to the XML data file.</param>
    /// <param name="doc">The Aspose.Pdf.Document whose form fields will be populated.</param>
    private static void ImportFormDataFromXml(string xmlPath, Document doc)
    {
        if (doc?.Form?.Fields == null)
            return;

        XDocument xDoc = XDocument.Load(xmlPath);
        // Iterate over each <field> element
        foreach (XElement fieldElem in xDoc.Descendants("field"))
        {
            // Field name can be supplied as an attribute "name" or as a child <name> element
            string fieldName = (string)fieldElem.Attribute("name");
            if (string.IsNullOrEmpty(fieldName))
            {
                XElement nameElem = fieldElem.Element("name");
                if (nameElem != null)
                    fieldName = nameElem.Value;
            }
            if (string.IsNullOrEmpty(fieldName))
                continue; // cannot map without a name

            string fieldValue = fieldElem.Value ?? string.Empty;

            // Find the matching field in the PDF form (case‑sensitive match on Name)
            var pdfField = doc.Form.Fields.FirstOrDefault(f => f.Name == fieldName);
            if (pdfField != null)
            {
                // Most field types expose a Value property that accepts string/object
                pdfField.Value = fieldValue;
            }
        }
    }
}
