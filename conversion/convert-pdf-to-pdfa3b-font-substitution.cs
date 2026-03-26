using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa3b.pdf";
        const string conversionLog = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Substitute any missing font with Arial.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

        using (Document doc = new Document(inputPath))
        {
            // Convert the document to PDF/A‑3b. The conversion is performed in‑place.
            doc.Convert(conversionLog, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

            // Save the converted PDF/A‑3b file.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Converted to PDF/A‑3b saved as '{outputPath}'.");
    }
}
