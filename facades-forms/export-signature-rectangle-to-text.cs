using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signature_rect.txt";
        const string signatureFieldName = "Signature1"; // change to your field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Locate the signature field (case‑insensitive)
        SignatureField sigField = doc.Form?.Fields?
            .FirstOrDefault(f => f is SignatureField &&
                                 string.Equals(f.Name, signatureFieldName, StringComparison.OrdinalIgnoreCase))
            as SignatureField;

        if (sigField == null)
        {
            Console.Error.WriteLine($"Signature field '{signatureFieldName}' not found.");
            return;
        }

        // Retrieve the rectangle of the signature field
        Aspose.Pdf.Rectangle rect = sigField.Rect;

        // Build the output text
        string rectInfo = $"Signature field '{signatureFieldName}' rectangle:" + Environment.NewLine +
                          $"LLX: {rect.LLX}" + Environment.NewLine +
                          $"LLY: {rect.LLY}" + Environment.NewLine +
                          $"URX: {rect.URX}" + Environment.NewLine +
                          $"URY: {rect.URY}";

        // Write rectangle data to a text file
        File.WriteAllText(outputPath, rectInfo);

        Console.WriteLine($"Rectangle exported to '{outputPath}'.");
    }
}
