using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string signedPdfPath = "signed_input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPdfPath = "signed_with_stamp.pdf";

        if (!File.Exists(signedPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {signedPdfPath}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        try
        {
            // Open the signed PDF with a read/write stream so that we can perform an incremental update.
            using (var fs = new FileStream(signedPdfPath, FileMode.Open, FileAccess.ReadWrite))
            using (var doc = new Document(fs))
            {
                // Create an image stamp. Position (100,500) is lower‑left corner.
                var imgStamp = new ImageStamp(stampImagePath)
                {
                    XIndent = 100, // distance from the left edge in points (1/72 inch)
                    YIndent = 500  // distance from the bottom edge in points
                };

                // Add the stamp to the first page.
                doc.Pages[1].AddStamp(imgStamp);

                // Parameterless Save performs an incremental update, preserving existing signatures.
                doc.Save();

                // After the incremental save the original stream (fs) now contains the updated PDF.
                // Copy it to the desired output file.
                fs.Position = 0;
                using (var outFs = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
                {
                    fs.CopyTo(outFs);
                }
            }

            Console.WriteLine($"Image stamp added without invalidating signature. Output: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
