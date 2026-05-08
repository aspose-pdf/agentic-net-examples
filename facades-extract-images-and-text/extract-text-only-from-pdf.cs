using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace MyApp
{
    class Program
    {
        static void Main()
        {
            const string inputPdf = "input.pdf";
            const string outputTxt = "output.txt";

            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"File not found: {inputPdf}");
                return;
            }

            // Initialize the PdfExtractor facade
            PdfExtractor extractor = new PdfExtractor();

            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdf);

            // Do NOT call ExtractImage(); only extract text.
            // extractor.ExtractImage(); // intentionally omitted to ignore images

            // Extract only the text content
            extractor.ExtractText();

            // Save the extracted text to a file
            extractor.GetText(outputTxt);

            // Release resources held by the extractor
            extractor.Close();

            Console.WriteLine($"Text extracted to '{outputTxt}' (images ignored).");
        }
    }
}

// Minimal NUnit stubs for compilation when the NUnit package is not referenced
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OneTimeSetUpAttribute : Attribute { }

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}