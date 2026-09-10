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

        // Load the PDF document (using the standard Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form
            Form form = doc.Form;
            if (form == null || form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Iterate over all form fields and process only signature fields
            foreach (Field field in form)
            {
                if (field is SignatureField sigField)
                {
                    Console.WriteLine("=== Signature Field ===");
                    // Basic field metadata
                    Console.WriteLine($"Name          : {sigField.Name}");
                    Console.WriteLine($"FullName      : {sigField.FullName}");
                    Console.WriteLine($"PartialName   : {sigField.PartialName}");
                    Console.WriteLine($"AlternateName : {sigField.AlternateName}");
                    Console.WriteLine($"Modified      : {sigField.Modified}");
                    Console.WriteLine($"ReadOnly      : {sigField.ReadOnly}");
                    Console.WriteLine($"Required      : {sigField.Required}");
                    Console.WriteLine($"Exportable    : {sigField.Exportable}");
                    Console.WriteLine($"SignatureExist: {sigField.Signature != null}");

                    // Signature object details (if present)
                    if (sigField.Signature != null)
                    {
                        var signature = sigField.Signature;
                        Console.WriteLine("--- Signature Object ---");
                        Console.WriteLine($"Authority   : {signature.Authority}");
                        Console.WriteLine($"Date        : {signature.Date}");
                        Console.WriteLine($"Reason      : {signature.Reason}");
                        Console.WriteLine($"Location    : {signature.Location}");
                        Console.WriteLine($"ContactInfo : {signature.ContactInfo}");
                        Console.WriteLine($"ShowProperties : {signature.ShowProperties}");
                        Console.WriteLine($"DefaultSignatureLength : {signature.DefaultSignatureLength}");
                        // ByteRange is an int array; display as comma‑separated list
                        Console.WriteLine($"ByteRange   : {string.Join(", ", signature.ByteRange ?? new int[0])}");
                    }

                    // Extract certificate (if any) and write its size
                    try
                    {
                        using (Stream certStream = sigField.ExtractCertificate())
                        {
                            if (certStream != null)
                            {
                                Console.WriteLine($"Certificate size: {certStream.Length} bytes");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Certificate extraction failed: {ex.Message}");
                    }

                    // Extract signature image (if any) and write its size
                    try
                    {
                        using (Stream imgStream = sigField.ExtractImage())
                        {
                            if (imgStream != null)
                            {
                                Console.WriteLine($"Signature image size: {imgStream.Length} bytes");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Image extraction failed: {ex.Message}");
                    }

                    Console.WriteLine(); // blank line between fields
                }
            }
        }
    }
}
