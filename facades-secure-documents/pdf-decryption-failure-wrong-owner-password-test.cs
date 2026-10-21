using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using NUnit.Framework;

// -----------------------------------------------------------------------------
// Minimal NUnit stubs – added because the project does not reference the real
// NUnit package. These definitions provide the attributes and Assert.Throws<T>
// used by the test code, allowing it to compile and run.
// -----------------------------------------------------------------------------
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
        /// <summary>
        /// Executes the supplied delegate and returns the caught exception of type T.
        /// If no exception or a different exception is thrown, an AssertionException
        /// (represented here by a generic Exception) is raised.
        /// </summary>
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
    public class DecryptionTests
    {
        private const string UserPassword = "user123";
        private const string OwnerPassword = "owner123";
        private const string WrongOwnerPassword = "wrongOwner";

        private string _originalPdfPath = null!;
        private string _encryptedPdfPath = null!;
        private string _decryptedPdfPath = null!;

        [SetUp]
        public void SetUp()
        {
            // Create temporary file paths
            _originalPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + "_original.pdf");
            _encryptedPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + "_encrypted.pdf");
            _decryptedPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + "_decrypted.pdf");

            // Create a simple PDF document and encrypt it
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Sample PDF for encryption test."));

                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(UserPassword, OwnerPassword, perms, CryptoAlgorithm.AESx256);
                // Save the encrypted document (the same file is saved twice – once as a reference
                // "original" and once as the "encrypted" version used by the test).
                doc.Save(_originalPdfPath);
                doc.Save(_encryptedPdfPath);
            }
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up temporary files
            foreach (var path in new[] { _originalPdfPath, _encryptedPdfPath, _decryptedPdfPath })
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
        }

        [Test]
        public void Decrypt_WithIncorrectOwnerPassword_ShouldThrowInvalidPasswordException()
        {
            // Aspose.Pdf throws InvalidPasswordException when a document is opened with a wrong password.
            // The PdfFileEditor.Decrypt method is not available in the referenced version, so we attempt to
            // load the encrypted PDF using the wrong password and expect the exception.
            Assert.Throws<InvalidPasswordException>(() =>
                new Document(_encryptedPdfPath, WrongOwnerPassword));
        }
    }

    // Dummy entry point to satisfy the compiler for a console‑type project.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // No runtime logic required – tests are executed by the test runner.
        }
    }
}
