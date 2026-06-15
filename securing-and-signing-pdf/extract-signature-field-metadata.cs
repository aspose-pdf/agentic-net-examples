using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Added namespace for WidgetAnnotation

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

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Access the form that contains all fields
            Form form = doc.Form;

            // Iterate over each field in the form
            foreach (WidgetAnnotation field in form)
            {
                // Identify signature fields
                if (field is SignatureField sigField)
                {
                    Console.WriteLine("Signature Field:");
                    Console.WriteLine($"  Name: {sigField.Name}");
                    Console.WriteLine($"  FullName: {sigField.FullName}");
                    Console.WriteLine($"  AlternateName: {sigField.AlternateName}");
                    Console.WriteLine($"  PartialName: {sigField.PartialName}");
                    Console.WriteLine($"  PageIndex: {sigField.PageIndex}");
                    Console.WriteLine($"  Rect: {sigField.Rect}");
                    Console.WriteLine($"  Modified: {sigField.Modified}");
                    Console.WriteLine($"  ReadOnly: {sigField.ReadOnly}");
                    Console.WriteLine($"  Required: {sigField.Required}");
                    Console.WriteLine($"  Signature Exists: {(sigField.Signature != null)}");

                    // If a signature is present, extract its metadata
                    if (sigField.Signature != null)
                    {
                        var signature = sigField.Signature;
                        Console.WriteLine($"  Signature Date: {signature.Date}");
                        Console.WriteLine($"  Reason: {signature.Reason}");
                        Console.WriteLine($"  Location: {signature.Location}");
                        Console.WriteLine($"  ContactInfo: {signature.ContactInfo}");
                        Console.WriteLine($"  Authority: {signature.Authority}");
                        // ByteRange is an array of integers
                        Console.WriteLine($"  ByteRange: {string.Join(", ", signature.ByteRange ?? Array.Empty<int>())}");
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
