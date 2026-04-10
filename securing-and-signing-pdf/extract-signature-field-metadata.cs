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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;
            if (form == null || form.Fields == null || form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Iterate over all fields and process only signature fields
            foreach (var field in form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    Console.WriteLine("=== Signature Field ===");
                    Console.WriteLine($"Name: {sigField.Name}");
                    Console.WriteLine($"FullName: {sigField.FullName}");
                    Console.WriteLine($"PartialName: {sigField.PartialName}");
                    Console.WriteLine($"AlternateName: {sigField.AlternateName}");
                    Console.WriteLine($"Modified: {sigField.Modified}");
                    Console.WriteLine($"ReadOnly: {sigField.ReadOnly}");
                    Console.WriteLine($"Required: {sigField.Required}");
                    Console.WriteLine($"PageIndex: {sigField.PageIndex}");

                    // Extract signature object details
                    Signature signature = sigField.Signature;
                    if (signature != null)
                    {
                        Console.WriteLine("--- Signature Details ---");
                        Console.WriteLine($"Authority: {signature.Authority}");
                        Console.WriteLine($"Date: {signature.Date}");
                        Console.WriteLine($"Reason: {signature.Reason}");
                        Console.WriteLine($"Location: {signature.Location}");
                        Console.WriteLine($"ContactInfo: {signature.ContactInfo}");
                        Console.WriteLine($"ByteRange: {string.Join(", ", signature.ByteRange ?? Array.Empty<int>())}");
                    }

                    Console.WriteLine(); // Blank line for readability
                }
            }
        }
    }
}
