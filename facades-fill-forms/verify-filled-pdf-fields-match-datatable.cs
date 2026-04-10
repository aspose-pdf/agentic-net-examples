using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ---------------------------------------------------------------------------
// Minimal MSTest stubs – added only when the real Microsoft.VisualStudio.TestTools.UnitTesting
// assembly is not referenced. They provide the attributes and Assert methods used in the
// test code so the project can compile without pulling the full MSTest package.
// ---------------------------------------------------------------------------
namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestClassAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestMethodAttribute : Attribute { }

    public static class Assert
    {
        // Made the optional message parameter nullable to silence CS8625/CS8604 warnings.
        public static void AreEqual(string expected, string actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
            {
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
            }
        }
    }
}

namespace AsposePdfFacadesTests
{
    [TestClass]
    public class AutoFillerFieldComparisonTests
    {
        private const string TemplatePdfPath = "template.pdf";   // PDF with form fields matching column names
        private const string OutputPdfPath   = "filled_output.pdf";

        [TestMethod]
        public void FilledPdfFieldsShouldMatchOriginalDataTable()
        {
            // ---------------------------------------------------------------
            // 1. Build a DataTable whose column names correspond to PDF fields.
            // ---------------------------------------------------------------
            DataTable sourceTable = new DataTable("FormData");
            sourceTable.Columns.Add("FirstName", typeof(string));
            sourceTable.Columns.Add("LastName",  typeof(string));
            sourceTable.Columns.Add("Email",     typeof(string));
            sourceTable.Rows.Add("John", "Doe", "john.doe@example.com");

            // Remove any leftover output from previous runs.
            if (File.Exists(OutputPdfPath))
                File.Delete(OutputPdfPath);

            // ---------------------------------------------------------------
            // 2. Fill the template PDF using Aspose.Pdf.Facades.AutoFiller.
            // ---------------------------------------------------------------
            using (AutoFiller filler = new AutoFiller())
            {
                filler.BindPdf(TemplatePdfPath);   // bind the template (input mode)
                filler.ImportDataTable(sourceTable); // each row creates a filled document
                filler.Save(OutputPdfPath);        // write the result to disk
            }

            // ---------------------------------------------------------------
            // 3. Open the generated PDF and compare each field value with the source data.
            // ---------------------------------------------------------------
            using (Form filledForm = new Form(OutputPdfPath))
            {
                foreach (DataColumn column in sourceTable.Columns)
                {
                    string fieldName      = column.ColumnName;
                    string expectedValue  = sourceTable.Rows[0][fieldName].ToString();
                    // GetField may return null; coalesce to empty string to satisfy non‑nullable contract.
                    string actualValue    = filledForm.GetField(fieldName) ?? string.Empty;

                    Assert.AreEqual(expectedValue, actualValue,
                        $"Field '{fieldName}' value mismatch. Expected: '{expectedValue}', Actual: '{actualValue}'.");
                }
            }

            // Clean up the generated PDF after verification.
            if (File.Exists(OutputPdfPath))
                File.Delete(OutputPdfPath);
        }
    }
}

// ---------------------------------------------------------------------------
// Minimal entry point so the project builds as a console application.
// The test runner (MSTest) will discover and execute the test method above.
// ---------------------------------------------------------------------------
public class Program
{
    public static void Main(string[] args)
    {
        // No runtime logic required – the presence of Main satisfies the compiler.
        // Tests can be run via the MSTest runner.
    }
}
