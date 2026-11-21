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
        const string pdfPath = "input.pdf";
        const string xmlPath = "data.xml";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(pdfPath))
            {
                // Load XML data
                XDocument xmlDoc = XDocument.Load(xmlPath);

                // Iterate over each element in the root and map to form fields
                foreach (XElement element in xmlDoc.Root.Elements())
                {
                    string fieldName = element.Name.LocalName;
                    string fieldValue = element.Value ?? string.Empty;

                    // Find the matching field in the PDF form
                    var field = doc.Form?.Fields?.FirstOrDefault(f => f.Name == fieldName);
                    if (field != null)
                    {
                        // Set the field value. Most field types accept a string representation.
                        field.Value = fieldValue;
                    }
                }

                // Enable automatic recalculation of calculated fields (optional but recommended).
                doc.Form.AutoRecalculate = true;

                // Save the updated PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
