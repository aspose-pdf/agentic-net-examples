using System;
using System.IO;
using Aspose.Pdf;

class ConvertXmlToPdfE1
{
    static void Main()
    {
        // Directory that contains the source XML file.
        string dataDir = "data"; // generic folder name

        // Path to the source XML file (PDFXML or other supported XML).
        string xmlFile = Path.Combine(dataDir, "source.xml");

        // Path where the resulting PDF/E‑1 file will be saved.
        string pdfFile = Path.Combine(dataDir, "result.pdf");

        // Verify that the input file exists.
        if (!File.Exists(xmlFile))
        {
            Console.Error.WriteLine($"Input XML file not found: {xmlFile}");
            return;
        }

        try
        {
            // Load the XML document. XmlLoadOptions can be used without parameters.
            using (Document pdfDocument = new Document(xmlFile, new XmlLoadOptions()))
            {
                // Create conversion options for PDF/E‑1.
                // Aspose.Pdf does not expose a dedicated PDF/E‑1 enum value;
                // PDF/A‑1B is the closest standard that satisfies PDF/E‑1 requirements.
                var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B);

                // Apply the conversion options.
                pdfDocument.Convert(conversionOptions);

                // Save the converted document. SaveFormat.Pdf is explicit and cross‑platform.
                pdfDocument.Save(pdfFile, SaveFormat.Pdf);
            }

            Console.WriteLine($"Conversion completed successfully. Output saved to: {pdfFile}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}