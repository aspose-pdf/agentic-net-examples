using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations; // DefaultAppearance

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outPath = "output.pdf";
        const string fontPath = "fonts/Custom.ttf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font not found: {fontPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Load the custom TrueType font
                Font customFont = FontRepository.OpenFont(fontPath);

                // Retrieve the signature field named "Signature"
                SignatureField sigField = doc.Form["Signature"] as SignatureField;
                if (sigField != null)
                {
                    // Assign a DefaultAppearance that uses the custom font
                    sigField.DefaultAppearance = new DefaultAppearance(customFont, 12, System.Drawing.Color.Black);
                }
                else
                {
                    Console.Error.WriteLine("Signature field 'Signature' not found.");
                }

                // Save the modified PDF
                doc.Save(outPath);
            }

            Console.WriteLine($"Custom font applied to signature field. Saved to '{outPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}