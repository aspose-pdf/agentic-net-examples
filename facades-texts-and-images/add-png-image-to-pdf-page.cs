using System;
using System.IO;
using Aspose.Pdf.Facades;

// Minimal NUnit stubs to allow compilation when the NUnit package is not referenced.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OneTimeSetUpAttribute : Attribute { }

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
            try { code(); }
            catch (T ex) { return ex; }
            catch (Exception ex) { throw new Exception($"Assert.Throws failed. Expected {typeof(T)} but got {ex.GetType()}.", ex); }
            throw new Exception($"Assert.Throws failed. No exception thrown. Expected {typeof(T)}.");
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "image.png";

        // Coordinates of the image rectangle (lower‑left X/Y, upper‑right X/Y)
        float lowerLeftX = 100f;
        float lowerLeftY = 200f;
        float upperRightX = 300f;
        float upperRightY = 400f;

        // Validate input files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create the PdfFileMend facade and bind the source PDF
        using (PdfFileMend mender = new PdfFileMend())
        {
            mender.BindPdf(inputPdf);

            // Add the PNG image to page 2 (pages are 1‑based)
            bool success = mender.AddImage(imagePath, 2, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
            if (!success)
            {
                Console.Error.WriteLine("Failed to add the image to the PDF.");
            }

            // Save the modified PDF
            mender.Save(outputPdf);
        }

        Console.WriteLine($"Image added and PDF saved to '{outputPdf}'.");
    }
}
