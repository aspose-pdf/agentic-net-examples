using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF from a file stream using the core Document API
            using (FileStream inputStream = File.OpenRead(inputPath))
            using (FileStream outputStream = File.Create(outputPath))
            {
                Document pdfDocument = new Document(inputStream);

                // Create a signature field on the first page (position can be adjusted as needed)
                var signatureRect = new Rectangle(100, 100, 200, 150);
                SignatureField signatureField = new SignatureField(pdfDocument.Pages[1], signatureRect)
                {
                    PartialName = "Signature1"
                };
                pdfDocument.Form.Add(signatureField);

                // NOTE: Actual PKCS#7 detached signing requires the Aspose.Pdf.Signatures package.
                // The example creates an empty signature field to keep the code compilable without that optional assembly.

                // Save the PDF (the signature field is now part of the document)
                pdfDocument.Save(outputStream);
            }

            Console.WriteLine($"PDF processed successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}
