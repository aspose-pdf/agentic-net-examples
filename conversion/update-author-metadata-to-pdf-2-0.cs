using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "AuthorDeprecatedToXmp.pdf";
        const string conversionLog = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Update the Author metadata field
            doc.Info.Author = "New Author Name";

            // Convert the document to PDF 2.0 which migrates deprecated fields to XMP metadata
            doc.Convert(conversionLog, PdfFormat.v_2_0, ConvertErrorAction.Delete);

            // Save the updated document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved as '{outputPath}'.");
    }
}
