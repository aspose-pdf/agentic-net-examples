using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace is required by the task

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Detect encryption by attempting to open the document.
        // If the PDF is encrypted, Aspose.Pdf throws PdfException.
        try
        {
            // Load the source PDF inside a using block for deterministic disposal.
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
            {
                // Update multiple XMP properties in the Metadata dictionary.
                // The Metadata dictionary stores XMP key/value pairs.
                // All changes are made before the document is saved, ensuring a single atomic write.
                doc.Metadata["dc:title"]       = "Accessible Document";
                doc.Metadata["dc:creator"]     = "Acme Corp";
                doc.Metadata["xmp:ModifyDate"] = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
                doc.Metadata["pdf:Producer"]   = "Aspose.Pdf for .NET";

                // Save to a temporary file first to avoid partial writes on failure.
                string tempPath = Path.Combine(
                    Path.GetDirectoryName(outputPath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(outputPath) + "_temp.pdf");

                doc.Save(tempPath);

                // Replace (or move) the temporary file to the final destination atomically.
                // If the target file already exists, it will be overwritten.
                File.Replace(tempPath, outputPath, null);
                Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
            }
        }
        catch (PdfException)
        {
            // The document could not be opened because it is encrypted.
            Console.Error.WriteLine("The PDF is encrypted and cannot be processed without a password.");
        }
        catch (Exception ex)
        {
            // General error handling.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}