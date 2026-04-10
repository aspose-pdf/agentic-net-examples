using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // <-- added for TextFragment
using NUnit.Framework;

// -----------------------------------------------------------------------------
// Minimal NUnit stubs – added because the project does not reference the real
// NUnit package. These stubs provide the attributes and Assert methods used in
// the test code, allowing it to compile and run.
// -----------------------------------------------------------------------------
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    // The test delegate type used by Assert.Throws<T>.
    public delegate void TestDelegate();

    public static class Assert
    {
        // Simple equality assertion – not used in the current test but kept for
        // completeness.
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }

        // Generic Throws implementation that matches the behaviour of NUnit's
        // Assert.Throws<T>. It executes the supplied delegate and returns the
        // caught exception of the expected type, or throws a descriptive error
        // if the exception type is different or no exception is thrown.
        public static T Throws<T>(TestDelegate code) where T : Exception
        {
            try
            {
                code();
            }
            catch (T ex)
            {
                return ex; // Expected exception was thrown.
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

        private string CreateSimplePdf()
        {
            // Create a one‑page PDF with a single text fragment.
            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pdf");
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                TextFragment tf = new TextFragment("Sample PDF for encryption");
                page.Paragraphs.Add(tf);
                doc.Save(tempPath);
            }
            return tempPath;
        }

        [Test]
        public void Decrypt_WithIncorrectOwnerPassword_ShouldThrowException()
        {
            // Arrange: create a PDF and encrypt it with a known owner password.
            string originalPdf = CreateSimplePdf();
            string encryptedPdf = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pdf");
            string decryptedPdf = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pdf");

            try
            {
                // Encrypt the PDF using PdfFileSecurity (facade API).
                PdfFileSecurity encryptor = new PdfFileSecurity(originalPdf, encryptedPdf);
                bool encryptResult = encryptor.EncryptFile(UserPassword, OwnerPassword,
                                                            DocumentPrivilege.Print, KeySize.x256);
                Assert.AreEqual(true, encryptResult, "Encryption should succeed.");

                // Act & Assert: attempt to decrypt with an incorrect owner password.
                PdfFileSecurity decryptor = new PdfFileSecurity(encryptedPdf, decryptedPdf);
                Assert.Throws<InvalidPasswordException>(() =>
                {
                    // DecryptFile throws when the password is invalid.
                    decryptor.DecryptFile(WrongOwnerPassword);
                });
            }
            finally
            {
                // Cleanup temporary files.
                foreach (var file in new[] { originalPdf, encryptedPdf, decryptedPdf })
                {
                    try { if (File.Exists(file)) File.Delete(file); } catch { /* ignore */ }
                }
            }
        }
    }
}

// Dummy entry point so the project builds as a console application.
public class Program
{
    public static void Main()
    {
        // No runtime logic required – tests are discovered and run by the test runner.
    }
}
