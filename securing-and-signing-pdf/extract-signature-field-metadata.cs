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
            // Access the form that contains all fields
            Form form = doc.Form;

            // Iterate over every field in the form
            foreach (Field field in form.Fields)
            {
                // Process only signature fields
                if (field is SignatureField sigField)
                {
                    Console.WriteLine("=== Signature Field ===");
                    Console.WriteLine($"Name: {sigField.Name}");
                    Console.WriteLine($"FullName: {sigField.FullName}");
                    Console.WriteLine($"AlternateName: {sigField.AlternateName}");
                    Console.WriteLine($"PartialName: {sigField.PartialName}");
                    Console.WriteLine($"PageIndex: {sigField.PageIndex}");
                    Console.WriteLine($"Rect: {sigField.Rect}");
                    Console.WriteLine($"Modified: {sigField.Modified}");
                    Console.WriteLine($"ReadOnly: {sigField.ReadOnly}");
                    Console.WriteLine($"Required: {sigField.Required}");
                    Console.WriteLine($"SignatureExists: {(sigField.Signature != null)}");

                    // If the field already contains a digital signature, extract its details
                    if (sigField.Signature != null)
                    {
                        var signature = sigField.Signature;
                        Console.WriteLine("--- Signature Object ---");
                        Console.WriteLine($"Authority: {signature.Authority}");
                        Console.WriteLine($"Date: {signature.Date}");
                        Console.WriteLine($"Location: {signature.Location}");
                        Console.WriteLine($"Reason: {signature.Reason}");
                        Console.WriteLine($"ContactInfo: {signature.ContactInfo}");
                        Console.WriteLine($"ShowProperties: {signature.ShowProperties}");
                        Console.WriteLine($"ByteRange length: {signature.ByteRange?.Length ?? 0}");
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}