using System;
using System.IO;
using System.Linq;
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

        // Load the PDF document using the standard load rule
        using (Document doc = new Document(inputPath))
        {
            // Access the form object; it may be empty
            Form form = doc.Form;
            if (form == null || form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Iterate over all fields and process only signature fields
            foreach (var field in form.Fields.OfType<SignatureField>())
            {
                Console.WriteLine("=== Signature Field ===");
                Console.WriteLine($"Name: {field.Name}");
                Console.WriteLine($"FullName: {field.FullName}");
                Console.WriteLine($"PartialName: {field.PartialName}");
                Console.WriteLine($"AlternateName: {field.AlternateName}");
                Console.WriteLine($"Modified: {field.Modified}");
                Console.WriteLine($"ReadOnly: {field.ReadOnly}");
                Console.WriteLine($"Required: {field.Required}");
                Console.WriteLine($"PageIndex: {field.PageIndex}");
                Console.WriteLine($"Rect: {field.Rect}");
                Console.WriteLine($"Flags: {field.Flags}");
                Console.WriteLine($"Color: {field.Color}");
                Console.WriteLine($"Border Width: {field.Border?.Width}");

                // Extract signature-specific metadata if a signature object exists
                if (field.Signature != null)
                {
                    var signature = field.Signature;
                    Console.WriteLine($"Signature Authority: {signature.Authority}");
                    Console.WriteLine($"Signature ContactInfo: {signature.ContactInfo}");
                    Console.WriteLine($"Signature Date: {signature.Date}");
                    Console.WriteLine($"Signature Location: {signature.Location}");
                    Console.WriteLine($"Signature Reason: {signature.Reason}");
                    Console.WriteLine($"Signature ShowProperties: {signature.ShowProperties}");
                }
                else
                {
                    Console.WriteLine("Signature object not present (field may be unsigned).");
                }

                Console.WriteLine();
            }
        }
    }
}
