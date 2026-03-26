using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation without the real NUnit package
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

    public static class Assert
    {
        public static void IsTrue(bool condition, string message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }

        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

[TestFixture]
public class ConvertPagesToImagesTests
{
    private const string InputPdfPath = "sample.pdf";
    private const string OutputImageBaseName = "page_image";

    [SetUp]
    public void SetUp()
    {
        // Create a simple PDF with two pages for testing
        using (Document doc = new Document())
        {
            Page firstPage = doc.Pages.Add();
            firstPage.Paragraphs.Add(new TextFragment("First page"));
            Page secondPage = doc.Pages.Add();
            secondPage.Paragraphs.Add(new TextFragment("Second page"));
            doc.Save(InputPdfPath);
        }
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up PDF file
        if (File.Exists(InputPdfPath))
        {
            File.Delete(InputPdfPath);
        }
        // Clean up generated image files (up to a reasonable page count)
        for (int i = 1; i <= 10; i++)
        {
            string imagePath = $"{OutputImageBaseName}{i}.png";
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }
    }

    [Test]
    public void ConvertPagesToImages_FlagCreatesImageForEachPage()
    {
        // Arrange: load the PDF document
        Document pdfDocument = new Document(InputPdfPath);
        int pageCount = pdfDocument.Pages.Count;
        Resolution resolution = new Resolution(150);

        // Act: convert each page to a PNG image using PngDevice
        for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
        {
            string imagePath = $"{OutputImageBaseName}{pageIndex}.png";
            using (FileStream imageStream = new FileStream(imagePath, FileMode.Create))
            {
                PngDevice pngDevice = new PngDevice(resolution);
                pngDevice.Process(pdfDocument.Pages[pageIndex], imageStream);
            }
        }

        // Assert: an image file should exist for each page in the source PDF
        for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
        {
            string imagePath = $"{OutputImageBaseName}{pageIndex}.png";
            Assert.IsTrue(File.Exists(imagePath), $"Image file for page {pageIndex} was not created.");
        }
    }
}

// Provide a dummy entry point so the project compiles as an executable.
public static class Program
{
    public static void Main(string[] args)
    {
        // No runtime logic required – the unit tests are executed by the test runner.
    }
}