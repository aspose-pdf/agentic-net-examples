using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputImage = "witness_signature.png";
        const string targetFieldName = "WitnessSignature";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        using (Document document = new Document(inputPath))
        {
            var pdfForm = document.Form;
            bool fieldFound = false;

            foreach (var field in pdfForm.Fields)
            {
                if (field is SignatureField signatureField &&
                    string.Equals(signatureField.PartialName, targetFieldName, StringComparison.Ordinal))
                {
                    fieldFound = true;

                    // ExtractImage now returns a Stream to avoid System.Drawing/Image ambiguity.
                    using (Stream imgStream = signatureField.ExtractImage())
                    {
                        if (imgStream != null)
                        {
                            using (FileStream fs = new FileStream(outputImage, FileMode.Create, FileAccess.Write))
                            {
                                imgStream.CopyTo(fs);
                            }
                            Console.WriteLine($"Signature image saved to '{outputImage}'.");
                        }
                        else
                        {
                            Console.WriteLine("No image found in the signature field.");
                        }
                    }
                    break;
                }
            }

            if (!fieldFound)
            {
                Console.WriteLine($"Signature field '{targetFieldName}' not found.");
            }
        }
    }
}
