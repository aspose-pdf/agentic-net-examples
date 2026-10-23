using System;
using System.IO;
using System.Linq; // Added for Count() extension method
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades; // PKCS7 lives in this namespace

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Ensure the document contains form fields
        if (pdfDoc.Form == null || pdfDoc.Form.Fields == null || pdfDoc.Form.Fields.Count() == 0)
        {
            Console.WriteLine("No form fields found in the document.");
            return;
        }

        int signatureIndex = 0;
        // Iterate over all form fields and process only signature fields
        foreach (Field field in pdfDoc.Form.Fields)
        {
            if (field is SignatureField sigField)
            {
                signatureIndex++;
                Console.WriteLine($"Signature {signatureIndex}:");

                // The Reason and Location are stored in the underlying PKCS7 object, not on the field itself
                if (sigField.Signature is PKCS7 pkcs7)
                {
                    Console.WriteLine($"  Reason   : {pkcs7.Reason ?? "<none>"}");
                    Console.WriteLine($"  Location : {pkcs7.Location ?? "<none>"}");
                }
                else
                {
                    Console.WriteLine("  No PKCS7 signature data available for this field.");
                }
            }
        }

        if (signatureIndex == 0)
        {
            Console.WriteLine("No signatures found in the document.");
        }
    }
}
