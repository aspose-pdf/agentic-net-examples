using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // TextBoxField

// Minimal NUnit stubs to allow compilation without the NUnit package
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OneTimeSetUpAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OneTimeTearDownAttribute : Attribute { }

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }

        public static void IsNotNull(object obj, string message = null)
        {
            if (obj == null)
                throw new Exception(message ?? "Assert.IsNotNull failed. Object is null.");
        }
    }
}

namespace AsposePdfTests
{
    [NUnit.Framework.TestFixture]
    public class NicknameUnicodeTests
    {
        private const string UnicodeNickname = "测试😊"; // Unicode characters to test

        private string _tempDir;
        private string _templatePdfPath;
        private string _filledPdfPath;

        [NUnit.Framework.OneTimeSetUp]
        public void GlobalSetup()
        {
            // Create a temporary directory for test files
            _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_tempDir);

            // Paths for the template and filled PDFs
            _templatePdfPath = Path.Combine(_tempDir, "template.pdf");
            _filledPdfPath = Path.Combine(_tempDir, "filled.pdf");

            // Create a simple PDF with a single TextBox form field named "Nickname"
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

                TextBoxField nicknameField = new TextBoxField(page, fieldRect)
                {
                    PartialName = "Nickname",
                    // Value is non‑nullable; initialise with empty string to avoid warnings
                    Value = string.Empty
                };

                doc.Form.Add(nicknameField);
                doc.Save(_templatePdfPath);
            }
        }

        [NUnit.Framework.Test]
        public void Nickname_Should_Store_Unicode_Value_Correctly()
        {
            // Load the template, set the field value, and save the filled PDF
            using (Document doc = new Document(_templatePdfPath))
            {
                if (doc.Form["Nickname"] is TextBoxField field)
                {
                    field.Value = UnicodeNickname;
                }
                else
                {
                    throw new Exception("Nickname field not found in the template PDF.");
                }

                doc.Save(_filledPdfPath);
            }

            // Load the filled PDF and verify the field value
            using (Document loadedDoc = new Document(_filledPdfPath))
            {
                var filledField = loadedDoc.Form["Nickname"] as TextBoxField;
                NUnit.Framework.Assert.IsNotNull(filledField, "The 'Nickname' field was not found in the PDF.");

                string actualValue = filledField?.Value?.ToString() ?? string.Empty;
                NUnit.Framework.Assert.AreEqual(UnicodeNickname, actualValue, "The Unicode nickname was not stored correctly.");
            }
        }

        [NUnit.Framework.OneTimeTearDown]
        public void GlobalTeardown()
        {
            // Clean up temporary files and directory
            try
            {
                if (File.Exists(_templatePdfPath))
                    File.Delete(_templatePdfPath);
                if (File.Exists(_filledPdfPath))
                    File.Delete(_filledPdfPath);
                if (Directory.Exists(_tempDir))
                    Directory.Delete(_tempDir, true);
            }
            catch
            {
                // Ignored – cleanup failures should not affect test results
            }
        }
    }

    // Dummy entry point to satisfy the compiler when building as an executable.
    public class Program
    {
        public static void Main() { }
    }
}
