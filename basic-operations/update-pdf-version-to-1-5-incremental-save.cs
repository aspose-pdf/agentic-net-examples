using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_incremental.pdf";
        const string conversionLog = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF
        using (Document doc = new Document(inputPath))
        {
            // Change PDF version to 1.5 using Document.Convert
            doc.Convert(conversionLog, PdfFormat.v_1_5, ConvertErrorAction.Delete);

            // Save with incremental update – open a read/write stream and call parameterless Save()
            using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.ReadWrite))
            {
                doc.Save(fs); // parameterless Save performs incremental update on the stream
            }
        }

        Console.WriteLine($"PDF saved with version 1.5 and incremental update to '{outputPath}'.");
    }
}
