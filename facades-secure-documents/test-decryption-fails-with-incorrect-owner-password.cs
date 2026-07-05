using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using NUnit.Framework; // Added using directive for NUnit stubs

// Minimal NUnit stubs to allow compilation when the NUnit package is not referenced.
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
        public static void IsTrue(bool condition, string? message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
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

namespace AsposePdfTests
{
    [TestFixture]
    public class PdfFileSecurityTests
    {
        private const string OwnerPassword = "owner123";
        private const string UserPassword = "user123";
        private const string WrongOwnerPassword = "wrongpass";

        private string _tempDir = string.Empty;

        [SetUp]
        public void SetUp()
        {
            // Create a temporary directory for test files.
            _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_tempDir);
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up all files created during the test.
            if (Directory.Exists(_tempDir))
            {
                Directory.Delete(_tempDir, true);
            }
        }

        [Test]
        public void DecryptFile_WithIncorrectOwnerPassword_ShouldThrowInvalidPasswordException()
        {
            // Paths for the intermediate files.
            string plainPdfPath = Path.Combine(_tempDir, "plain.pdf");
            string encryptedPdfPath = Path.Combine(_tempDir, "encrypted.pdf");
            string decryptedPdfPath = Path.Combine(_tempDir, "decrypted.pdf");

            // -----------------------------------------------------------------
            // 1. Create a simple PDF document (one page with a text fragment).
            // -----------------------------------------------------------------
            using (Document doc = new Document())
            {
                // Add a page.
                Page page = doc.Pages.Add();

                // Add a text fragment to the page.
                TextFragment tf = new TextFragment("Aspose.Pdf unit test");
                page.Paragraphs.Add(tf);

                // Save the unencrypted PDF.
                doc.Save(plainPdfPath);
            }

            // -----------------------------------------------------------------
            // 2. Encrypt the PDF using a known owner password.
            // -----------------------------------------------------------------
            using (PdfFileSecurity encryptor = new PdfFileSecurity(plainPdfPath, encryptedPdfPath))
            {
                // Encrypt with user and owner passwords, allowing printing.
                bool encryptResult = encryptor.EncryptFile(
                    UserPassword,
                    OwnerPassword,
                    DocumentPrivilege.Print,
                    KeySize.x256);

                Assert.IsTrue(encryptResult, "Encryption should succeed.");
            }

            // -----------------------------------------------------------------
            // 3. Attempt to decrypt with an incorrect owner password.
            //    DecryptFile is expected to throw InvalidPasswordException.
            // -----------------------------------------------------------------
            using (PdfFileSecurity decryptor = new PdfFileSecurity(encryptedPdfPath, decryptedPdfPath))
            {
                // The lambda invokes DecryptFile with a wrong password.
                // Assert that the specific exception is thrown.
                Assert.Throws<InvalidPasswordException>(() => decryptor.DecryptFile(WrongOwnerPassword));
            }

            // No further assertions are needed; the test passes if the exception is thrown.
        }
    }

    // Simple entry point to satisfy the compiler when building as an executable.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // The test runner (e.g., dotnet test) will discover and execute the tests.
            // This Main method is only present to provide a valid entry point for the project.
        }
    }
}
