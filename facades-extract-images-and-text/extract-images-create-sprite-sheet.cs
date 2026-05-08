using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;                  // PdfExtractor, ImageMergeMode, PdfConverter
using Aspose.Pdf.Drawing;                 // ImageFormat

// ---------------------------------------------------------------------------
// Minimal NUnit stubs – required only when the NUnit package is not referenced.
// They allow any leftover test attributes in the source to compile without
// pulling the full NUnit library.
// ---------------------------------------------------------------------------
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SetUpAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TearDownAttribute : Attribute { }

    public delegate void TestDelegate();

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }

        public static T Throws<T>(TestDelegate code) where T : Exception
        {
            try
            {
                code();
            }
            catch (T ex)
            {
                return ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"Assert.Throws failed. Expected {typeof(T)} but got {ex.GetType()}.", ex);
            }
            throw new Exception($"Assert.Throws failed. No exception thrown. Expected {typeof(T)}.");
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPng = "sprite_sheet.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // List to hold each extracted image as a stream
        List<Stream> imageStreams = new List<Stream>();

        // Extract images from the PDF using PdfExtractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage(); // start image extraction

            while (extractor.HasNextImage())
            {
                // Store each image in a memory stream
                MemoryStream ms = new MemoryStream();
                // GetNextImage writes the image (default format JPEG) into the stream
                extractor.GetNextImage(ms);
                ms.Position = 0; // reset for later reading
                imageStreams.Add(ms);
            }
        }

        if (imageStreams.Count == 0)
        {
            Console.WriteLine("No images were found in the PDF.");
            return;
        }

        // Merge all extracted images into a single sprite sheet (horizontal layout)
        // ImageMergeMode.Horizontal creates a side‑by‑side sprite sheet.
        // Spacing and margin are set to 0 (no extra spacing).
        Stream mergedStream = PdfConverter.MergeImages(
            imageStreams,
            ImageFormat.Png,
            ImageMergeMode.Horizontal,
            0,   // spacing
            0);  // margin

        // Save the merged sprite sheet to a PNG file
        using (FileStream outFile = new FileStream(outputPng, FileMode.Create, FileAccess.Write))
        {
            mergedStream.CopyTo(outFile);
        }

        // Clean up the individual image streams
        foreach (var s in imageStreams)
            s.Dispose();

        mergedStream.Dispose();

        Console.WriteLine($"Sprite sheet created: {outputPng}");
    }
}
