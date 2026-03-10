using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input_form.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the PDF with AcroForm using the Form facade
        // -----------------------------------------------------------------
        using (Form form = new Form(inputPdf))
        {
            // List all field names (useful for debugging)
            Console.WriteLine("AcroForm fields:");
            foreach (string name in form.FieldNames)
                Console.WriteLine($" - {name}");

            // -----------------------------------------------------------------
            // 2. Export field data to the three supported formats
            // -----------------------------------------------------------------
            // FDF (Forms Data Format) – a compact binary format used by PDF viewers.
            using (FileStream fdfStream = new FileStream("fields.fdf", FileMode.Create, FileAccess.Write))
                form.ExportFdf(fdfStream);

            // XML – a verbose representation of field names and values.
            using (FileStream xmlStream = new FileStream("fields.xml", FileMode.Create, FileAccess.Write))
                form.ExportXml(xmlStream);

            // XFDF (XML Forms Data Format) – XML version of FDF, compatible with many tools.
            using (FileStream xfdfStream = new FileStream("fields.xfdf", FileMode.Create, FileAccess.Write))
                form.ExportXfdf(xfdfStream);
        }

        // -----------------------------------------------------------------
        // 3. Demonstrate importing each format back into a PDF copy
        // -----------------------------------------------------------------
        // Helper to copy the original PDF for each import scenario
        void ImportAndSave(string sourcePdf, Action<Form> importAction, string outputPdf)
        {
            // Copy the original PDF to a new file that will receive the imported data
            File.Copy(inputPdf, sourcePdf, true);

            using (Form importForm = new Form(sourcePdf))
            {
                importAction(importForm);
                // Save changes back to the same file (SaveableFacade supports Save())
                importForm.Save();
            }

            // Optionally rename the processed file to a distinct output name
            if (File.Exists(outputPdf))
                File.Delete(outputPdf);
            File.Move(sourcePdf, outputPdf);
        }

        // Import from FDF
        ImportAndSave("temp_fdf.pdf",
            f => f.ImportFdf(new FileStream("fields.fdf", FileMode.Open, FileAccess.Read)),
            "imported_from_fdf.pdf");

        // Import from XML
        ImportAndSave("temp_xml.pdf",
            f => f.ImportXml(new FileStream("fields.xml", FileMode.Open, FileAccess.Read)),
            "imported_from_xml.pdf");

        // Import from XFDF
        ImportAndSave("temp_xfdf.pdf",
            f => f.ImportXfdf(new FileStream("fields.xfdf", FileMode.Open, FileAccess.Read)),
            "imported_from_xfdf.pdf");

        Console.WriteLine("Export and import operations completed.");
    }
}