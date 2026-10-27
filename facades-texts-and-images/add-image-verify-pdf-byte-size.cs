using System;
using System.IO;
using Aspose.Pdf;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation when the NUnit package is not referenced
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
        // Throws<T> stub (from verified fixes)
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

        // Greater<T> stub used in the test
        public static void Greater<T>(T actual, T expected, string message = null) where T : IComparable<T>
        {
            if (actual.CompareTo(expected) <= 0)
            {
                throw new Exception(message ?? $"Assert.Greater failed. Expected > {expected}, but got {actual}.");
            }
        }
    }
}

public class PdfModificationTests
{
    // Minimal 1x1 pixel PNG (transparent) byte array
    private static readonly byte[] SamplePng = new byte[]
    {
        0x89,0x50,0x4E,0x47,0x0D,0x0A,0x1A,0x0A,
        0x00,0x00,0x00,0x0D,0x49,0x48,0x44,0x52,
        0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,
        0x08,0x06,0x00,0x00,0x00,0x1F,0x15,0xC4,
        0x89,0x00,0x00,0x00,0x0A,0x49,0x44,0x41,
        0x54,0x78,0x9C,0x63,0x60,0x00,0x00,0x00,
        0x02,0x00,0x01,0xE2,0x21,0xBC,0x33,0x00,
        0x00,0x00,0x00,0x49,0x45,0x4E,0x44,0xAE,
        0x42,0x60,0x82
    };

    private string CreateTempPng()
    {
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
        File.WriteAllBytes(tempPath, SamplePng);
        return tempPath;
    }

    [Test]
    public void AddingImageIncreasesPdfSize()
    {
        // Create a simple PDF with a single blank page
        using (Document originalDoc = new Document())
        {
            originalDoc.Pages.Add();

            // Save original PDF to a memory stream to capture its size
            using (MemoryStream originalStream = new MemoryStream())
            {
                originalDoc.Save(originalStream);
                long originalSize = originalStream.Length;

                // Reset stream position before re‑loading the document
                originalStream.Position = 0;

                // Prepare a temporary PNG image
                string imagePath = CreateTempPng();

                // Load the PDF from the original stream and add the image using ImageStamp
                using (Document modifiedDoc = new Document(originalStream))
                {
                    ImageStamp imgStamp = new ImageStamp(imagePath)
                    {
                        // Position the image using XIndent/YIndent and alignment properties
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment   = VerticalAlignment.Bottom,
                        XIndent = 100, // distance from the left edge in points
                        YIndent = 100, // distance from the bottom edge in points
                        Width = 100,
                        Height = 100,
                        Background = false
                    };

                    // Add the stamp to the first page
                    modifiedDoc.Pages[1].AddStamp(imgStamp);

                    // Save the modified PDF to another memory stream
                    using (MemoryStream modifiedStream = new MemoryStream())
                    {
                        modifiedDoc.Save(modifiedStream);
                        long modifiedSize = modifiedStream.Length;

                        // Clean up temporary image file
                        File.Delete(imagePath);

                        // Verify that the modified PDF is larger than the original
                        Assert.Greater(modifiedSize, originalSize,
                            "PDF size should increase after adding an image.");
                    }
                }
            }
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    public static void Main(string[] args)
    {
        // No runtime logic required – tests are executed by the test runner.
    }
}
