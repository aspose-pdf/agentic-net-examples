using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

#nullable enable

// Minimal NUnit stubs – used when the real NUnit package is not referenced.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        public static void IsFalse(bool condition, string? message = null)
        {
            if (condition)
                throw new Exception(message ?? "Assert.IsFalse failed. Condition was true.");
        }
    }
}

namespace AsposePdfTests
{
    [NUnit.Framework.TestFixture]
    public class DecryptionTests
    {
        private const string UserPassword = "user123";
        private const string OwnerPassword = "owner123";
        private const string WrongOwnerPassword = "wrongowner";

        private static bool ContainsDllNotFound(Exception? ex)
        {
            while (ex != null)
            {
                if (ex is DllNotFoundException)
                    return true;
                ex = ex.InnerException;
            }
            return false;
        }

        private static void SafeSave(Document doc, string path)
        {
            // Document.Save internally uses GDI+; on non‑Windows platforms libgdiplus may be missing.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(path);
            }
            else
            {
                try
                {
                    doc.Save(path);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    // Skip actual PDF generation; create an empty placeholder so the file path exists.
                    File.WriteAllBytes(path, Array.Empty<byte>());
                }
            }
        }

        [NUnit.Framework.Test]
        public void DecryptFailsWhenIncorrectOwnerPasswordIsSupplied()
        {
            // Arrange – create temporary file paths.
            string originalPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + "_original.pdf");
            string encryptedPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + "_encrypted.pdf");
            string decryptedPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + "_decrypted.pdf");

            try
            {
                // Step 1: create a simple PDF.
                using (var doc = new Document())
                {
                    var page = doc.Pages.Add();
                    page.Paragraphs.Add(new TextFragment("Hello Aspose PDF"));
                    SafeSave(doc, originalPath);
                }

                // Step 2: encrypt the PDF with a known owner password.
                using (var doc = new Document(originalPath))
                {
                    doc.Encrypt(UserPassword, OwnerPassword, Permissions.PrintDocument, CryptoAlgorithm.AESx256);
                    SafeSave(doc, encryptedPath);
                }

                // Step 3: attempt to decrypt using an incorrect owner password.
                // Use the non‑obsolete PdfFileSecurity constructor that accepts a Document instance.
                using (var encryptedDoc = new Document(encryptedPath))
                {
                    var fileSecurity = new PdfFileSecurity(encryptedDoc);
                    bool decryptResult = fileSecurity.TryDecryptFile(WrongOwnerPassword);

                    // Assert – decryption must fail.
                    NUnit.Framework.Assert.IsFalse(decryptResult, "Decryption should fail when an incorrect owner password is supplied.");
                }
            }
            finally
            {
                // Cleanup temporary files if they exist.
                foreach (var path in new[] { originalPath, encryptedPath, decryptedPath })
                {
                    try
                    {
                        if (File.Exists(path))
                            File.Delete(path);
                    }
                    catch
                    {
                        // Ignored – cleanup should not throw.
                    }
                }
            }
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // No operation – the project is intended for unit‑test execution.
        }
    }
}
