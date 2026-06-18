using System;
using System.IO;
using Aspose.Pdf;

// Minimal NUnit stubs to satisfy compilation when the NUnit package is not referenced.
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
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_images.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf collections are 1‑based, so iterate pages accordingly.
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                var images = page.Resources.Images;

                // Remove images by iterating backwards and calling Delete (1‑based index).
                for (int imgIndex = images.Count; imgIndex >= 1; imgIndex--)
                {
                    images.Delete(imgIndex);
                }
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"All images removed. Saved to '{outputPath}'.");
    }
}
