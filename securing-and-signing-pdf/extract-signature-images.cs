using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExtractSignatureImages
{
    static void Main()
    {
        const string inputPdf  = "signed_document.pdf";
        const string outputDir = "SignatureImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            int imageCounter = 1;

            // Iterate over all form fields in the document
            foreach (Field field in doc.Form)
            {
                // Process only signature fields
                if (field is SignatureField signatureField)
                {
                    // Extract the signature appearance image as JPEG
                    using (Stream imageStream = signatureField.ExtractImage(ImageFormat.Jpeg))
                    {
                        if (imageStream != null)
                        {
                            string outputPath = Path.Combine(outputDir,
                                $"Signature_{imageCounter}.jpg");

                            // Save the extracted image to a file
                            using (FileStream fileStream = new FileStream(
                                outputPath, FileMode.Create, FileAccess.Write))
                            {
                                imageStream.CopyTo(fileStream);
                            }

                            Console.WriteLine($"Extracted signature image saved to: {outputPath}");
                        }
                        else
                        {
                            Console.WriteLine($"No image found for signature field #{imageCounter}.");
                        }
                    }

                    imageCounter++;
                }
            }
        }
    }
}