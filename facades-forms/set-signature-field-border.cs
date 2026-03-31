using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
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
            FormEditor editor = new FormEditor();
            editor.BindPdf(doc);

            // Example of using SetFieldAttribute – mark the field as required
            editor.SetFieldAttribute("Signature", PropertyFlag.Required);

            // Retrieve the signature field and set a custom border thickness of 2 points
            SignatureField sigField = (SignatureField)doc.Form["Signature"];
            if (sigField != null)
            {
                sigField.Border.Width = 2; // thickness in points
            }
            else
            {
                Console.Error.WriteLine("Signature field not found.");
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
