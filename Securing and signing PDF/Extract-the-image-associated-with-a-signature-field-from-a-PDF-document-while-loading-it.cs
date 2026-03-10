using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all form fields; signature fields are of type SignatureField
            foreach (SignatureField sigField in doc.Form)
            {
                // Attempt to extract the signature image (JPEG encoded stream)
                Stream imageStream = sigField.ExtractImage();

                if (imageStream != null)
                {
                    // Build an output file name based on the field name (or use a default)
                    string fieldName = string.IsNullOrEmpty(sigField.Name) ? "signature" : sigField.Name;
                    string outputPath = $"{fieldName}.jpg";

                    // Save the extracted image stream to a file
                    using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        imageStream.CopyTo(file);
                    }

                    Console.WriteLine($"Extracted signature image saved to '{outputPath}'.");
                }
                else
                {
                    Console.WriteLine($"No image found for signature field '{sigField.Name}'.");
                }
            }
        }
    }
}