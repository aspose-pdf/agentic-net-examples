using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation without the actual NUnit package.
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
        public static void IsTrue(bool condition, string message = null)
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
    public class PdfDecryptionTests
    {
        private const string UserPassword = "user123";
        private const string OwnerPassword = "owner123";
        private const string WrongOwnerPassword = "wrongOwner";

        private string _originalPdfPath;
        private string _encryptedPdfPath;
        private string _decryptedPdfPath;

        [SetUp]
        public void SetUp()
        {
            // Create temporary file paths
            string tempDir = Path.GetTempPath();
            _originalPdfPath = Path.Combine(tempDir, Guid.NewGuid().ToString() + "_original.pdf");
            _encryptedPdfPath = Path.Combine(tempDir, Guid.NewGuid().ToString() + "_encrypted.pdf");
            _decryptedPdfPath = Path.Combine(tempDir, Guid.NewGuid().ToString() + "_decrypted.pdf");

            // Create a simple PDF document
            using (Document doc = new Document())
            {
                // Add a page and some text
                Page page = doc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Sample PDF for encryption test."));
                // Save the unencrypted PDF
                doc.Save(_originalPdfPath);
            }

            // Encrypt the PDF using PdfFileSecurity (owner password is required for decryption)
            using (PdfFileSecurity encryptor = new PdfFileSecurity(_originalPdfPath, _encryptedPdfPath))
            {
                bool encryptResult = encryptor.EncryptFile(UserPassword, OwnerPassword, DocumentPrivilege.Print, KeySize.x256);
                Assert.IsTrue(encryptResult, "Encryption should succeed.");
            }
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up temporary files
            foreach (var path in new[] { _originalPdfPath, _encryptedPdfPath, _decryptedPdfPath })
            {
                try
                {
                    if (File.Exists(path))
                        File.Delete(path);
                }
                catch
                {
                    // Ignored – best‑effort cleanup
                }
            }
        }

        [Test]
        public void Decrypt_WithIncorrectOwnerPassword_ShouldThrowException()
        {
            // Attempt to decrypt using an incorrect owner password.
            using (PdfFileSecurity decryptor = new PdfFileSecurity(_encryptedPdfPath, _decryptedPdfPath))
            {
                // DecryptFile throws an exception when the password is invalid.
                Assert.Throws<Exception>(() => decryptor.DecryptFile(WrongOwnerPassword));
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when building an executable.
public class Program
{
    public static void Main()
    {
        // No operation – tests are executed by the test runner.
    }
}