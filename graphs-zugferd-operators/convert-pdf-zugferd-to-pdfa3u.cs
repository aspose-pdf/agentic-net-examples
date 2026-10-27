using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // PDF with ZUGFeRD XML attachment
        const string outputPath = "output_pdfa3u.pdf";  // Resulting PDF/A‑3u file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF. The ZUGFeRD XML is stored as an embedded file.
            using (Document doc = new Document(inputPath))
            {
                // List existing embedded files to verify presence.
                if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
                {
                    foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                    {
                        Console.WriteLine($"Found embedded file: {fileSpec.Name}");
                    }
                }
                else
                {
                    Console.WriteLine("No embedded files found in the source PDF.");
                }

                // Prepare conversion options for PDF/A‑3u.
                var convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_3U)
                {
                    OptimizeFileSize = true
                };

                // Perform the conversion. Embedded files are preserved automatically.
                doc.Convert(convOptions);

                // Save the converted document.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Conversion completed. PDF/A‑3u saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
