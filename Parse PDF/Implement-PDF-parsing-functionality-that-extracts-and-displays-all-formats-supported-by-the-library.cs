using System;
using System.IO;
using Aspose.Pdf; // Provides Document, LoadFormat, SaveFormat, PdfFormat

class Program
{
    static void Main()
    {
        const string inputPath  = "sample.pdf";      // PDF to parse
        const string outputPath = "sample_copy.pdf"; // Demonstrate saving

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF (lifecycle: using ensures disposal)
            using (Document doc = new Document(inputPath))
            {
                // Save a copy to illustrate proper save usage
                doc.Save(outputPath);

                // -----------------------------------------------------------------
                // Extract and display all supported formats from the Aspose.Pdf API
                // -----------------------------------------------------------------

                // Load formats (enum LoadFormat)
                Console.WriteLine("Supported Load Formats:");
                foreach (LoadFormat fmt in Enum.GetValues(typeof(LoadFormat)))
                {
                    Console.WriteLine($"- {fmt}");
                }

                // Save formats (enum SaveFormat)
                Console.WriteLine("\nSupported Save Formats:");
                foreach (SaveFormat fmt in Enum.GetValues(typeof(SaveFormat)))
                {
                    Console.WriteLine($"- {fmt}");
                }

                // PDF-specific formats (enum PdfFormat)
                Console.WriteLine("\nSupported PDF Formats:");
                foreach (PdfFormat fmt in Enum.GetValues(typeof(PdfFormat)))
                {
                    Console.WriteLine($"- {fmt}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}