using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Aspose.Pdf;

namespace AsposePdfTests.Integration
{
    class Program
    {
        static void Main()
        {
            // Setup temporary working directory
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);

            string pdfPath = Path.Combine(tempDir, "test.pdf");
            string attachment1Path = Path.Combine(tempDir, "attach1.txt");
            string attachment2Path = Path.Combine(tempDir, "attach2.txt");

            try
            {
                // Create simple attachment files
                File.WriteAllText(attachment1Path, "Attachment 1 content");
                File.WriteAllText(attachment2Path, "Attachment 2 content");

                // Create a minimal PDF document with a single blank page and save it
                using (var doc = new Document())
                {
                    doc.Pages.Add();
                    doc.Save(pdfPath);
                }

                // Load PDF and add two embedded files
                using (var doc = new Document(pdfPath))
                {
                    AddEmbeddedFile(doc, attachment1Path);
                    AddEmbeddedFile(doc, attachment2Path);

                    // Verify that both embedded files are present
                    if (doc.EmbeddedFiles.Count == 2)
                        Console.WriteLine("PASS: Embedded files count is 2 after adding.");
                    else
                        Console.WriteLine($"FAIL: Expected 2 embedded files, found {doc.EmbeddedFiles.Count}.");

                    // Remove the first embedded file by its file name (not full path)
                    string fileNameToRemove = Path.GetFileName(attachment1Path);
                    doc.EmbeddedFiles.Delete(fileNameToRemove);

                    // Verify that the count decreased to 1
                    if (doc.EmbeddedFiles.Count == 1)
                        Console.WriteLine("PASS: Embedded files count is 1 after removal.");
                    else
                        Console.WriteLine($"FAIL: Expected 1 embedded file after removal, found {doc.EmbeddedFiles.Count}.");

                    // Persist changes
                    doc.Save(pdfPath);
                }

                // Reload the PDF to ensure the removal persisted
                using (var doc = new Document(pdfPath))
                {
                    if (doc.EmbeddedFiles.Count == 1)
                        Console.WriteLine("PASS: Embedded files count remains 1 after reloading.");
                    else
                        Console.WriteLine($"FAIL: Expected 1 embedded file after reload, found {doc.EmbeddedFiles.Count}.");
                }
            }
            finally
            {
                // Clean up temporary files and directory
                try
                {
                    if (Directory.Exists(tempDir))
                        Directory.Delete(tempDir, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Cleanup error: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Helper that creates a FileSpecification from a file on disk and adds it to the document's EmbeddedFiles collection.
        /// </summary>
        private static void AddEmbeddedFile(Document doc, string filePath)
        {
            var fileSpec = new FileSpecification(Path.GetFileName(filePath));
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(filePath));
            doc.EmbeddedFiles.Add(fileSpec);
        }
    }
}
