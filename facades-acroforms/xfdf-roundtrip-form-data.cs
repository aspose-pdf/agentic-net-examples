using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class FormXfdfRoundTrip
{
    static void Main()
    {
        const string inputPdfPath  = "form.pdf";          // source PDF with AcroForm fields
        const string outputPdfPath = "form_roundtrip.pdf"; // PDF after import
        const string textFieldName = "TextBox1";          // example field names
        const string checkBoxName = "CheckBox1";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Load the source PDF and fill some fields
            // ------------------------------------------------------------
            using (Document srcDoc = new Document(inputPdfPath))
            {
                Form srcForm = new Form(srcDoc);
                srcForm.FillField(textFieldName, "Sample Text");
                srcForm.FillField(checkBoxName, true);

                // --------------------------------------------------------
                // 2. Export the filled form data to XFDF (in memory)
                // --------------------------------------------------------
                using (MemoryStream xfdfStream = new MemoryStream())
                {
                    srcForm.ExportXfdf(xfdfStream);
                    xfdfStream.Position = 0; // reset for reading

                    // ----------------------------------------------------
                    // 3. Load a fresh copy of the PDF (empty fields) and import XFDF
                    // ----------------------------------------------------
                    using (Document dstDoc = new Document(inputPdfPath))
                    {
                        Form dstForm = new Form(dstDoc);
                        dstForm.ImportXfdf(xfdfStream);

                        // ------------------------------------------------
                        // 4. Verify that the field values were restored
                        // ------------------------------------------------
                        string importedText = dstForm.GetField(textFieldName) as string;

                        // The value returned for a check box is a string (e.g., "On"/"Off").
                        // Convert it safely to a boolean instead of casting directly.
                        string checkBoxValue = dstForm.GetField(checkBoxName) as string;
                        bool importedCheck = false;
                        if (!string.IsNullOrEmpty(checkBoxValue))
                        {
                            // Accept typical PDF checkbox values.
                            importedCheck = string.Equals(checkBoxValue, "On", StringComparison.OrdinalIgnoreCase) ||
                                            string.Equals(checkBoxValue, "Yes", StringComparison.OrdinalIgnoreCase) ||
                                            bool.TryParse(checkBoxValue, out bool parsed) && parsed;
                        }

                        Console.WriteLine($"Imported TextField value : \"{importedText}\"");
                        Console.WriteLine($"Imported CheckBox value   : {importedCheck}");

                        // ------------------------------------------------
                        // 5. Save the document with imported data
                        // ------------------------------------------------
                        dstDoc.Save(outputPdfPath);
                    }
                }
            }

            Console.WriteLine($"Round‑trip XFDF export/import completed. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
