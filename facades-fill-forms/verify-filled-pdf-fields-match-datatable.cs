using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Minimal NUnit stubs – used when the NUnit package is not referenced.
// ---------------------------------------------------------------------------
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    // Additional NUnit attributes can be added here if needed (e.g., SetUp, TearDown).

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

// ---------------------------------------------------------------------------
// Dummy entry point – required because the project is built as an executable.
// In a real test project the output type would be a library, but adding a
// Main method removes the CS5001 error without affecting the unit‑test logic.
// ---------------------------------------------------------------------------
public static class Program
{
    public static void Main() { /* No‑op – entry point for the executable build */ }
}

namespace AsposePdfTests
{
    using NUnit.Framework;

    [TestFixture]
    public class AutoFillerFieldComparisonTests
    {
        private const string TemplatePdfPath = "template.pdf";
        private const string FilledPdfPath   = "filled.pdf";

        // Helper to create a sample DataTable matching the PDF form fields
        private DataTable CreateSampleDataTable()
        {
            DataTable table = new DataTable("FormData");
            // Add columns – names must match the field names in the PDF form (case‑sensitive)
            table.Columns.Add("FirstName", typeof(string));
            table.Columns.Add("LastName",  typeof(string));
            table.Columns.Add("Age",       typeof(int));
            table.Columns.Add("Subscribe", typeof(bool));

            // Add a single row of test data
            var row = table.NewRow();
            row["FirstName"] = "John";
            row["LastName"]  = "Doe";
            row["Age"]       = 30;
            row["Subscribe"] = true;
            table.Rows.Add(row);

            return table;
        }

        [Test]
        public void FilledPdfFields_ShouldMatchOriginalDataTable()
        {
            // Arrange
            var dataTable = CreateSampleDataTable();

            // Ensure any previous output file is removed
            if (File.Exists(FilledPdfPath))
                File.Delete(FilledPdfPath);

            // Act – fill the PDF using AutoFiller
            using (AutoFiller autoFiller = new AutoFiller())
            {
                // Bind the template PDF
                autoFiller.BindPdf(TemplatePdfPath);

                // Import the DataTable (column names must match field names)
                autoFiller.ImportDataTable(dataTable);

                // Save the merged result to a file
                autoFiller.Save(FilledPdfPath);
            }

            // Assert – read back the field values and compare with the DataTable
            using (Form form = new Form(FilledPdfPath))
            {
                // The DataTable contains exactly one row for this test
                var sourceRow = dataTable.Rows[0];

                foreach (DataColumn column in dataTable.Columns)
                {
                    string fieldName = column.ColumnName;
                    object expectedValue = sourceRow[fieldName];
                    object actualValue   = form.GetField(fieldName);

                    // Convert both values to string for a simple comparison (handles nulls)
                    string expectedStr = expectedValue?.ToString() ?? string.Empty;
                    string actualStr   = actualValue?.ToString()   ?? string.Empty;

                    Assert.AreEqual(expectedStr, actualStr,
                        $"Field '{fieldName}' value mismatch. Expected: '{expectedStr}', Actual: '{actualStr}'.");
                }
            }

            // Cleanup – optional removal of the generated file
            if (File.Exists(FilledPdfPath))
                File.Delete(FilledPdfPath);
        }
    }
}
