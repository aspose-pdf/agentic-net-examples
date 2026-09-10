using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load, flatten, and save the PDF
        using (Document doc = new Document(inputPath))
        {
            // Remove all form fields and annotations, leaving only their visual appearance
            doc.Flatten();

            // Save the flattened PDF (core API)
            doc.Save(outputPath);
        }

        // Add custom metadata indicating the flattening date using the Facades API
        PdfFileInfo info = new PdfFileInfo();
        info.BindPdf(outputPath); // Load the just‑saved flattened PDF

        // Use ISO 8601 format for the date/time string
        string flattenDate = DateTime.UtcNow.ToString("o");
        info.SetMetaInfo("FlattenedDate", flattenDate);

        // Save the updated PDF with the new metadata
        info.SaveNewInfo(outputPath);

        Console.WriteLine($"Flattened PDF saved to '{outputPath}' with metadata FlattenedDate={flattenDate}");
    }
}