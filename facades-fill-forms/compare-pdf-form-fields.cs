using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using NUnit.Framework;

// Minimal NUnit stubs (used when the real NUnit package is not referenced)
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OneTimeSetUpAttribute : Attribute { }

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
            {
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
            }
        }
    }
}

namespace PdfFormTests
{
    [TestFixture]
    public class FormFillingTests
    {
        private const string TemplatePath = "template.pdf";
        private const string OutputPath = "filled.pdf";

        [Test]
        public void VerifyFilledFieldsMatchDataTable()
        {
            // Prepare sample data that matches the PDF form field names
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("FirstName", typeof(string));
            dataTable.Columns.Add("LastName", typeof(string));
            DataRow row = dataTable.NewRow();
            row["FirstName"] = "John";
            row["LastName"] = "Smith";
            dataTable.Rows.Add(row);

            // Fill the PDF form using AutoFiller
            using (AutoFiller filler = new AutoFiller())
            {
                filler.BindPdf(TemplatePath);
                filler.ImportDataTable(dataTable);
                filler.Save(OutputPath);
            }

            // Open the filled PDF and verify that each field value matches the DataTable entry
            using (Document doc = new Document(OutputPath))
            {
                Aspose.Pdf.Forms.Form form = doc.Form;
                if (form != null)
                {
                    foreach (Field field in form.Fields)
                    {
                        string fieldName = field.FullName ?? field.PartialName;
                        if (fieldName != null && dataTable.Columns.Contains(fieldName))
                        {
                            string expected = dataTable.Rows[0][fieldName].ToString();
                            string actual = field.Value != null ? field.Value.ToString() : string.Empty;
                            Assert.AreEqual(expected, actual, $"Field '{fieldName}' value does not match.");
                        }
                    }
                }
            }
        }
    }

    public static class Program
    {
        public static void Main(string[] args)
        {
            // Required entry point for compilation; unit tests are executed via a test runner.
        }
    }
}