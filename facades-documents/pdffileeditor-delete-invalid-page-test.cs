using System;
using System.IO;
using Aspose.Pdf;

namespace AsposePdfApi
{
    class Program
    {
        // Entry point required for a console application
        static void Main(string[] args)
        {
            try
            {
                bool testResult = DeletePageBeyondLengthThrowsException();
                Console.WriteLine($"Test DeletePageBeyondLengthThrowsException: {(testResult ? "PASSED" : "FAILED")}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Unexpected error during test execution: {ex.Message}");
            }
        }

        // Unit‑test‑style method: verifies that deleting a non‑existent page throws an exception
        static bool DeletePageBeyondLengthThrowsException()
        {
            // Create a temporary PDF with two pages
            string tempPdfPath = Path.Combine(Path.GetTempPath(), "temp_test.pdf");
            try
            {
                // Build a simple PDF document
                using (Document doc = new Document())
                {
                    // Add two blank pages
                    doc.Pages.Add();
                    doc.Pages.Add();
                    doc.Save(tempPdfPath);
                }

                // Open the document and attempt to delete a page number that exceeds the page count
                using (Document doc = new Document(tempPdfPath))
                {
                    int pageCount = doc.Pages.Count; // should be 2
                    int invalidPageNumber = pageCount + 1; // 3

                    try
                    {
                        // This call is expected to throw an exception
                        doc.Pages.Delete(invalidPageNumber);
                        // If no exception is thrown, the test fails
                        return false;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        // Expected exception type – test passes
                        return true;
                    }
                    catch (Exception)
                    {
                        // Any other exception means the behavior is not as expected
                        return false;
                    }
                }
            }
            finally
            {
                // Clean up the temporary file
                if (File.Exists(tempPdfPath))
                {
                    try { File.Delete(tempPdfPath); } catch { /* ignore cleanup errors */ }
                }
            }
        }
    }
}