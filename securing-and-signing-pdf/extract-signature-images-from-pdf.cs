using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed.pdf";
        const string outputFolder = "SignatureImages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify that the input PDF exists before trying to load it
        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"Input file '{inputPdf}' was not found. Please place the signed PDF in the application directory or update the path.");
            return; // Exit gracefully – no unhandled exception will be thrown
        }

        // Load the signed PDF document
        using (Document doc = new Document(inputPdf))
        {
            int signatureCounter = 1;

            // Iterate over all form fields in the document
            foreach (var field in doc.Form)
            {
                // Process only signature fields
                if (field is SignatureField signatureField)
                {
                    // Extract the signature appearance image as a JPEG stream
                    using (Stream imageStream = signatureField.ExtractImage())
                    {
                        if (imageStream != null)
                        {
                            string outputPath = Path.Combine(outputFolder,
                                $"Signature_{signatureCounter}.jpg");

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
                            Console.WriteLine($"No image found for signature field #{signatureCounter}.");
                        }
                    }

                    signatureCounter++;
                }
            }
        }
    }
}
