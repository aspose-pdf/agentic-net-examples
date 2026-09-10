using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // -------------------------------------------------------------------
        // Ensure the input file exists. In the sandbox there is no pre‑existing
        // PDF, so we create a minimal one on‑the‑fly. This follows the
        // "hardcoded-input-file-generate-inline-first" pattern.
        // -------------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (Document seed = new Document())
            {
                // Add a single blank page (or add content if desired).
                seed.Pages.Add();
                // Optional: add a simple text fragment so the PDF is not empty.
                seed.Pages[1].Paragraphs.Add(new TextFragment("Sample PDF"));
                seed.Save(inputPath);
            }
        }

        // Load PDF meta‑information using the Facades API.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            bool isEncrypted = pdfInfo.IsEncrypted;
            string algorithm = "None";

            if (isEncrypted)
            {
                // The underlying Document is available via the PdfFileInfo object.
                Document doc = pdfInfo.Document;
                algorithm = doc.CryptoAlgorithm.HasValue
                    ? doc.CryptoAlgorithm.Value.ToString()
                    : "Unknown";
            }

            // Display the results (console used as UI placeholder).
            Console.WriteLine($"Is Encrypted: {isEncrypted}");
            Console.WriteLine($"Encryption Algorithm: {algorithm}");
        }
    }
}
