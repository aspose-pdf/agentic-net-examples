using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "template.pdf";   // PDF that contains an AcroForm
        const string xfdfPath      = "data.xfdf";      // Optional XFDF file with field values
        const string filledPdfPath = "filled.pdf";     // Resulting PDF after filling
        const string exportXmlPath = "export.xml";     // Exported form data in XML format

        // Verify that the source PDF exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Fill form fields using the Form facade (Aspose.Pdf.Facades)
        // -----------------------------------------------------------------
        // The Form class implements IDisposable, so we wrap it in a using block.
        using (Form form = new Form(sourcePdfPath))
        {
            // Example of filling individual fields.
            // Field names are fully qualified (e.g., "Parent.Child.Field").
            form.FillField("Customer.Name", "John Doe");   // Text field
            form.FillField("AgreeTerms", true);           // Check box
            form.FillField("Gender", 1);                  // Radio button (index)

            // If an XFDF file is provided, import its values.
            if (File.Exists(xfdfPath))
            {
                using (FileStream xfdfStream = File.OpenRead(xfdfPath))
                {
                    form.ImportXfdf(xfdfStream);
                }
            }

            // Save the filled PDF to disk.
            form.Save(filledPdfPath);
        }

        // -----------------------------------------------------------------
        // 2. Export the filled form data to XML (using the same Form facade)
        // -----------------------------------------------------------------
        // Bind the newly created PDF and export its field values.
        using (Form exportForm = new Form(filledPdfPath))
        {
            using (FileStream xmlStream = File.Create(exportXmlPath))
            {
                exportForm.ExportXml(xmlStream);
            }
        }

        Console.WriteLine("Form processing completed successfully.");
    }
}