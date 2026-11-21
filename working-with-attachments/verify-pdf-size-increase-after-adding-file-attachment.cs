using System;
using System.IO;
using Aspose.Pdf;

namespace AsposePdfTests
{
    class Program
    {
        private static string _tempDir;
        private static string _originalPdfPath;
        private static string _updatedPdfPath;
        private static string _attachmentPath;

        static void Main()
        {
            bool overallSuccess = true;
            try
            {
                SetUp();
                if (!TestAddingAttachment_IncreasesFileSize())
                {
                    overallSuccess = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FAIL Unexpected exception: {ex.Message}");
                overallSuccess = false;
            }
            finally
            {
                try
                {
                    TearDown();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Warning: cleanup failed - {ex.Message}");
                }
            }

            Console.WriteLine(overallSuccess ? "ALL TESTS PASSED" : "ONE OR MORE TESTS FAILED");
        }

        private static void SetUp()
        {
            // Create a temporary directory for test files
            _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_tempDir);

            // Paths for the PDFs and the attachment file
            _originalPdfPath = Path.Combine(_tempDir, "original.pdf");
            _updatedPdfPath = Path.Combine(_tempDir, "updated.pdf");
            _attachmentPath = Path.Combine(_tempDir, "sample.txt");

            // Create a simple text file to be used as attachment
            File.WriteAllText(_attachmentPath, "This is a sample attachment used for testing.");

            // Create a minimal PDF (one blank page) and save it as the original file
            using (Document doc = new Document())
            {
                // Add a blank page (Pages are 1‑based)
                doc.Pages.Add();
                doc.Save(_originalPdfPath);
            }
        }

        private static void TearDown()
        {
            // Clean up all temporary files and directory
            if (Directory.Exists(_tempDir))
            {
                Directory.Delete(_tempDir, true);
            }
        }

        private static bool TestAddingAttachment_IncreasesFileSize()
        {
            const string testName = "AddingAttachment_IncreasesFileSize";
            try
            {
                // Get size of the original PDF
                long originalSize = new FileInfo(_originalPdfPath).Length;

                // Load the original PDF, add the attachment, and save as a new file
                using (Document doc = new Document(_originalPdfPath))
                {
                    // Create a FileSpecification for the attachment
                    var attachmentSpec = new FileSpecification(Path.GetFileName(_attachmentPath), "Test attachment");
                    attachmentSpec.Contents = new MemoryStream(File.ReadAllBytes(_attachmentPath));

                    // Add the attachment to the PDF via the EmbeddedFiles collection
                    doc.EmbeddedFiles.Add(attachmentSpec);

                    // Save the modified PDF
                    doc.Save(_updatedPdfPath);
                }

                // Get size of the PDF after attachment addition
                long updatedSize = new FileInfo(_updatedPdfPath).Length;

                // Verify that the file size has increased
                if (updatedSize > originalSize)
                {
                    Console.WriteLine($"PASS {testName}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"FAIL {testName}: updated size ({updatedSize}) is not greater than original size ({originalSize})");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FAIL {testName}: exception {ex.GetType().Name} - {ex.Message}");
                return false;
            }
        }
    }
}
