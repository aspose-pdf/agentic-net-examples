// ------------------------------------------------------------
// File: Program.cs
// ------------------------------------------------------------
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the facade and bind the PDF file
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdf);

            // Retrieve names of all non‑empty signatures in the document
            // false => exclude empty signature fields
            var signatureNames = pdfSignature.GetSignatureNames(false);

            foreach (var sigName in signatureNames)
            {
                // Try to extract the X509 certificate associated with the signature
                if (pdfSignature.TryExtractCertificate(sigName, out X509Certificate2 cert) && cert != null)
                {
                    // SerialNumber is a hexadecimal string; log it for audit purposes
                    Console.WriteLine($"Signature: {sigName.Name}");
                    Console.WriteLine($"  Certificate Serial Number: {cert.SerialNumber}");
                }
                else
                {
                    Console.WriteLine($"Signature: {sigName.Name} – No certificate found.");
                }
            }
        }
    }
}

// ------------------------------------------------------------
// File: AsposePdfApi.GeneratedMSBuildEditorConfig.editorconfig
// ------------------------------------------------------------
// This file is intentionally empty but must exist so that the project
// does not fail with CS2001 (source file not found). It is compiled as
// a C# source file to satisfy the <Compile Include="..."/> entry in the
// .csproj.
namespace DummyNamespace { }
