using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExtractSignatureImages
{
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";
        const string outputFolder = "SignatureImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all form fields in the document
            foreach (Field field in doc.Form)
            {
                // Process only signature fields
                if (field is SignatureField signature)
                {
                    // Extract the signature appearance image as a JPEG stream
                    Stream imageStream = signature.ExtractImage();

                    if (imageStream != null)
                    {
                        // Build a unique file name using page index and field name
                        string fileName = $"sig_page{signature.PageIndex}_field_{signature.Name}.jpg";
                        string outputPath = Path.Combine(outputFolder, fileName);

                        // Save the extracted image to disk
                        using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            imageStream.CopyTo(file);
                        }

                        Console.WriteLine($"Extracted signature image saved to: {outputPath}");
                    }
                    else
                    {
                        Console.WriteLine($"No image found for signature field '{signature.Name}' on page {signature.PageIndex}.");
                    }
                }
            }
        }
    }
}