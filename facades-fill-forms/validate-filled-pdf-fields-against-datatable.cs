using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;
using NUnit.Framework; // Added to bring stub types into scope

// Minimal NUnit stubs to allow compilation without the NUnit package.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class AutoFillerFieldComparisonTests
    {
        // Path to a PDF template that contains form fields named "FirstName", "LastName", "Address".
        private const string TemplatePdfPath = "template.pdf";

        [Test]
        public void FilledPdfFieldsShouldMatchOriginalDataTable()
        {
            // ------------------------------------------------------------
            // 1. Prepare a DataTable with column names matching the PDF fields.
            // ------------------------------------------------------------
            DataTable sourceTable = new DataTable("FormData");
            sourceTable.Columns.Add("FirstName", typeof(string));
            sourceTable.Columns.Add("LastName",  typeof(string));
            sourceTable.Columns.Add("Address",   typeof(string));

            // Add a single row of test data.
            sourceTable.Rows.Add("John", "Doe", "123 Main St");

            // ------------------------------------------------------------
            // 2. Use AutoFiller to merge the data into the PDF template.
            //    The result is written into a memory stream (no file I/O).
            // ------------------------------------------------------------
            using (MemoryStream filledPdfStream = new MemoryStream())
            using (AutoFiller autoFiller = new AutoFiller())
            {
                // Bind the template PDF using the recommended API (instead of the obsolete InputFileName).
                autoFiller.BindPdf(TemplatePdfPath);

                // Direct the output to the memory stream.
                autoFiller.OutputStream = filledPdfStream;

                // Import the DataTable.
                autoFiller.ImportDataTable(sourceTable);

                // Perform the fill operation.
                autoFiller.Save();

                // Reset the stream position so it can be read later.
                filledPdfStream.Position = 0;

                // ------------------------------------------------------------
                // 3. Open the filled PDF with the Form facade to read field values.
                // ------------------------------------------------------------
                using (Form filledForm = new Form(filledPdfStream))
                {
                    // Iterate over each column (field) and compare values.
                    foreach (DataColumn column in sourceTable.Columns)
                    {
                        string expectedValue = sourceTable.Rows[0][column].ToString();

                        // GetField returns the current value of the specified field.
                        string actualValue = filledForm.GetField(column.ColumnName);

                        // Assert that the filled value matches the original data.
                        Assert.AreEqual(
                            expectedValue,
                            actualValue,
                            $"Field '{column.ColumnName}' does not match the source data.");
                    }
                }
            }
        }
    }

    // Adding a minimal entry point so the project builds as a console application.
    // The method does nothing because the real execution is performed by the unit test runner.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // No-op – required for a valid entry point.
        }
    }
}
