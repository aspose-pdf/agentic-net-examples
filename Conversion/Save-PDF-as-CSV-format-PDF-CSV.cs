using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.csv";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPath))
            {
                // Extract all textual content from the PDF
                Aspose.Pdf.Text.TextAbsorber absorber = new Aspose.Pdf.Text.TextAbsorber();
                pdfDoc.Pages.Accept(absorber);
                string extractedText = absorber.Text;

                // Write the extracted text to a CSV file.
                // Each line of the PDF becomes a separate CSV row.
                // Values are quoted and internal quotes are escaped.
                using (StreamWriter writer = new StreamWriter(outputPath, false, System.Text.Encoding.UTF8))
                {
                    using (StringReader sr = new StringReader(extractedText))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string escaped = line.Replace("\"", "\"\"");
                            writer.WriteLine($"\"{escaped}\"");
                        }
                    }
                }
            }

            Console.WriteLine($"PDF text extracted and saved as CSV to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}