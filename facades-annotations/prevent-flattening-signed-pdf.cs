using System;
using System.IO;
using Aspose.Pdf;
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

        // Use PdfFileSignature (Facade) to detect digital signatures
        bool hasSignature;
        PdfFileSignature signatureFacade = new PdfFileSignature();
        try
        {
            signatureFacade.BindPdf(inputPath);
            hasSignature = signatureFacade.ContainsSignature();
        }
        finally
        {
            // Close the facade to release resources
            signatureFacade.Close();
        }

        // If signatures are present, skip flattening to preserve their validity
        // Otherwise, flatten the document (remove form fields/annotations)
        using (Document doc = new Document(inputPath))
        {
            if (!hasSignature)
            {
                // Flatten all form fields and annotations
                doc.Flatten();
            }

            // Save the (possibly flattened) document
            doc.Save(outputPath);
        }

        Console.WriteLine(hasSignature
            ? "Signature detected – document saved without flattening."
            : "No signature – document flattened and saved.");
    }
}