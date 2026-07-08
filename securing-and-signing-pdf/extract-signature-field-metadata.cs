using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form object which holds all fields
            Form form = doc.Form;

            // Iterate through each field in the form
            foreach (WidgetAnnotation field in form)
            {
                // Identify signature fields
                if (field is SignatureField sigField)
                {
                    Console.WriteLine("=== Signature Field ===");

                    // Basic field metadata
                    Console.WriteLine($"Name: {sigField.Name}");
                    Console.WriteLine($"FullName: {sigField.FullName}");
                    Console.WriteLine($"AlternateName: {sigField.AlternateName}");
                    Console.WriteLine($"PartialName: {sigField.PartialName}");
                    Console.WriteLine($"Modified: {sigField.Modified}");
                    Console.WriteLine($"ReadOnly: {sigField.ReadOnly}");
                    Console.WriteLine($"Required: {sigField.Required}");
                    Console.WriteLine($"SignatureExists: {(sigField.Signature != null ? "Yes" : "No")}");

                    // Extract signature-specific details if present
                    if (sigField.Signature != null)
                    {
                        var signature = sigField.Signature;
                        Console.WriteLine("--- Signature Details ---");
                        Console.WriteLine($"Authority: {signature.Authority}");
                        Console.WriteLine($"Date: {signature.Date}");
                        Console.WriteLine($"Location: {signature.Location}");
                        Console.WriteLine($"Reason: {signature.Reason}");
                        Console.WriteLine($"ContactInfo: {signature.ContactInfo}");
                        Console.WriteLine($"ShowProperties: {signature.ShowProperties}");
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}