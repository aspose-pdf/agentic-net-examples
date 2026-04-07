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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the AcroForm of the document
            Form form = doc.Form;

            // If the document has no AcroForm fields, report and exit
            if (form.Count == 0)
            {
                Console.WriteLine("No AcroForm fields found in the document.");
                return;
            }

            Console.WriteLine("AcroForm field names:");
            // Enumerate fields via the Fields property (returns Field[])
            foreach (Field field in form.Fields)
            {
                // The Name property holds the field's identifier
                Console.WriteLine($"- {field.Name}");
            }

            // If the document also contains an XFA form, list its field names
            if (form.HasXfa)
            {
                string[] xfaNames = form.XFA.FieldNames;
                if (xfaNames != null && xfaNames.Length > 0)
                {
                    Console.WriteLine("\nXFA field names:");
                    foreach (string name in xfaNames)
                    {
                        Console.WriteLine($"- {name}");
                    }
                }
            }
        }
    }
}