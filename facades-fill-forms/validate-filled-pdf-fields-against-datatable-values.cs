using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

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
    [NUnit.Framework.TestFixture]
    public class AutoFillerFieldComparisonTests
    {
        // Path to a PDF template that contains form fields named "FirstName", "LastName", "Address"
        private const string TemplatePdfPath = "template.pdf";

        [NUnit.Framework.Test]
        public void FilledPdfFields_ShouldMatchOriginalDataTableValues()
        {
            // ------------------------------------------------------------
            // 1. Prepare source data in a DataTable (column names must match PDF field names)
            // ------------------------------------------------------------
            DataTable sourceTable = new DataTable("MailMerge");
            sourceTable.Columns.Add("FirstName", typeof(string));
            sourceTable.Columns.Add("LastName",  typeof(string));
            sourceTable.Columns.Add("Address",   typeof(string));

            // Add a single row of test data
            sourceTable.Rows.Add("John", "Doe", "123 Main St");

            // ------------------------------------------------------------
            // 2. Fill the PDF template using AutoFiller and capture the result in a MemoryStream
            // ------------------------------------------------------------
            using (AutoFiller autoFiller = new AutoFiller())
            {
                // Bind the template PDF file
                autoFiller.BindPdf(TemplatePdfPath);

                // Import the DataTable values
                autoFiller.ImportDataTable(sourceTable);

                // Save the filled PDF into a memory stream (merged output)
                using (MemoryStream filledStream = new MemoryStream())
                {
                    autoFiller.Save(filledStream);
                    filledStream.Position = 0; // Reset for reading

                    // ------------------------------------------------------------
                    // 3. Load the filled PDF with the Form facade to read back field values
                    // ------------------------------------------------------------
                    using (Form form = new Form(filledStream))
                    {
                        // Iterate over each column/field and compare values
                        foreach (DataColumn column in sourceTable.Columns)
                        {
                            // Retrieve the value from the PDF field (returns object?)
                            object? fieldValueObj = form.GetField(column.ColumnName);
                            string fieldValue = fieldValueObj?.ToString() ?? string.Empty;

                            // Expected value from the original DataTable
                            string expectedValue = sourceTable.Rows[0][column.ColumnName]?.ToString() ?? string.Empty;

                            // Assert that the filled field matches the source value
                            NUnit.Framework.Assert.AreEqual(expectedValue, fieldValue,
                                $"Field '{column.ColumnName}' value mismatch. Expected: '{expectedValue}', Actual: '{fieldValue}'.");
                        }
                    }
                }
            }
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // No-op – the real work is performed by the NUnit test runner.
        }
    }
}
