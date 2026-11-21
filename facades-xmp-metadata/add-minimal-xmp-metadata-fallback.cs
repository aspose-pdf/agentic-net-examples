using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Aspose.Pdf.Document sourceDoc = new Aspose.Pdf.Document(inputPath))
        {
            // Create a new document to hold the result
            using (Aspose.Pdf.Document targetDoc = new Aspose.Pdf.Document())
            {
                // Copy all pages from source to target
                targetDoc.Pages.Add(sourceDoc.Pages);

                // Determine whether the source PDF already contains metadata
                bool hasMetadata = sourceDoc.Metadata.Count > 0;

                if (hasMetadata)
                {
                    // Copy existing metadata entries to the target document
                    foreach (var key in sourceDoc.Metadata.Keys)
                    {
                        targetDoc.Metadata[key] = sourceDoc.Metadata[key];
                    }

                    // Save the document with the copied metadata
                    targetDoc.Save(outputPath);
                    Console.WriteLine($"PDF saved with existing metadata to '{outputPath}'.");
                }
                else
                {
                    // No metadata present – generate minimal XMP metadata
                    // First save the target document to a temporary file
                    string tempPath = Path.GetTempFileName();
                    targetDoc.Save(tempPath);

                    // Use PdfXmpMetadata (Facades API) to add XMP entries
                    Aspose.Pdf.Facades.PdfXmpMetadata xmp = new Aspose.Pdf.Facades.PdfXmpMetadata();
                    xmp.BindPdf(tempPath);

                    // Minimal required XMP entries (title and creation date)
                    xmp.Add("dc:title", Path.GetFileNameWithoutExtension(inputPath));
                    xmp.Add("xmp:CreateDate", DateTime.UtcNow.ToString("o"));

                    // Save the final PDF with the new XMP metadata
                    xmp.Save(outputPath);
                    xmp.Close();

                    // Clean up the temporary file
                    File.Delete(tempPath);

                    Console.WriteLine($"Generated minimal XMP metadata and saved to '{outputPath}'.");
                }
            }
        }
    }
}