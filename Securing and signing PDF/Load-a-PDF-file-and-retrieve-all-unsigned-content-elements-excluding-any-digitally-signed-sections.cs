using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Signatures; // for integrity check (optional)
using Aspose.Pdf.Forms; // for SignatureField detection

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // OPTIONAL: run a compromise check – we only need the boolean result.
            var detector = new SignaturesCompromiseDetector(doc);
            bool signaturesOk = detector.Check(out var compromiseResult);

            // Detect digital signature *fields* – works across all Aspose.Pdf versions.
            bool hasSignatureFields = false;
            if (doc.Form != null && doc.Form.Fields != null)
            {
                foreach (var field in doc.Form.Fields)
                {
                    if (field is SignatureField)
                    {
                        hasSignatureFields = true;
                        break;
                    }
                }
            }

            if (hasSignatureFields)
            {
                Console.WriteLine("Document contains digital signature field(s).");
                Console.WriteLine("Extracting content; note that signed sections cannot be separated via the current API.");
            }
            else
            {
                Console.WriteLine("No digital signatures detected. Extracting all content.");
            }

            // Extract text from the document (unsigned content approximation)
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages.Accept(absorber);
            string extractedText = absorber.Text;

            Console.WriteLine("=== Extracted Text (unsigned content) ===");
            Console.WriteLine(extractedText);
        }
    }
}
