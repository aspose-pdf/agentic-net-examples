using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExtractSignatureImages
{
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";
        const string outputDir = "SignatureImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the signed PDF
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all form fields in the document
            foreach (Field field in doc.Form)
            {
                // Process only signature fields
                if (field is SignatureField signature)
                {
                    // Extract the signature appearance image as a JPEG stream
                    using (Stream imageStream = signature.ExtractImage())
                    {
                        if (imageStream != null)
                        {
                            string outputPath = Path.Combine(outputDir, $"{signature.Name}.jpg");
                            // Save the extracted image to a file
                            using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                            {
                                imageStream.CopyTo(file);
                            }
                            Console.WriteLine($"Extracted signature image saved to: {outputPath}");
                        }
                        else
                        {
                            Console.WriteLine($"No image found for signature field: {signature.Name}");
                        }
                    }
                }
            }
        }
    }
}