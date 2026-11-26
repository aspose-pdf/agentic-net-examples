using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input directory containing the source PDFs
        const string inputDirectory = "directoryG";

        // Output directory for the converted PDF/X‑3 files
        string outputDirectory = Path.Combine(inputDirectory, "ConvertedPDFX3");
        Directory.CreateDirectory(outputDirectory);

        // Path to the ICC profile that will be embedded
        const string iccProfilePath = "profileM.icc";

        // Iterate over all PDF files in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPdfPath = Path.Combine(outputDirectory, fileNameWithoutExt + "_PDFX3.pdf");
            string conversionLogPath = Path.Combine(outputDirectory, fileNameWithoutExt + "_conversion_log.xml");

            // Load each PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Embed the ICC profile as an OutputIntent (required for PDF/X‑3)
                if (File.Exists(iccProfilePath))
                {
                    // Remove any existing intents and add the new one
                    doc.OutputIntents.Clear();
                    doc.OutputIntents.Add(new OutputIntent(iccProfilePath));
                }

                // Convert the document to PDF/X‑3:2003.
                // The Convert method writes a conversion log and changes the document format.
                doc.Convert(conversionLogPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the converted PDF/X‑3 file
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Converted '{pdfPath}' → '{outputPdfPath}'");
        }
    }
}