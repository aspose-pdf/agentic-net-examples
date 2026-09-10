using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation when the real NUnit package is not referenced.
// These stubs provide the attributes and Assert.Throws<T> used in the test.
// In a real test project you would reference the NUnit NuGet package instead.
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

namespace AsposePdfTests
{
    [TestFixture]
    public class PdfDecryptionTests
    {
        private const string OwnerPassword = "owner123";
        private const string UserPassword = "user123";
        private const string WrongOwnerPassword = "wrongpass";

        private string _tempDir;
        private string _originalPdfPath;
        private string _encryptedPdfPath;

        [SetUp]
        public void SetUp()
        {
            // Create a temporary directory for test files
            _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_tempDir);

            _originalPdfPath = Path.Combine(_tempDir, "original.pdf");
            _encryptedPdfPath = Path.Combine(_tempDir, "encrypted.pdf");

            // Create a simple PDF document
            using (Document doc = new Document())
            {
                // Add a page
                Page page = doc.Pages.Add();

                // Add some text to the page
                TextFragment tf = new TextFragment("Sample PDF for encryption test.");
                page.Paragraphs.Add(tf);

                // Save the unencrypted PDF
                doc.Save(_originalPdfPath);
            }

            // Encrypt the PDF using the correct owner password
            PdfFileSecurity encryptor = new PdfFileSecurity(_originalPdfPath, _encryptedPdfPath);
            // EncryptFile throws if it fails; we ignore the return value for the test setup
            encryptor.EncryptFile(UserPassword, OwnerPassword, DocumentPrivilege.Print, KeySize.x256);
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up temporary files and directory
            try
            {
                if (File.Exists(_originalPdfPath))
                    File.Delete(_originalPdfPath);
                if (File.Exists(_encryptedPdfPath))
                    File.Delete(_encryptedPdfPath);
                if (Directory.Exists(_tempDir))
                    Directory.Delete(_tempDir, true);
            }
            catch
            {
                // Ignored – cleanup should not affect test results
            }
        }

        [Test]
        public void DecryptFile_WithIncorrectOwnerPassword_ShouldThrowException()
        {
            // Arrange: create a PdfFileSecurity instance for the encrypted file
            PdfFileSecurity decryptor = new PdfFileSecurity(_encryptedPdfPath, Path.Combine(_tempDir, "decrypted.pdf"));

            // Act & Assert: DecryptFile should throw when an incorrect owner password is supplied
            Assert.Throws<InvalidPasswordException>(() =>
            {
                // This call is expected to fail because the password is wrong
                decryptor.DecryptFile(WrongOwnerPassword);
            });
        }
    }
}

// Dummy entry point to satisfy the compiler for a console‑style project.
public static class Program
{
    public static void Main()
    {
        // No operation – the test runner (or manual execution) will invoke the tests.
    }
}
