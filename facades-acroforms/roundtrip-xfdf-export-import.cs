using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "form.pdf";          // source PDF with AcroForm fields
        const string outputPdf = "form_roundtrip.pdf"; // result after import

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ---------- Fill the form with sample data ----------
        using (Form filler = new Form(inputPdf))
        {
            // Fill each field with a distinct test value
            foreach (string fieldName in filler.FieldNames)
            {
                filler.FillField(fieldName, $"TestValue_{fieldName}");
            }

            // Export the filled data to XFDF (in memory)
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                filler.ExportXfdf(xfdfStream);
                xfdfStream.Position = 0; // reset for reading

                // ---------- Import the XFDF into a fresh form ----------
                using (Form importer = new Form(inputPdf))
                {
                    // Clear existing values (optional, demonstrates round‑trip)
                    foreach (string name in importer.FieldNames)
                    {
                        importer.FillField(name, string.Empty);
                    }

                    // Import the previously exported XFDF data
                    importer.ImportXfdf(xfdfStream);
                    importer.Save(outputPdf); // persist the imported values
                }

                // ---------- Verify that values were preserved ----------
                using (Form verifier = new Form(outputPdf))
                {
                    bool allMatch = true;
                    foreach (string fieldName in verifier.FieldNames)
                    {
                        object value = verifier.GetField(fieldName);
                        string expected = $"TestValue_{fieldName}";
                        if (!expected.Equals(value?.ToString()))
                        {
                            allMatch = false;
                            Console.WriteLine($"Mismatch in field '{fieldName}': expected '{expected}', got '{value}'");
                        }
                    }

                    Console.WriteLine(allMatch
                        ? "Round‑trip successful: all field values match."
                        : "Round‑trip failed: some field values differ.");
                }
            }
        }
    }
}