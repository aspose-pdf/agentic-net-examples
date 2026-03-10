using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source_form.pdf";   // PDF containing the form fields
        const string exportedJsonPath = "form_fields.json"; // External format for manipulation
        const string targetPdfPath = "target_form.pdf";   // PDF to receive imported fields

        // Verify that the source PDF exists before proceeding
        if (!File.Exists(sourcePdfPath))
        {
            Console.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the source PDF using the Form facade
        // -----------------------------------------------------------------
        using (Form form = new Form(sourcePdfPath))
        {
            // -----------------------------------------------------------------
            // 2. Export the form fields to JSON (external format)
            //    ExportJson(Stream outputJsonStream, bool includeButtonFields)
            // -----------------------------------------------------------------
            using (FileStream jsonOut = new FileStream(exportedJsonPath, FileMode.Create, FileAccess.Write))
            {
                // 'false' – button field values are not exported (default behavior)
                form.ExportJson(jsonOut, false);
            }

            // Optional: also export to other formats (FDF, XFDF, XML) if needed
            // using (FileStream fdfOut = new FileStream("form_fields.fdf", FileMode.Create, FileAccess.Write))
            // {
            //     form.ExportFdf(fdfOut);
            // }
        }

        // -----------------------------------------------------------------
        // 3. Import the previously exported JSON back into a (new or existing) PDF
        // -----------------------------------------------------------------
        using (Form targetForm = new Form(targetPdfPath))
        {
            using (FileStream jsonIn = new FileStream(exportedJsonPath, FileMode.Open, FileAccess.Read))
            {
                // ImportJson(Stream inputJsonStream) – matches fields by full names
                targetForm.ImportJson(jsonIn);
            }

            // Save the changes to the target PDF. The parameterless Save() method is obsolete;
            // use the overload that accepts a destination path.
            targetForm.Save(targetPdfPath);
        }

        Console.WriteLine("Form fields exported to JSON and imported into target PDF successfully.");
    }
}
