using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

// Minimal NUnit stubs to allow compilation without the NUnit package
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        // Allow null for the optional message parameter (nullable reference type)
        public static void AreEqual<T>(T expected, T actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

namespace AsposePdfTests
{
    [NUnit.Framework.TestFixture]
    public class TextPositionTests
    {
        // Expected coordinates for the added text
        private const double ExpectedX = 150.0;
        private const double ExpectedY = 300.0;
        private const string TestText = "Sample Text";

        [NUnit.Framework.Test]
        public void AddedText_ShouldAppearAtExpectedCoordinates()
        {
            // Create a new PDF document with a single blank page
            using (var doc = new Document())
            {
                doc.Pages.Add();

                // Create a text fragment with the desired position
                var textFragment = new TextFragment(TestText);
                textFragment.Position = new Position(ExpectedX, ExpectedY);

                // Append the fragment to the first page using TextBuilder
                var builder = new TextBuilder(doc.Pages[1]);
                builder.AppendText(textFragment);

                // Save the document to a memory stream
                using (var ms = new MemoryStream())
                {
                    doc.Save(ms);
                    ms.Position = 0; // Reset stream for reading

                    // Load the document back from the stream
                    using (var loadedDoc = new Document(ms))
                    {
                        // Extract text fragments from the first page
                        var absorber = new TextFragmentAbsorber();
                        loadedDoc.Pages[1].Accept(absorber);

                        // Ensure at least one fragment was found
                        if (absorber.TextFragments.Count == 0)
                            throw new Exception("No text fragments were found in the document.");

                        // Retrieve the first (and only) fragment (1‑based indexing)
                        var extractedFragment = absorber.TextFragments[1];

                        // Verify that the extracted coordinates match the expected values
                        NUnit.Framework.Assert.AreEqual(ExpectedX, extractedFragment.Position.XIndent,
                            "X coordinate does not match the expected value.");
                        NUnit.Framework.Assert.AreEqual(ExpectedY, extractedFragment.Position.YIndent,
                            "Y coordinate does not match the expected value.");
                    }
                }
            }
        }
    }

    // Dummy entry point to satisfy the console application requirement.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // No operation – tests are discovered and run by the test runner.
        }
    }
}
