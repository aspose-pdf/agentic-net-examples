using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the signature field by its partial name
            SignatureField sigField = doc.Form["Signature"] as SignatureField;
            if (sigField == null)
            {
                Console.Error.WriteLine("Signature field 'Signature' not found.");
                return;
            }

            // Set custom border thickness (2 points)
            sigField.Border.Width = 2;

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with updated border to '{outputPath}'.");
    }
}