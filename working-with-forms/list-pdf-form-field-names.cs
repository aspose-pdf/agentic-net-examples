using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Check if the document contains any AcroForm fields
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No AcroForm fields found in the PDF.");
            }
            else
            {
                Console.WriteLine("AcroForm field names:");
                // Enumerate fields via the Form.Fields collection
                foreach (Field field in doc.Form.Fields)
                {
                    // The Name property holds the field's name
                    Console.WriteLine($"- {field.Name}");
                }
            }

            // If the PDF also contains an XFA form, list its field names
            if (doc.Form != null && doc.Form.HasXfa && doc.Form.XFA != null)
            {
                Console.WriteLine("XFA field names:");
                foreach (string xfaName in doc.Form.XFA.FieldNames)
                {
                    Console.WriteLine($"- {xfaName}");
                }
            }
        }
    }
}