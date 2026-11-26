using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "TitleDeprecatedToXmp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Update the document title metadata
                doc.Info.Title = "New Title";

                // Convert the document to PDF 2.0. The conversion process moves
                // deprecated Info dictionary entries to XMP metadata automatically.
                // A conversion log file is required by the API; it can be a temporary file.
                string conversionLog = Path.GetTempFileName();
                doc.Convert(conversionLog, PdfFormat.v_2_0, ConvertErrorAction.Delete);

                // Save the updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Saved updated PDF to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
