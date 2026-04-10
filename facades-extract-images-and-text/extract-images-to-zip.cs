// ------------------------------------------------------------
// File: Program.cs
// ------------------------------------------------------------
using System;
using System.IO;
using System.IO.Compression;
using System.Drawing.Imaging; // for ImageFormat
using Aspose.Pdf.Facades;   // PdfExtractor

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputZipPath = "images.zip";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Initialize the extractor and bind the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage(); // prepare image extraction

            // Create the ZIP archive to store extracted images
            using (FileStream zipFileStream = new FileStream(outputZipPath, FileMode.Create))
            using (ZipArchive zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
            {
                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Extract each image into a memory stream as PNG
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imageStream, ImageFormat.Png);
                        imageStream.Position = 0; // reset for reading

                        // Add the image to the ZIP archive
                        string entryName = $"image-{imageIndex}.png";
                        ZipArchiveEntry entry = zipArchive.CreateEntry(entryName);
                        using (Stream entryStream = entry.Open())
                        {
                            imageStream.CopyTo(entryStream);
                        }
                    }
                    imageIndex++;
                }
            }
        }

        Console.WriteLine($"Extraction complete. Images saved to '{outputZipPath}'.");
    }
}

// ------------------------------------------------------------
// File: AsposePdfApi.GeneratedMSBuildEditorConfig.editorconfig
// ------------------------------------------------------------
// This file is intentionally left empty. It exists solely to satisfy the
// project file entry that treats it as a source file. Adding a minimal
// valid C# construct prevents the CS2001 "source file could not be found"
// compilation error.
namespace Dummy { }

