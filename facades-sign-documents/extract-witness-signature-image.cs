using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "WitnessSignature.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document doc = new Document(inputPath);

            // Retrieve the signature field named 'WitnessSignature'
            var field = doc.Form["WitnessSignature"] as SignatureField;
            if (field == null)
            {
                Console.Error.WriteLine("Signature field 'WitnessSignature' not found or not a signature field.");
                return;
            }

            // Extract the image stream from the signature appearance
            using (Stream imgStream = field.ExtractImage())
            {
                if (imgStream == null)
                {
                    Console.Error.WriteLine("No image found in the signature field.");
                    return;
                }

                // Save the extracted image as PNG
                using (FileStream file = File.Create(outputPath))
                {
                    imgStream.CopyTo(file);
                }
            }

            Console.WriteLine($"Signature image saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
