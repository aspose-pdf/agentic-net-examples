using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // PDF with form fields
        const string xmlPath   = "data.xml";       // XML containing a subset of field values
        const string outputPdf = "output.pdf";

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

        // Load the XML document
        XDocument xmlDoc = XDocument.Load(xmlPath);
        if (xmlDoc.Root == null)
        {
            Console.Error.WriteLine("XML document has no root element.");
            return;
        }

        // Open the PDF document inside a using block (ensures proper disposal)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate over each element in the XML.
            // Assume each element name corresponds to a form field name.
            foreach (XElement element in xmlDoc.Root.Elements())
            {
                string fieldName  = element.Name.LocalName;
                string fieldValue = element.Value;

                // Check if the PDF contains a field with this name
                if (pdfDoc.Form.HasField(fieldName))
                {
                    // The Form indexer returns a WidgetAnnotation; cast it to Field.
                    Field? field = pdfDoc.Form[fieldName] as Field;
                    if (field != null)
                    {
                        field.Value = fieldValue; // Update the field with the XML value
                    }
                    else
                    {
                        // This should not happen if HasField returned true, but guard anyway.
                        Console.WriteLine($"Field '{fieldName}' exists but could not be cast to a form Field; skipping.");
                    }
                }
                else
                {
                    // Field not present – ignore or log as needed
                    Console.WriteLine($"Field '{fieldName}' not found in PDF; skipping.");
                }
            }

            // Save the updated PDF (no SaveOptions needed for PDF output)
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with updated fields to '{outputPdf}'.");
    }
}
