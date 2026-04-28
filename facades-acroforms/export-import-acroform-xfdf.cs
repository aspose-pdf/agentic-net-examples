using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Minimal stubs for MSTest attributes to allow compilation when the test
// framework is not referenced. These are only needed for the build errors
// reported (Microsoft.VisualStudio.TestTools.UnitTesting namespace).
// ---------------------------------------------------------------------------
namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestClassAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestMethodAttribute : Attribute { }
}

namespace AsposePdfFormRoundTrip
{
    class Program
    {
        static void Main(string[] args)
        {
            const string inputPdf = "FormSample.pdf";      // source PDF with AcroForm fields
            const string outputPdf = "FormRoundTrip.pdf"; // result after import
            const string xfdfPath = "exported.xfdf";      // optional XFDF file for inspection

            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"Input file not found: {inputPdf}");
                return;
            }

            // STEP 1: Load PDF into Form facade (binds the PDF automatically)
            using (Form form = new Form(inputPdf))
            {
                // STEP 2: Capture original field values
                var originalValues = new Dictionary<string, object>();
                foreach (string fieldName in form.FieldNames)
                {
                    originalValues[fieldName] = form.GetField(fieldName);
                }

                // STEP 3: Export form data to XFDF (in‑memory round‑trip)
                using (MemoryStream xfdfStream = new MemoryStream())
                {
                    form.ExportXfdf(xfdfStream);
                    xfdfStream.Position = 0; // reset for reading

                    // (Optional) write XFDF to a file for manual inspection
                    File.WriteAllBytes(xfdfPath, xfdfStream.ToArray());

                    // STEP 4: Import the XFDF back into a fresh Form instance
                    using (Form formImport = new Form(inputPdf))
                    {
                        formImport.ImportXfdf(xfdfStream);
                        formImport.Save(outputPdf);
                    }
                }

                // STEP 5: Verify round‑trip preservation
                using (Form verificationForm = new Form(outputPdf))
                {
                    bool allMatch = true;
                    foreach (string fieldName in verificationForm.FieldNames)
                    {
                        object original = originalValues.ContainsKey(fieldName) ? originalValues[fieldName] : null;
                        object imported = verificationForm.GetField(fieldName);

                        if (!object.Equals(original, imported))
                        {
                            allMatch = false;
                            Console.WriteLine($"Mismatch in field '{fieldName}': original='{original}' , imported='{imported}'");
                        }
                    }

                    Console.WriteLine(allMatch
                        ? "Round‑trip successful: all field values are preserved."
                        : "Round‑trip completed with mismatches (see above).");
                }
            }
        }
    }
}
