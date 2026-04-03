using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "signed_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form object (creates one if it does not exist)
            Form form = doc.Form;

            // Define the rectangle where the signature field will appear
            // (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a signature field on the document (page will be assigned later)
            SignatureField sigField = new SignatureField(doc, rect);
            sigField.Name          = "Signature1";          // field identifier
            sigField.AlternateName = "Sign Here";          // tooltip shown in Acrobat
            sigField.Required      = true;                 // make it a required field

            // Add the field to the form on page 1 (Aspose.Pdf uses 1‑based page indexing)
            form.Add(sigField, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature field added and saved to '{outputPath}'.");
    }
}