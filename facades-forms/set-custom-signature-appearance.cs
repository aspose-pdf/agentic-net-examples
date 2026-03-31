using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Locate the signature field named "Signature"
            SignatureField sigField = null;
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField s && s.PartialName == "Signature")
                {
                    sigField = s;
                    break;
                }
            }

            if (sigField == null)
            {
                Console.Error.WriteLine("Signature field 'Signature' not found.");
                return;
            }

            // Create a custom appearance object
            SignatureCustomAppearance customAppearance = new SignatureCustomAppearance();
            customAppearance.BackgroundColor = Aspose.Pdf.Color.LightGray;
            customAppearance.IsForegroundImage = false; // image will be drawn as background
            customAppearance.FontFamilyName = "Arial";
            customAppearance.FontSize = 12;
            customAppearance.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Assign the custom appearance to the signature field
            sigField.Signature.CustomAppearance = customAppearance;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
    }
}