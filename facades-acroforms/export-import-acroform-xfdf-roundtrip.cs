using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";          // source PDF with form fields
        const string outputPdf = "form_roundtrip.pdf"; // PDF after XFDF round‑trip

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // ------------------------------------------------------------
        // Export form data to XFDF (in memory)
        // ------------------------------------------------------------
        using (Form exportForm = new Form(inputPdf))
        {
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                exportForm.ExportXfdf(xfdfStream);
                xfdfStream.Position = 0; // reset for reading

                Console.WriteLine($"Exported XFDF size: {xfdfStream.Length} bytes");

                // ------------------------------------------------------------
                // Import the XFDF back into a new PDF (round‑trip)
                // ------------------------------------------------------------
                using (Form importForm = new Form(inputPdf, outputPdf))
                {
                    importForm.ImportXfdf(xfdfStream);
                    importForm.Save(); // persist the imported data
                }

                // ------------------------------------------------------------
                // Verify that field values are identical before and after
                // ------------------------------------------------------------
                using (Form originalForm = new Form(inputPdf))
                using (Form roundTripForm = new Form(outputPdf))
                {
                    foreach (string fieldName in originalForm.FieldNames)
                    {
                        object originalValue = originalForm.GetField(fieldName);
                        object roundTripValue = roundTripForm.GetField(fieldName);
                        bool match = Equals(originalValue, roundTripValue);
                        Console.WriteLine($"Field '{fieldName}': original='{originalValue}' | round‑trip='{roundTripValue}' => {(match ? "OK" : "MISMATCH")}");
                    }
                }
            }
        }

        Console.WriteLine($"Round‑trip PDF saved as '{outputPdf}'.");
    }
}