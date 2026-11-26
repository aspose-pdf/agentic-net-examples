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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Apply a simple font substitution (replace missing "Helvetica" with "Arial")
            // FontRepository.Substitutions is the correct API for font substitution.
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Arial"));

            // Enable automatic tagging for accessibility
            AutoTaggingSettings.Default.EnableAutoTagging = true;

            // Convert the document to PDF/A-3b. The first argument is the path for the conversion log file.
            doc.Convert(conversionLog, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

            // Save the resulting PDF/A-3b file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A-3b file saved to '{outputPath}'.");
    }
}
