using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input_form.pdf";      // source PDF with form fields
        const string xfdfPath       = "form_data.xfdf";      // temporary XFDF file
        const string importedPdfPath = "imported_form.pdf"; // PDF after XFDF import

        // Verify source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the PDF, fill some fields, and export the form data to XFDF
        // -----------------------------------------------------------------
        using (Document doc = new Document(inputPdfPath))
        {
            // Initialize the Form facade on the loaded document
            using (Form form = new Form(doc))
            {
                // Example: fill a couple of fields (replace with actual field names)
                // Note: field names are case‑sensitive and must be fully qualified.
                form.FillField("TextField1", "Sample Text");
                form.FillField("CheckBox1", true);
                form.FillField("RadioGroup1", 2); // select the second option

                // Export the filled form data to XFDF
                using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    form.ExportXfdf(xfdfStream);
                }

                // Save the PDF with filled values (optional, for visual inspection)
                doc.Save("filled_form.pdf");
            }
        }

        // ---------------------------------------------------------------
        // 2. Load a fresh copy of the original PDF and import the XFDF data
        // ---------------------------------------------------------------
        using (Document docImport = new Document(inputPdfPath))
        {
            using (Form formImport = new Form(docImport))
            {
                // Import the previously exported XFDF data
                using (FileStream xfdfRead = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
                {
                    formImport.ImportXfdf(xfdfRead);
                }

                // Save the document after import – this PDF should contain the same values
                formImport.Save(importedPdfPath);
            }
        }

        // ---------------------------------------------------------------
        // 3. Verify that the round‑trip preserved the field values
        // ---------------------------------------------------------------
        using (Document verifyDoc = new Document(importedPdfPath))
        {
            using (Form verifyForm = new Form(verifyDoc))
            {
                // Retrieve field values and display them
                string textValue   = verifyForm.GetField("TextField1")?.ToString() ?? "(null)";
                bool   checkValue  = Convert.ToBoolean(verifyForm.GetField("CheckBox1"));
                int    radioValue  = Convert.ToInt32(verifyForm.GetField("RadioGroup1"));

                Console.WriteLine("Verification after XFDF round‑trip:");
                Console.WriteLine($"TextField1 : {textValue}");
                Console.WriteLine($"CheckBox1 : {checkValue}");
                Console.WriteLine($"RadioGroup1: {radioValue}");
            }
        }
    }
}