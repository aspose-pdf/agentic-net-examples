using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input_xfa.pdf";
        const string outputPdf = "output_filled.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Access the AcroForm object attached to the document
            Form form = doc.Form;

            // Verify that the document actually contains an XFA form
            if (!form.HasXfa)
            {
                Console.WriteLine("The PDF does not contain an XFA form.");
                return;
            }

            // Obtain the XFA object which provides access to XFA fields
            XFA xfa = form.XFA;

            // List all XFA field names (useful for debugging / discovery)
            Console.WriteLine("XFA field names in the document:");
            foreach (string fieldName in xfa.FieldNames)
            {
                Console.WriteLine($"  - {fieldName}");
            }

            // ------------------------------------------------------------
            // Example: set a value for a specific XFA field
            // ------------------------------------------------------------
            // Replace this with an actual field name that exists in your PDF
            string targetField = "Form1.TextField1";

            // Ensure the field exists before attempting to set its value
            if (Array.Exists(xfa.FieldNames, f => f == targetField))
            {
                // The XFA class provides an indexer (Item) that allows setting a value by field name
                xfa[targetField] = "Value set via XFA indexer";
                Console.WriteLine($"Field '{targetField}' updated successfully.");
            }
            else
            {
                Console.WriteLine($"Field '{targetField}' not found in the XFA form.");
            }

            // ------------------------------------------------------------
            // Optional: retrieve the XML template node for the field
            // ------------------------------------------------------------
            XmlNode templateNode = xfa.GetFieldTemplate(targetField);
            if (templateNode != null)
            {
                Console.WriteLine($"Template node for '{targetField}' has name: {templateNode.Name}");
            }

            // Save the modified PDF back to disk
            doc.Save(outputPdf);
            Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
        }
    }
}